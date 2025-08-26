#include "GameSelectScene.h"

//switch用識別番号
extern int sceneNo;

GameSelectScene::GameSelectScene()
{
	LoadTexture(L"assets/Select_BG.png", &gpTextureBackGround);

	LoadTexture(L"assets/stage_1-1.png", &gpTextureSelect01);
	LoadTexture(L"assets/stage_1-2.png", &gpTextureSelect02);
	LoadTexture(L"assets/stage_1-3.png", &gpTextureSelect03);

	LoadTexture(L"assets/StageSelectUI.png", &gpTextureSelect04);

	LoadTexture(L"assets/yazirusi.png", &gpTextureStageSelectMove01);
	LoadTexture(L"assets/yazirusi_hidari.png", &gpTextureStageSelectMove02);

	//背景
	backGround = new StaticObject();
	backGround->mSprite->SetTexture(gpTextureBackGround);
	backGround->mSprite->mSize.x = 2.8f;
	backGround->mSprite->mSize.y = 1.5f;

	//⋆
	select = new AnimHitObject();
	select->mSprite->SetTexture(gpTextureSelect01);
	select->mSprite->mCenter.x = -0.8f;
	select->mSprite->mCenter.y = -0.1f;
	select->SetSize(0.75f, 0.95f);
	select->mHitCircle->mHankei = 0.25f;

	select2 = new AnimHitObject();
	select2->mSprite->SetTexture(gpTextureSelect02);
	select2->mSprite->mCenter.x = 0.0f;
	select2->mSprite->mCenter.y = -0.1f;
	select2->SetSize(0.75f, 0.95f);
	select2->mHitCircle->mHankei = 0.25f;

	select3 = new AnimHitObject();
	select3->mSprite->SetTexture(gpTextureSelect03);
	select3->mSprite->mCenter.x = 0.8f;
	select3->mSprite->mCenter.y = -0.1f;
	select3->SetSize(0.75f, 0.95f);
	select3->mHitCircle->mHankei = 0.25f;

	select4 = new AnimHitObject();
	select4->mSprite->SetTexture(gpTextureSelect04);
	select4->mSprite->mCenter.x = -0.85f;
	select4->mSprite->mCenter.y = 0.55f;
	select4->SetSize(1.0f, 0.4f);

	//gameselect2へ移動用
	selectMoveR = new AnimHitObject();
	selectMoveR->mSprite->SetTexture(gpTextureStageSelectMove01);
	selectMoveR->mSprite->mCenter.x = 1.25f;
	selectMoveR->mSprite->mCenter.y = -0.15f;
	selectMoveR->SetSize(0.1f, 0.15f);
	selectMoveR->mHitCircle->mHankei = 0.01f;

	selectMoveL = new AnimHitObject();
	selectMoveL->mSprite->SetTexture(gpTextureStageSelectMove02);
	selectMoveL->mSprite->mCenter.x = -1.25f;
	selectMoveL->mSprite->mCenter.y = -0.15f;
	selectMoveL->SetSize(0.1f, 0.15f);
	selectMoveL->mHitCircle->mHankei = 0.01f;

	//マウス画像読み込み
	LoadTexture(L"assets/MouseN.png", &gpTextureMouse01);
	LoadTexture(L"assets/MouseY.png", &gpTextureMouse02);

	//マウス実体化
	gpMouse = new Operation();
	gpMouse->mSprite->SetTexture(gpTextureMouse01);
	gpMouse->SetSize(0.35f, 0.35f);

	// カーソルの位置をセットする
	//SetCursorPos(0.0f, 0.0f);
}

GameSelectScene::~GameSelectScene()
{
	// マウス解放
	delete gpMouse;
	delete backGround;
	delete select;
	delete select2;
	delete select3;
	delete select4;
	delete selectMoveR;
	delete selectMoveL;
	//delete StageSelect;

	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureMouse02);
	COM_SAFE_RELEASE(gpTextureSelect01);
	COM_SAFE_RELEASE(gpTextureSelect02);
	COM_SAFE_RELEASE(gpTextureSelect03);
	COM_SAFE_RELEASE(gpTextureSelect04);
	COM_SAFE_RELEASE(gpTextureStageSelectMove01);
	COM_SAFE_RELEASE(gpTextureStageSelectMove02);
	COM_SAFE_RELEASE(gpTextureBackGround);
}
void GameSelectScene::Update()
{
	//ここに処理を書く
	select->Update();
	select2->Update();
	select3->Update();
	select4->Update();
	selectMoveR->Update();
	selectMoveL->Update();
	backGround->Update();

	//stageSelect->Update();

	//マウスクリックしたら音を鳴らす
	if (Input_GetKeyTrigger(VK_LBUTTON))
	{
		XA_Play(SOUND_LABEL_SE001,0.25f);
	}

	// マウスの画像差し替え
	if (Input_GetKeyDown(VK_LBUTTON))
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse02);
	}
	else
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse01);
	}

	gpMouse->Update();

#ifdef _DEBUG
	//ここに処理を書く
	if (Input_GetKeyTrigger(VK_RETURN))
	{
		//リザルトに変える
		sceneNo = 3;
	}
#endif // _DEBUG

	if (gpMouse->mHitCircle->IsHit(select->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE007, 0.35f);
			//リザルトに変える
			sceneNo = 6;
		}
	}

	if (gpMouse->mHitCircle->IsHit(select2->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE007, 0.35f);
			//リザルトに変える
			sceneNo = 7;
		}
	}

	if (gpMouse->mHitCircle->IsHit(select3->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE007, 0.35f);
			//リザルトに変える
			sceneNo = 8;
		}
	}

	if (gpMouse->mHitCircle->IsHit(selectMoveR->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE009, 0.8f);
			//リザルトに変える
			sceneNo = 9;
		}
	}

	if (gpMouse->mHitCircle->IsHit(selectMoveL->mHitCircle) == true)
	{
		//ここに処理を書く
		if (Input_GetKeyTrigger(VK_LBUTTON))
		{
			XA_Stop(SOUND_LABEL_SE001);
			XA_Play(SOUND_LABEL_SE009, 0.8f);
			//リザルトに変える
			sceneNo = 13;
		}
	}
}

void GameSelectScene::Draw()
{
	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	//stageSelect->Draw();
	backGround->Draw();
	select->Draw();
	select2->Draw();
	select3->Draw();
	select4->Draw();
	selectMoveR->Draw();
	selectMoveL->Draw();
	gpMouse->Draw();
}