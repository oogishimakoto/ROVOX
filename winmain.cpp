#undef UNICODE // Unicodeではなく、マルチバイト文字を使う

//----------------------------------------------------------------------------------
// メインインクルード
#include "winmain.h"
#include <iostream>     // cout
#include <ctime>        // time
#include <cstdlib>      // srand,rand

//----------------------------------------------------------------------------------
// メモリリークを調べる為に必要
#include "crtdbg.h"
#include <functional>
#define malloc(X) _malloc_dbg(X,_NORMAL_BLOCK,__FILE__,__LINE__) 
#define new ::new(_NORMAL_BLOCK, __FILE__, __LINE__)

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR IpCmdLine, int nCmdShow)
{
#ifdef _DEBUG
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
#endif // _DEBUG
	//乱数初期化
	srand((unsigned int)time(NULL));

	//ウィンドウのパラメータを決めて、登録
	WNDCLASSEX wc;

	wc.cbSize = sizeof(WNDCLASSEX);
	wc.style = CS_CLASSDC;
	wc.lpfnWndProc = WndProc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hInstance = hInstance;
	wc.hIcon = NULL;
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wc.lpszMenuName = NULL;
	wc.lpszClassName = CLASS_NAME;
	wc.hIconSm = NULL;

	RegisterClassEx(&wc);

	//ウィンドウを作る関数
	HWND hWnd;
	hWnd = CreateWindowEx(0,// 拡張ウィンドウスタイル
		CLASS_NAME,// ウィンドウクラスの名前
		WINDOW_NAME,// ウィンドウの名前
		//WS_OVERLAPPEDWINDOW,// ウィンドウスタイル
		WS_POPUP | WS_SYSMENU | WS_MINIMIZEBOX,// ウィンドウスタイル画面サイズ固定		×ボタンあり	最小化あり
		CW_USEDEFAULT,// ウィンドウの左上Ｘ座標
		CW_USEDEFAULT,// ウィンドウの左上Ｙ座標 
		SCREEN_WIDTH,// ウィンドウの幅
		SCREEN_HEIGHT,// ウィンドウの高さ
		NULL,// 親ウィンドウのハンドル
		NULL,// メニューハンドルまたは子ウィンドウID
		hInstance,// インスタンスハンドル
		NULL);// ウィンドウ作成データ

	// 指定されたウィンドウの表示状態を設定(ウィンドウを表示)
	ShowWindow(hWnd, nCmdShow);
	// ウィンドウの状態を直ちに反映(ウィンドウのクライアント領域を更新)
	UpdateWindow(hWnd);

	//Direct3Dの初期化関数を呼び出す
	Direct3D_Init(hWnd);

	//----------------------------------------------------------------------------------
	// マウス系
	// マウスカーソルの非表示
	ShowCursor(FALSE);

	// カーソルの位置取得
	GetCursorPos(&cursor);
	// カーソルの位置をセットする
	SetCursorPos(0.0f, 0.0f);

	//----------------------------------------------------------------------------------
	// FPS測定で必要な情報取得
	//現在時刻をマイクロ秒で取得
	std::function<long long(void)> currentTimeMicro = []()
	{
		std::chrono::system_clock::duration d = std::chrono::system_clock::now().time_since_epoch();
		return std::chrono::duration_cast<std::chrono::microseconds>(d).count();
	};

	//現在時刻を取得(1秒=1000000)
	long long end = currentTimeMicro();

	//次の更新時間を計算(1秒/フレームレート)
	long long next = end + (1000 * 1000 / fps);

	//----------------------------------------------------------------------------------
	// 初期化
	HDC hdc = GetDC(hWnd);

	// サウンド初期化
	XA_Initialize();

	std::srand(time(NULL));

	//----------------------------------------------------------------------------------
	//メッセージループ
	MSG msg = {};
	// メインループ
	for (;;)
	{
		// 前回のループからユーザー操作があったか調べる
		BOOL doesMessageExist = PeekMessage(&msg, NULL, 0, 0, PM_REMOVE);

		if (doesMessageExist)
		{ // ユーザー操作があった場合

			// 間接的にウインドウプロシージャを呼び出す
			DispatchMessage(&msg);
			TranslateMessage(&msg);
			// アプリ終了命令が来た
			if (msg.message == WM_QUIT)
			{
				// 終了
				break;
			}
		}
		else
		{
			//ゲームの処理を記述
			//DirectXの描画処理などもここに記述する
			{
				timeBeginPeriod(1); // 精度を１ミリ秒に上げる

				cnt++;				// fps計算用加算変数に加算していく

				//fps描画
				std::string* fpsStr = fr.update();
				const char* fpsStrResult = fpsStr->c_str();
				TextOut(hdc, 10, 1000, fpsStrResult, fpsStr->size());

				//重い処理があった場合
				std::this_thread::sleep_for(std::chrono::milliseconds(5));	// 処理を止める

				//できるだけ60fpsになるようにスレッド待機
				end = currentTimeMicro();
				if (end < next)
				{
					//更新時間まで待機
					std::this_thread::sleep_for(std::chrono::microseconds(next - end));

					//次の更新時間を計算(1秒/フレームレート加算)
					next += (1000 * 1000 / fps);
				}
				else
				{
					//更新時間を過ぎた場合は現在時刻から次の更新時間を計算
					next = end + (1000 * 1000 / fps);
				}
				gDeltaTime = next;	// デルタタイムに固定fpsの差分渡す
				timeEndPeriod(1);	// 精度を元に戻す
			}

			// カーソルの位置を毎フレーム取得する
			GetCursorPos(&cursor);

			Game.Initialize();	// シーン更新
			Game.Update();		// 更新処理
			Game.Draw();		// 描画処理	

			Input_Refresh(); // キー状態の更新

			if (endcheck == true)
				break;
		}
	}

	// サウンド解放
	XA_Release();

	Game.Relesase();	// 解放処理

	//Direct3Dの解放関数を呼び出す
	Direct3D_Release();

	//ループを抜けたらアプリの終了処理をする
	UnregisterClass(CLASS_NAME, hInstance);

	return (int)msg.wParam;
}

