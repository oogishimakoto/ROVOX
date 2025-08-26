#include "RetryScene.h"

//switch用識別番号
extern int sceneNo;

extern int oldsceneNo;

RetryScene::RetryScene()
{
	//マウス画像読み込み
	LoadTexture(L"assets/MouseN.png", &gpTextureMouse01);
	LoadTexture(L"assets/MouseY.png", &gpTextureMouse02);

	//マウス実体化
	gpMouse = new Operation();
	gpMouse->mSprite->SetTexture(gpTextureMouse01);
	gpMouse->SetSize(0.35f, 0.35f);

	// リザルトテクスチャ
	LoadTexture(L"assets/Now_loading/nowloading.png", &gpTextureRetryBg);
	ReyryBg = new StaticObject();
	ReyryBg->mSprite->SetTexture(gpTextureRetryBg);
	ReyryBg->mSprite->mCenter.x = 0.0f;
	ReyryBg->mSprite->mCenter.y = 0.0f;
	ReyryBg->SetSize(2.66f, 1.5f);

	LoadTexture(L"assets/Now_loading/walk_1.png", &gpTextureDoll_bg[0]);
	LoadTexture(L"assets/Now_loading/walk_2.png", &gpTextureDoll_bg[1]);
	LoadTexture(L"assets/Now_loading/walk_3.png", &gpTextureDoll_bg[2]);
	LoadTexture(L"assets/Now_loading/walk_4.png", &gpTextureDoll_bg[3]);
	LoadTexture(L"assets/Now_loading/walk_5.png", &gpTextureDoll_bg[4]);
	LoadTexture(L"assets/Now_loading/walk_6.png", &gpTextureDoll_bg[5]);
	LoadTexture(L"assets/Now_loading/walk_7.png", &gpTextureDoll_bg[6]);
	LoadTexture(L"assets/Now_loading/walk_8.png", &gpTextureDoll_bg[7]);
	Doll_Bg = new StaticObject();
	Doll_Bg->mSprite->SetTexture(gpTextureDoll_bg[0]);
	Doll_Bg->mSprite->mCenter.x = 0.72f;
	Doll_Bg->mSprite->mCenter.y = -0.35f;
	Doll_Bg->SetSize(0.75f, 0.75f);

	mTime = 0.0f;

	//移動用アニメーション変数
	//移動時のアニメーションの切り替え番号
	AnimCoount = 0;

	//アニメーション切り替え速度
	AnimSpeed = 1.4f;

	//移動用アニメーション変数
	//移動時のアニメーションの切り替え番号
	AnimCoountValues = 0.0f;
}

RetryScene::~RetryScene()
{
	// マウス解放
	delete gpMouse;

	// ドール背景解放
	delete Doll_Bg;

	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureMouse02);

	COM_SAFE_RELEASE(gpTextureRetryBg);

	for(int i = 0; i < MAX_DOLL_BG; i++)
		COM_SAFE_RELEASE(gpTextureDoll_bg[i]);
}

void RetryScene::Update()
{
	mTime++;
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

	//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
	AnimCoountValues += AnimSpeed;

	if (AnimCoountValues > ANIMATIONVALUES)
	{
		AnimCoount++;

		if (AnimCoount == 0)
		{
			Doll_Bg->mSprite->mCenter.x = 0.72f;
		}
		if (AnimCoount == 1)
		{
			Doll_Bg->mSprite->mCenter.x = 0.48f;
		}
		if (AnimCoount == 2)
		{
			Doll_Bg->mSprite->mCenter.x = 0.24f;
		}
		if (AnimCoount == 3)
		{
			Doll_Bg->mSprite->mCenter.x = 0.0f;
		}
		if (AnimCoount == 4)
		{
			Doll_Bg->mSprite->mCenter.x = -0.24f;
		}
		if (AnimCoount == 5)
		{
			Doll_Bg->mSprite->mCenter.x = -0.48f;
		}
		if (AnimCoount == 6)
		{
			Doll_Bg->mSprite->mCenter.x = -0.72f;
		}
		if (AnimCoount == 7)
		{
			Doll_Bg->mSprite->mCenter.x = -0.96f;
		}
		if (AnimCoount == 8)
		{
			AnimCoount = 0;
			sceneNo = oldsceneNo;
		}
		AnimCoountValues = 0;

	}
	Doll_Bg->mSprite->SetTexture(gpTextureDoll_bg[AnimCoount]);

	ReyryBg->Update();
	Doll_Bg->Update();

	gpMouse->Update();
}

void RetryScene::Draw()
{
	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	ReyryBg->Draw();
	Doll_Bg->Draw();
	gpMouse->Draw();
}
