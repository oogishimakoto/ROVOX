#include "TitleScene.h"

//switch用識別番号
extern int sceneNo;

extern bool endcheck;

TitleScene::TitleScene()
{
	LoadTexture(L"assets/UI_title.jpg", &gpTextureTitle01);

	LoadTexture(L"assets/title_logo.png", &gpTextureTitleLogo);

	LoadTexture(L"assets/UI_Start.png", &gpTextureHosi01);

	LoadTexture(L"assets/UI_Exit.png", &gpTextureDool01);

	//タイトルの背景画面
	Titele = new StaticObject();
	Titele->mSprite->SetTexture(gpTextureTitle01);
	Titele->mSprite->mSize.x = 2.9f;
	Titele->mSprite->mSize.y = 1.5f;
	Titele->mSprite->mCenter.x = 0.0f;
	Titele->mSprite->mCenter.y = 0.0f;

	//タイトルのロゴ
	Titlelogo = new StaticObject();
	Titlelogo->mSprite->SetTexture(gpTextureTitleLogo);
	Titlelogo->mSprite->mCenter.y = 0.26f;
	Titlelogo->SetSize(1.05f,0.8f);

	//タイトルのスタートロゴ
	StartLogo = new AnimHitObject();
	StartLogo->mSprite->SetTexture(gpTextureHosi01);
	StartLogo->mSprite->mCenter.x = -0.5f;
	StartLogo->mSprite->mCenter.y = -0.37f;
	StartLogo->SetSize(0.65f, 0.35f);

	//タイトルの終了ロゴ
	endLogo = new AnimHitObject();
	endLogo->mSprite->SetTexture(gpTextureDool01);
	endLogo->mSprite->mCenter.x = 0.5f;
	endLogo->mSprite->mCenter.y = -0.37f;
	endLogo->SetSize(0.65f, 0.35f);

	//マウス画像読み込み
	LoadTexture(L"assets/MouseN.png", &gpTextureMouse01);
	LoadTexture(L"assets/MouseY.png", &gpTextureMouse02);

	//マウス実体化
	gpMouse = new Operation();
	gpMouse->mSprite->SetTexture(gpTextureMouse01);
	gpMouse->SetSize(0.35f, 0.35f);

	// カーソルの位置をセットする
	SetCursorPos(0.0f, 0.0f);
}

TitleScene::~TitleScene()
{
	delete Titele;
	delete Titlelogo;
	delete StartLogo;
	delete endLogo;

	// マウス解放
	delete gpMouse;

	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureMouse02);
	COM_SAFE_RELEASE(gpTextureHosi01);
	COM_SAFE_RELEASE(gpTextureDool01);
	COM_SAFE_RELEASE(gpTextureTitle01);
	COM_SAFE_RELEASE(gpTextureTitleLogo);
}

void TitleScene::Update()
{
	//ここに処理を書く
	Titele->Update();
	Titlelogo->Update();
	StartLogo->Update();
	endLogo->Update();

	//マウスクリックしたら音を鳴らす
	if (Input_GetKeyTrigger(VK_LBUTTON))
	{
		XA_Play(SOUND_LABEL_SE001, 0.45f);
	}

	if (Input_GetKeyDown(VK_LBUTTON))
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse02);
	}
	else
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse01);
	}

	gpMouse->Update();

	if (gpMouse->mHitCircle->IsHit(StartLogo->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE007, 0.5f);
			//リザルトに変える
			sceneNo = 2;
		}
	}

	if (gpMouse->mHitCircle->IsHit(endLogo->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			//リザルトに変える
			endcheck = true;
		}
	}
}

void TitleScene::Draw()
{
	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	Titele->Draw();
	Titlelogo->Draw();
	StartLogo->Draw();
	endLogo->Draw();

	gpMouse->Draw();
}