// ウインドウプロシージャ関数を作る
// ※関数を作れるのはグローバル領域(=どの関数の中でもない場所)だけ！
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	// uMsg（この関数の第2引数）が、ユーザー操作のID情報
	switch (uMsg)
	{
	case WM_DESTROY:// ウィンドウ破棄のメッセージ
		PostQuitMessage(0);// “WM_QUIT”メッセージを送る　→　アプリ終了
		break;

	case WM_CLOSE:  // xボタンが押されたら
		DestroyWindow(hWnd);  // “WM_DESTROY”メッセージを送る
		break;

	case WM_LBUTTONDOWN: // 左クリックされたとき
		Input_SetKeyDown(VK_LBUTTON);
		clickCheck = true;
		break;

	case WM_LBUTTONUP: // 左クリックされたとき
		Input_SetKeyUp(VK_LBUTTON);
		clickCheck = false;
		break;

	case WM_RBUTTONDOWN: // 右クリックされたとき
		Input_SetKeyDown(VK_RBUTTON);
		break;

	case WM_RBUTTONUP: // 右クリックされたとき
		Input_SetKeyUp(VK_RBUTTON);
		break;

	case WM_MOUSEMOVE: // マウスカーソルが動いたとき

		// カーソルの位置取得
		GetCursorPos(&cursor);

		break;

	case WM_KEYDOWN:
		// キーが押された時のリアクションを書く
		// ESCが押されたのかどうかチェック
		if (LOWORD(wParam) == VK_ESCAPE)
		{
			// メッセージボックスで修了確認
			int result;
			result = MessageBox(NULL, "終了してよろしいですか？",
				"終了確認", MB_YESNO | MB_ICONQUESTION);
			if (result == IDYES) // 「はい」ボタンが押された時
			{
				// xボタンが押されたのと同じ効果を発揮する
				PostMessage(hWnd, WM_CLOSE, wParam, lParam);
			}
		}
		Input_SetKeyDown(LOWORD(wParam));
		break;

	case WM_KEYUP: // キーが離されたイベント
		Input_SetKeyUp(LOWORD(wParam));
		break;

	default:
		// 上のcase以外の場合の処理を実行
		return DefWindowProc(hWnd, uMsg, wParam, lParam);
		break;
	}
	return 0;
}
