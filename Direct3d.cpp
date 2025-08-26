#include"Direct3d.h"

//DirectX11リンク設定
#pragma comment(lib,"d3d11.lib")

//コンパイル済みシェーダーをインクルード
#include"vs.h"
#include"ps.h"

//プロトタイプ宣言

//デバイスを作成する関数
BOOL CreateDevice(HWND hWnd);

//レンダーターゲットを作成する関数
BOOL CreateRnderTarget();

//シェーダーオブジェクトを作成する関数
BOOL CreateSheder();

//ビューポートを設定する関数
void SetViewPort();

//グローバル変数の宣言

//DIRECT3D構造体を動的確保してアドレスを入れる変数
static DIRECT3D* gpD3D;

//画面の幅と高さを持つ変数
static UINT gScreenWidth, gScreenHeight;

BOOL Direct3D_Init(HWND hWnd)
{
	//DIRECT3D構造体を動的確保
	//malloc関数→引数で指定したサイズのメモリを確保し、
	//			  そのアドレスを返す。
	gpD3D = (DIRECT3D*)malloc(sizeof(DIRECT3D));

	//上で確保したメモリをゼロクリアする
	//ZeroMemory関数→第一引数でしたアドレスから、
	//				  第二引数で指定したサイズ分、0で埋める
	ZeroMemory(gpD3D, sizeof(DIRECT3D));

	//デバイスを作成する関数を呼び出す
	CreateDevice(hWnd);

	//レンダーターゲットを作成する関数を呼び出す
	CreateRnderTarget();

	//シェーダーオブジェクトを作成する関数を呼び出す
	CreateSheder();

	//ビューポートを設定する関数を呼び出す
	SetViewPort();

	return 0;
}

void Direct3D_Release()
{
	COM_SAFE_RELEASE(gpD3D->renderTarget);
	COM_SAFE_RELEASE(gpD3D->swapChain);
	COM_SAFE_RELEASE(gpD3D->context);
	COM_SAFE_RELEASE(gpD3D->device);
	COM_SAFE_RELEASE(gpD3D->vertexShader);
	COM_SAFE_RELEASE(gpD3D->pixelShader);
	COM_SAFE_RELEASE(gpD3D->inputLayout);
	COM_SAFE_RELEASE(gpD3D->blendAlpha);
	COM_SAFE_RELEASE(gpD3D->samplerPoint);

	//DIRECT3D構造体の実体を解放する
	if (gpD3D != NULL)
	{
		free(gpD3D);
		gpD3D = NULL;
	}
}

DIRECT3D * Direct3D_Get()
{
	return gpD3D;
}

BOOL CreateDevice(HWND hWnd)
{
	UINT flags = 0;
	// Graphic Toolを使ってデバッグを行う場合は、下の行のコメントを外してデバッグモードでデバイスを作成する。
	//flags |= D3D11_CREATE_DEVICE_DEBUG;

	D3D_FEATURE_LEVEL pLevels[] = { D3D_FEATURE_LEVEL_11_0 };
	D3D_FEATURE_LEVEL level;

	RECT                rect;
	DXGI_SWAP_CHAIN_DESC scDesc;

	// ウインドウのクライアント領域（実際に描画できる範囲）のサイズを取得
	GetClientRect(hWnd, &rect);
	ZeroMemory(&scDesc, sizeof(scDesc));

	gScreenWidth = (UINT)(rect.right - rect.left);
	gScreenHeight = (UINT)(rect.bottom - rect.top);

	scDesc.BufferCount = 1;
	scDesc.BufferDesc.Width = gScreenWidth;
	scDesc.BufferDesc.Height = gScreenHeight;
	scDesc.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
	scDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
	scDesc.OutputWindow = hWnd;
	scDesc.SampleDesc.Count = 1;
	scDesc.SampleDesc.Quality = 0;
	scDesc.Windowed = TRUE; // ウインドウモードとフルスクリーンモード切り替え

	HRESULT  hr;
	// DirectX11デバイス、コンテキスト、スワップチェインの作成
	hr = D3D11CreateDeviceAndSwapChain(NULL,
		D3D_DRIVER_TYPE_HARDWARE,
		NULL,
		flags,
		pLevels,
		1,
		D3D11_SDK_VERSION,
		&scDesc,
		&gpD3D->swapChain,
		&gpD3D->device,
		&level,
		&gpD3D->context);

	return SUCCEEDED(hr);

}

BOOL CreateRnderTarget()
{
	// バックバッファ取得
	ID3D11Texture2D* pBackBuffer = NULL;

	HRESULT hr;
	hr = gpD3D->swapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer);

	if (SUCCEEDED(hr))
	{
		// レンダーターゲットビューを作成
		hr = gpD3D->device->CreateRenderTargetView(pBackBuffer, NULL, &gpD3D->renderTarget);


		if (SUCCEEDED(hr))
		{
			// レンダリングターゲットを設定　（注）今は2DなのでZバッファ無効にしておく
			gpD3D->context->OMSetRenderTargets(1, &gpD3D->renderTarget, NULL);

			return TRUE;
		}
	}

	COM_SAFE_RELEASE(pBackBuffer); // バックバッファ解放

	return FALSE;

}

