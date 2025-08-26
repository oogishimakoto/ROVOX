#include "ResultScene.h"

extern int latterCount;

ResultScene::ResultScene()
{
	//ドール画像読み込み
	LoadTexture(L"assets/ridy-yorokobi.png", &gpTextureDool01);

	LoadTexture(L"assets/result_BG.png", &gpTextureBackGround);

	LoadTexture(L"assets/2.png", &gpTextureResult01);

	LoadTexture(L"assets/ridy-kira.png", &gpTextureHosi01);

	LoadTexture(L"assets/UI_Go_Title.png", &gpTexturetitleGoLogo);

	LoadTexture(L"assets/14.png", &gpTextureSelectGoLogo);


	//ドール実体化
	dool = new StaticObject();
	dool->mSprite->SetTexture(gpTextureDool01);
	dool->mSprite->mCenter.x = -0.8f;
	dool->mSprite->mCenter.y = -0.2f;
	dool->SetSize(1.5f, 1.5f);

	//背景
	backGround = new StaticObject();
	backGround->mSprite->SetTexture(gpTextureBackGround);
	backGround->mSprite->mSize.x = 2.8f;
	backGround->mSprite->mSize.y = 1.5f;
	backGround->mSprite->mCenter.x = 0.0f;
	backGround->mSprite->mCenter.y = 0.0f;

	//resultの画面
	result = new StaticObject();
	result->mSprite->SetTexture(gpTextureResult01);
	result->mSprite->mSize.x = 2.675f / 2.0f;
	result->mSprite->mSize.y = 1.5f;
	result->mSprite->mCenter.x = 0.7f;
	result->mSprite->mCenter.y = 0.0f;

	//⋆
	hosi = new StaticObject();
	hosi->mSprite->SetTexture(gpTextureHosi01);
	hosi->mSprite->mCenter.x = -0.8f;
	hosi->mSprite->mCenter.y = -0.0f;
	hosi->SetSize(1.5f, 1.5f);

	//タイトルへ行くロゴ
	titleLogo = new AnimHitObject();
	titleLogo->mSprite->SetTexture(gpTexturetitleGoLogo);
	titleLogo->mSprite->mCenter.x = 0.4f;
	titleLogo->mSprite->mCenter.y = -0.65f;
	titleLogo->SetSize(0.45f, 0.25f);

	//select画面へ行くロゴ
	selectLogo = new AnimHitObject();
	selectLogo->mSprite->SetTexture(gpTextureSelectGoLogo);
	selectLogo->mSprite->mCenter.x = 0.95f;
	selectLogo->mSprite->mCenter.y = -0.65f;
	selectLogo->SetSize(0.45f, 0.25f);

	//マウス画像読み込み
	LoadTexture(L"assets/MouseN.png", &gpTextureMouse01);
	LoadTexture(L"assets/MouseY.png", &gpTextureMouse02);

	//マウス実体化
	gpMouse = new Operation();
	gpMouse->mSprite->SetTexture(gpTextureMouse01);
	gpMouse->SetSize(0.35f, 0.35f);

	//手紙
	for (int i = 0; i < 3; i++)
	{
		latter[i] = new StaticObject();
		latter[i]->mSprite->SetTexture(gpTextureKey);
		latter[i]->mSprite->mCenter.y = 0.15f;
		latter[i]->SetSize(0.15f, 0.15f);
		latter[i]->mSprite->mCenter.x = (latter[i]->mSprite->mSize.x * i) + 0.6f;
		latter[i]->Activate(false);
	}
}

ResultScene::~ResultScene()
{
	delete dool;
	delete backGround;
	delete result;
	delete hosi;
	delete titleLogo;
	delete selectLogo;
	// マウス解放
	delete gpMouse;
	for (int i = 0; i < 3; i++)
	{
		delete latter[i];
	}

	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureMouse02);
	COM_SAFE_RELEASE(gpTextureHosi01);
	COM_SAFE_RELEASE(gpTextureResult01);
	COM_SAFE_RELEASE(gpTextureDool01);
	COM_SAFE_RELEASE(gpTextureBackGround);
	COM_SAFE_RELEASE(gpTexturetitleGoLogo);
	COM_SAFE_RELEASE(gpTextureSelectGoLogo);

}

void ResultScene::Update()
{
	//ここに処理を書く
	dool->Update();
	hosi->Update();
	result->Update();
	backGround->Update();
	titleLogo->Update();
	selectLogo->Update();

	//手紙
	for (int i = 0; i < 3; i++)
	{		
		latter[i]->Update();
	}

	//マウスクリックしたら音を鳴らす
	if (Input_GetKeyTrigger(VK_LBUTTON))
	{
		XA_Play(SOUND_LABEL_SE001,0.25f);
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

	if (gpMouse->mHitCircle->IsHit(titleLogo->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE007, 0.5f);
			//リザルトに変える
			sceneNo = 1;
		}
	}

	if (gpMouse->mHitCircle->IsHit(selectLogo->mHitCircle) == true)
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

	//ドールが0以上なら
	if (latterCount > 0)
	{
		switch (latterCount)
		{
		case 1:
			latter[0]->Activate(true);
			break;
		case 2:
			latter[0]->Activate(true);
			latter[1]->Activate(true);
			break;
		case 3:
			latter[0]->Activate(true);
			latter[1]->Activate(true);
			latter[2]->Activate(true);
			break;
		default:
			break;
		}
	}
}

void ResultScene::Draw()
{
	backGround->Draw();
	result->Draw();
	hosi->Draw();
	dool->Draw();
	titleLogo->Draw();
	selectLogo->Draw();

	//手紙
	for (int i = 0; i < 3; i++)
	{		
		latter[i]->Draw();
	}

	gpMouse->Draw();
}