BOOL CreateSheder()
{
	HRESULT hr;
	//頂点シェーダー生成
	hr = gpD3D->device->CreateVertexShader(&g_vs_main, sizeof(g_vs_main), NULL, &gpD3D->vertexShader);

	if (FAILED(hr))
		return FALSE;

	//ピクセルシェーダー生成
	hr = gpD3D->device->CreatePixelShader(&g_ps_main, sizeof(g_ps_main), NULL, &gpD3D->pixelShader);

	if (FAILED(hr))
		return FALSE;

	// 頂点シェーダーのパラメータセット
	gpD3D->context->VSSetShader(gpD3D->vertexShader, NULL, 0);
	// ピクセルシェーダーのパラメータセット
	gpD3D->context->PSSetShader(gpD3D->pixelShader, NULL, 0);

	//頂点レイアウト作成　※頂点データを変更したら、この配列も変更する
	D3D11_INPUT_ELEMENT_DESC vertexDesc[] = {
		//{ "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0 }, // xyz
		   { "POSITION", 0, DXGI_FORMAT_R32G32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0 },  // xy
		   { "TEX", 0, DXGI_FORMAT_R32G32_FLOAT, 0, D3D11_APPEND_ALIGNED_ELEMENT, D3D11_INPUT_PER_VERTEX_DATA, 0 },  // uv
		   { "COL", 0, DXGI_FORMAT_R32G32B32A32_FLOAT, 0,
		  D3D11_APPEND_ALIGNED_ELEMENT, D3D11_INPUT_PER_VERTEX_DATA, 0 },// color

	};

	hr = gpD3D->device->CreateInputLayout(vertexDesc, ARRAYSIZE(vertexDesc),
		g_vs_main, sizeof(g_vs_main), &gpD3D->inputLayout);

	if (FAILED(hr))
		return FALSE;

	// 頂点レイアウトをセット
	gpD3D->context->IASetInputLayout(gpD3D->inputLayout);
	// トポロジー（プリミティブタイプ）をセット
	gpD3D->context->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST); // 三角形リストを指定

	// ブレンドステート設定
	D3D11_BLEND_DESC BlendDesc;

	// テクスチャ透過処理が行われるように設定
	ZeroMemory(&BlendDesc, sizeof(BlendDesc));
	BlendDesc.AlphaToCoverageEnable = FALSE;
	BlendDesc.IndependentBlendEnable = FALSE;
	BlendDesc.RenderTarget[0].BlendEnable = TRUE;
	BlendDesc.RenderTarget[0].SrcBlend = D3D11_BLEND_SRC_ALPHA;
	BlendDesc.RenderTarget[0].DestBlend = D3D11_BLEND_INV_SRC_ALPHA;
	BlendDesc.RenderTarget[0].BlendOp = D3D11_BLEND_OP_ADD;
	BlendDesc.RenderTarget[0].SrcBlendAlpha = D3D11_BLEND_ZERO;
	BlendDesc.RenderTarget[0].DestBlendAlpha = D3D11_BLEND_ONE;
	BlendDesc.RenderTarget[0].BlendOpAlpha = D3D11_BLEND_OP_ADD;
	BlendDesc.RenderTarget[0].RenderTargetWriteMask = 0x0f;

	hr = gpD3D->device->CreateBlendState(&BlendDesc, &gpD3D->blendAlpha);

	if (SUCCEEDED(hr)) {
		gpD3D->context->OMSetBlendState(gpD3D->blendAlpha, NULL, 0xffffffff);
	}

	// サンプラーステートを設定
	D3D11_SAMPLER_DESC samplerDesc;
	samplerDesc.Filter = D3D11_FILTER_MIN_MAG_MIP_POINT; // ポイント補完
	samplerDesc.AddressU = D3D11_TEXTURE_ADDRESS_CLAMP;
	samplerDesc.AddressV = D3D11_TEXTURE_ADDRESS_CLAMP;
	samplerDesc.AddressW = D3D11_TEXTURE_ADDRESS_CLAMP;
	samplerDesc.MipLODBias = 0.0f;
	samplerDesc.MaxAnisotropy = 1;

	hr = gpD3D->device->CreateSamplerState(&samplerDesc, &gpD3D->samplerPoint);

	return TRUE;
}

void SetViewPort()
{
	//ビューポート設定
	D3D11_VIEWPORT viewport;
	viewport.TopLeftX = 0;
	viewport.TopLeftY = 0;
	viewport.Width = (FLOAT)gScreenWidth;
	viewport.Height = (FLOAT)gScreenHeight;
	viewport.MinDepth = 0.0f;
	viewport.MaxDepth = 1.0f;

	gpD3D->context->RSSetViewports(1, &viewport);

}
