#include "Stage1_2.h"

extern bool clickCheck;
extern bool clickCheckAfter;

extern int latterCount;

//ドールが歩いてる画像8枚
extern ID3D11ShaderResourceView* gpTextureDoolWalk[MAX_GP];
//ジャンプの画像8枚
extern ID3D11ShaderResourceView* gpTextureDoolJump[MAX_GP];
//ドールが歩いてる画像8枚
extern ID3D11ShaderResourceView* RgpTextureDoolWalk[MAX_GP];
//ジャンプの画像8枚
extern ID3D11ShaderResourceView* RgpTextureDoolJump[MAX_GP];
//押してる画像
extern ID3D11ShaderResourceView* gpTextureDoolPushu[MAX_GP];
//梯子登る画像
extern ID3D11ShaderResourceView* gpTextureDoolNoboru[MAX_GP];

//歩く画像
extern ID3D11ShaderResourceView* gpTextureGearUI01;

//はしご画像
extern ID3D11ShaderResourceView* gpTextureGearUI02;

//ジャンプ画像
extern ID3D11ShaderResourceView* gpTextureGearUI03;

//リセット
extern ID3D11ShaderResourceView* gpTextureGearUI04;

//手紙画像
extern ID3D11ShaderResourceView* gpTextureGearUI05;

//会話画像
extern ID3D11ShaderResourceView* gpTextureDoolTalk[MAX_GP];

extern ID3D11ShaderResourceView* gpTextureMapTipWall;		//マップチップ用画像壁
extern ID3D11ShaderResourceView* gpTextureMapTipHasigo;	//マップチップ用画像梯子

extern ID3D11ShaderResourceView* gpTextureFe01;			//フェードアウト画像01のテクスチャ

extern ID3D11ShaderResourceView* gpTextureBox;				//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureBox2;				//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureBox3;				//箱の画像のテクスチャ

extern ID3D11ShaderResourceView* gpTextureKey;				//箱の画像のテクスチャ

extern ID3D11ShaderResourceView* gpTextureGoal;				//ゴール画像のテクスチャ

extern ID3D11ShaderResourceView* gpTextureDoolFallRight;	//右向きにこけるドール画像01のテクスチャ

extern ID3D11ShaderResourceView* gpTextureGear01;			//ギア画像01のテクスチャ

extern ID3D11ShaderResourceView* gpTextureGoldGear01;		//ギア画像01のテクスチャ(金色
extern ID3D11ShaderResourceView* gpTextureGoldGear02;		//ギア画像02のテクスチャ(金色)
extern ID3D11ShaderResourceView* gpTextureGoldGear03;		//ギア画像03のテクスチャ(金色)
extern ID3D11ShaderResourceView* gpTextureGoldGear04;		//ギア画像02のテクスチャ(金色)
extern ID3D11ShaderResourceView* gpTextureGoldGear05;		//ギア画像03のテクスチャ(金色)

extern DWORD gDeltaTime;

//当たるたびに向きを変える
extern bool mapHitChange;

Stage1_2::Stage1_2()
{
	mapHitChange = true;
	latterCount = 0;

	//ドール画像読み込み
	LoadTexture(L"assets/dool_idle.png", &gpTextureDool01);

	//ドールの歩く画像読み込み
	LoadTexture(L"assets/walk_1.png", &gpTextureDoolWalk[0]);
	LoadTexture(L"assets/walk_2.png", &gpTextureDoolWalk[1]);
	LoadTexture(L"assets/walk_3.png", &gpTextureDoolWalk[2]);
	LoadTexture(L"assets/walk_4.png", &gpTextureDoolWalk[3]);
	LoadTexture(L"assets/walk_5.png", &gpTextureDoolWalk[4]);
	LoadTexture(L"assets/walk_06.png", &gpTextureDoolWalk[5]);
	LoadTexture(L"assets/walk_7.png", &gpTextureDoolWalk[6]);
	LoadTexture(L"assets/walk_8.png", &gpTextureDoolWalk[7]);

	//ドールのジャンプ画像読み込み
	LoadTexture(L"assets/j1.png", &gpTextureDoolJump[0]);
	LoadTexture(L"assets/j2.png", &gpTextureDoolJump[1]);
	LoadTexture(L"assets/j3.png", &gpTextureDoolJump[2]);
	LoadTexture(L"assets/j4.png", &gpTextureDoolJump[3]);
	LoadTexture(L"assets/j5.png", &gpTextureDoolJump[4]);
	LoadTexture(L"assets/j6.png", &gpTextureDoolJump[5]);
	LoadTexture(L"assets/j7.png", &gpTextureDoolJump[6]);
	LoadTexture(L"assets/j8.png", &gpTextureDoolJump[7]);

	//ドールの歩く画像読み込み
	LoadTexture(L"assets/Rwalk_1.png", &RgpTextureDoolWalk[0]);
	LoadTexture(L"assets/Rwalk_2.png", &RgpTextureDoolWalk[1]);
	LoadTexture(L"assets/Rwalk_3.png", &RgpTextureDoolWalk[2]);
	LoadTexture(L"assets/Rwalk_4.png", &RgpTextureDoolWalk[3]);
	LoadTexture(L"assets/Rwalk_5.png", &RgpTextureDoolWalk[4]);
	LoadTexture(L"assets/Rwalk_06.png", &RgpTextureDoolWalk[5]);
	LoadTexture(L"assets/Rwalk_7.png", &RgpTextureDoolWalk[6]);
	LoadTexture(L"assets/Rwalk_8.png", &RgpTextureDoolWalk[7]);

	//ドールのジャンプ画像読み込み
	LoadTexture(L"assets/Rj1.png", &RgpTextureDoolJump[0]);
	LoadTexture(L"assets/Rj2.png", &RgpTextureDoolJump[1]);
	LoadTexture(L"assets/Rj3.png", &RgpTextureDoolJump[2]);
	LoadTexture(L"assets/Rj4.png", &RgpTextureDoolJump[3]);
	LoadTexture(L"assets/Rj5.png", &RgpTextureDoolJump[4]);
	LoadTexture(L"assets/Rj6.png", &RgpTextureDoolJump[5]);
	LoadTexture(L"assets/Rj7.png", &RgpTextureDoolJump[6]);
	LoadTexture(L"assets/Rj8.png", &RgpTextureDoolJump[7]);

	//ドールの押してる画像読み込み
	LoadTexture(L"assets/osu_1.png", &gpTextureDoolPushu[0]);
	LoadTexture(L"assets/osu_2.png", &gpTextureDoolPushu[1]);
	LoadTexture(L"assets/osu_3.png", &gpTextureDoolPushu[2]);
	LoadTexture(L"assets/osu_4.png", &gpTextureDoolPushu[3]);
	LoadTexture(L"assets/osu_5.png", &gpTextureDoolPushu[4]);
	LoadTexture(L"assets/osu_6.png", &gpTextureDoolPushu[5]);
	LoadTexture(L"assets/osu_7.png", &gpTextureDoolPushu[6]);
	LoadTexture(L"assets/osu_8.png", &gpTextureDoolPushu[7]);

	//ドールの梯子登る画像読み込み
	LoadTexture(L"assets/ladder_1.png", &gpTextureDoolNoboru[0]);
	LoadTexture(L"assets/ladder_2.png", &gpTextureDoolNoboru[1]);
	LoadTexture(L"assets/ladder_3.png", &gpTextureDoolNoboru[2]);
	LoadTexture(L"assets/ladder_4.png", &gpTextureDoolNoboru[3]);
	LoadTexture(L"assets/ladder_5.png", &gpTextureDoolNoboru[4]);
	LoadTexture(L"assets/ladder_6.png", &gpTextureDoolNoboru[5]);
	LoadTexture(L"assets/ladder_7.png", &gpTextureDoolNoboru[6]);
	LoadTexture(L"assets/ladder_8.png", &gpTextureDoolNoboru[7]);

	//ドールの梯子登る画像読み込み
	LoadTexture(L"assets/Sentence1.png", &gpTextureDoolTalk[0]);
	LoadTexture(L"assets/Sentence2.png", &gpTextureDoolTalk[1]);
	LoadTexture(L"assets/Sentence3.png", &gpTextureDoolTalk[2]);
	LoadTexture(L"assets/Sentence4.png", &gpTextureDoolTalk[3]);
	LoadTexture(L"assets/Sentence5.png", &gpTextureDoolTalk[4]);
	LoadTexture(L"assets/Sentence6.png", &gpTextureDoolTalk[5]);
	LoadTexture(L"assets/Sentence7.png", &gpTextureDoolTalk[6]);
	LoadTexture(L"assets/Sentence9.png", &gpTextureDoolTalk[7]);

	//gearUI
	LoadTexture(L"assets/UI_Name_aruku.png", &gpTextureGearUI01);
	LoadTexture(L"assets/UI_Name_hasigo.png", &gpTextureGearUI02);
	LoadTexture(L"assets/UI_Name_janpu.png", &gpTextureGearUI03);
	LoadTexture(L"assets/UI_Name_risetto.png", &gpTextureGearUI04);
	LoadTexture(L"assets/UI_Name_tegami.png", &gpTextureGearUI05);

	//壁用画像読み込み
	LoadTexture(L"assets/murasaki.png", &gpTextureMapTipWall);

	//梯子画像読み込み
	LoadTexture(L"assets/hasigo.png", &gpTextureMapTipHasigo);

	//右向きにこけるドール画像読み込み
	LoadTexture(L"assets/dool_kokeru_R.png", &gpTextureDoolFallRight);

	//ド左向きにこけるール画像読み込み
	LoadTexture(L"assets/dool_kokeru_L.png", &gpTextureDoolFallLeft);

	//石画像読み込み
	LoadTexture(L"assets/stone.png", &gpTextureStone);

	//背景画像読み込み
	LoadTexture(L"assets/BackGround.jpg", &gpTextureBackGround);

	//ギア画像読み込み(金色)		
	LoadTexture(L"assets/walk_gear.png", &gpTextureGoldGear01);

	LoadTexture(L"assets/jump_gear.png", &gpTextureGoldGear02);

	LoadTexture(L"assets/ladder_gear.png", &gpTextureGoldGear03);


	LoadTexture(L"assets/memo_gear.png", &gpTextureGoldGear04);

	LoadTexture(L"assets/tegami_gear.png", &gpTextureGoldGear05);
	//ギア画像読み込み
	LoadTexture(L"assets/gear_walk.png", &gpTextureGear01);

	LoadTexture(L"assets/gear_jump.png", &gpTextureGear02);

	LoadTexture(L"assets/gear_up.png", &gpTextureGear03);

	//ステージギア
	LoadTexture(L"assets/gear6.png", &gpTextureGear04);
	LoadTexture(L"assets/gear2.png", &gpTextureGear05);
	//ギア画像読み込み
	LoadTexture(L"assets/UI_Thinking.png", &gpTextureUI01);
	LoadTexture(L"assets/UI_MemoryGear.png", &gpTextureUI02);
	LoadTexture(L"assets/UI_Item.png", &gpTextureUI03);
	LoadTexture(L"assets/UI_slot.png", &gpTextureUI04);
	LoadTexture(L"assets/UI_Reverse.png", &gpTextureUI05);
	LoadTexture(L"assets/UI_Reverse_left2.png", &gpTextureUI06);
	LoadTexture(L"assets/UI_Reverse_right2 .png", &gpTextureUI07);

	//フェード
	LoadTexture(L"assets/white.png", &gpTextureFe01);

	//goal
	LoadTexture(L"assets/goal.png", &gpTextureGoal);

	//箱
	LoadTexture(L"assets/Box.png", &gpTextureBox);

	LoadTexture(L"assets/IMG_0624.png", &gpTextureBox2);

	LoadTexture(L"assets/IMG_0616.png", &gpTextureBox3);

	//鍵	
	LoadTexture(L"assets/Key.png", &gpTextureKey);

	//ドール実体化
	dool = new Dool();
	dool->mSprite->SetTexture(gpTextureDool01);
	dool->mSprite->mCenter.x = -0.8f;
	/*dool->mSprite->mCenter.x = 0.5f;*/
	dool->mSprite->mCenter.y = 1.0f;
	dool->SetSize(0.2f, 0.2f);

	//ギア生成クラス生成
	gj = new GearJanereter({ 4,0 });
	gj->mSprite->SetTexture(gpTextureGoldGear01);
	gj->mSprite->mCenter.x = 0.9f;
	gj->mSprite->mCenter.y = -0.3f;
	gj->SetSize(0.2f, 0.2f);
	gj->mHitCircle->mHankei = 0.05f;

	// ジャンプギア
	jumpGear = new GearJanereter({ 2,0 });
	jumpGear->mSprite->SetTexture(gpTextureGoldGear02);
	jumpGear->mSprite->mCenter.x = 1.05f;
	jumpGear->mSprite->mCenter.y = -0.465f;
	jumpGear->SetSize(0.2f, 0.2f);
	jumpGear->mHitCircle->mHankei = 0.05f;

	// つかむギア
	catchiGear = new GearJanereter({ 3,0 });
	catchiGear->mSprite->SetTexture(gpTextureGoldGear05);
	catchiGear->mSprite->mCenter.x = 0.9f;
	catchiGear->mSprite->mCenter.y = -0.63f;
	catchiGear->SetSize(0.2f, 0.2f);
	catchiGear->mHitCircle->mHankei = 0.05f;

	// 登るギア
	upGear = new GearJanereter({ 1,0 });
	upGear->mSprite->SetTexture(gpTextureGoldGear03);
	upGear->mSprite->mCenter.x = 1.2f;
	upGear->mSprite->mCenter.y = -0.3f;
	upGear->SetSize(0.2f, 0.2f);
	upGear->mHitCircle->mHankei = 0.05f;


	//空のギア
	emptiGear = new GearJanereter({ 1,0 });
	emptiGear->mSprite->SetTexture(gpTextureGoldGear04);
	emptiGear->mSprite->mCenter.x = 1.2f;
	emptiGear->mSprite->mCenter.y = -0.63f;
	emptiGear->SetSize(0.2f, 0.2f);
	emptiGear->mHitCircle->mHankei = 0.05f;

	gj->SetDoRotate(false);
	jumpGear->SetDoRotate(false);
	catchiGear->SetDoRotate(false);
	upGear->SetDoRotate(false);
	emptiGear->SetDoRotate(false);

	//ステージギア右に動かす
	StageGearX = new GearJanereter({ 0,0 });
	StageGearX->mSprite->SetTexture(gpTextureGear05);
	StageGearX->SetPosition(0.9f, 0.08f);
	StageGearX->SetSize(0.2f, 0.2f);
	StageGearX->mHitCircle->mHankei = 0.001f;

	//StageGearX->Activate(false);

	//ステージギア上に動かす
	StageGearY = new GearJanereter({ 0,0 });
	StageGearY->mSprite->SetTexture(gpTextureGear04);
	StageGearY->SetPosition(1.15f, 0.08f);
	StageGearY->SetSize(0.2f, 0.2f);
	StageGearY->mHitCircle->mHankei = 0.01f;

	//ステージギアをはめる用
	StageX = new HitObject();
	StageX->mSprite->SetTexture(gpTextureGear05);
	StageX->SetSize(0.2f, 0.2f);

	StageY = new HitObject();
	StageY->mSprite->SetTexture(gpTextureGear04);
	StageY->SetSize(0.2f, 0.2f);

	StageX2 = new HitObject();
	StageX2->mSprite->SetTexture(gpTextureGear05);
	StageX2->SetSize(0.2f, 0.2f);


	//生成したギアの画像を設定する
		//生成したギアの画像を設定する
	for (int i = 0; i < 75; i++)
	{
		gj->mGear[i]->mSprite->SetTexture(gpTextureGoldGear01);
		jumpGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear02);
		catchiGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear05);
		upGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear03);
		StageGearX->mGear[i]->mSprite->SetTexture(gpTextureGear05);
		StageGearY->mGear[i]->mSprite->SetTexture(gpTextureGear04);
		emptiGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear04);
	}

	//ドールの管理クラス生成
	doolMg = new DoolManager();
	doolMg->mSprite->SetTexture(gpTextureGear01);
	doolMg->mSprite->mCenter.x = 0.95f;
	doolMg->mSprite->mCenter.y = 0.5f;
	doolMg->SetSize(0.6f, 0.4f);
	doolMg->mHitCircle->mHankei = 0.15f;

	////ゴールクラス生成
	//goal = new Goal();
	//goal->mSprite->SetTexture(gpTextureGoal);
	//goal->mSprite->mCenter.x = 0.3f;
	//goal->mSprite->mCenter.y = -0.2f;

	//石クラス
	stone = new AStone();
	stone->mSprite->SetTexture(gpTextureStone);
	stone->mSprite->mCenter.x = -0.5f;
	stone->mSprite->mCenter.y = 0.0f;
	stone->active = false;

	//背景
	for (int i = 0; i < 2; i++)
	{
		background[i] = new StaticObject();
		background[i]->mSprite->SetTexture(gpTextureBackGround);
		background[i]->mSprite->mSize.x = 2.675f;
		background[i]->mSprite->mSize.y = 1.36f;
		background[i]->mSprite->mCenter.y = 0.074f;
		background[i]->mSprite->mCenter.x = i * 2;
	}

	//UI
		//UI
	UI01 = new HitObject();
	UI01->mSprite->SetTexture(gpTextureUI01);
	UI01->mSprite->mCenter.x = -0.34f;
	UI01->mSprite->mCenter.y = -0.54f;
	UI01->mSprite->mSize.x = 1.99f;
	UI01->mSprite->mSize.y = 0.42f;

	UI02 = new StaticObject();
	UI02->mSprite->SetTexture(gpTextureUI02);
	UI02->mSprite->mCenter.x = 1.0f;
	UI02->mSprite->mCenter.y = -0.4f;
	UI02->SetSize(0.7f, 0.7f);

	UI03 = new StaticObject();
	UI03->mSprite->SetTexture(gpTextureUI03);
	UI03->mSprite->mCenter.x = 1.0f;
	UI03->mSprite->mCenter.y = 0.13f;
	UI03->mSprite->mSize.x = 0.7f;
	UI03->mSprite->mSize.y = 0.37f;


	UI04 = new StaticObject();
	UI04->mSprite->SetTexture(gpTextureUI04);
	UI04->mSprite->mCenter.x = 1.0f;
	UI04->mSprite->mCenter.y = 0.53f;
	UI04->mSprite->mSize.x = 0.7f;
	UI04->mSprite->mSize.y = 0.45f;

	UI05 = new HitObject();
	UI05->mSprite->SetTexture(gpTextureUI06);
	UI05->mSprite->mCenter.x = 0.49f;
	UI05->mSprite->mCenter.y = 0.63f;
	UI05->mSprite->mSize.x = 0.22f;
	UI05->mSprite->mSize.y = 0.08f;
	UI05->mHitCircle->mHankei = 0.05f;

	UI06 = new StaticObject();
	UI06->mSprite->SetTexture(gpTextureUI05);
	UI06->mSprite->mCenter.x = 0.35f;
	UI06->mSprite->mCenter.y = 0.55f;
	UI06->mSprite->mSize.x = 0.6f;
	UI06->mSprite->mSize.y = 0.4f;

	//マップチップファイル読み込み
	maptip = new MaptipFileRead("assets/Map_CSV/Stage1-2.csv");
	//maptip = new MaptipFileRead("1-1.csv");
	maptip->mapObjSet();

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
		latter[i]->mSprite->mCenter.y = 0.68f;
		latter[i]->SetSize(0.15f, 0.15f);
		latter[i]->mSprite->mCenter.x = (latter[i]->mSprite->mSize.x * i) + -1.0f;
		latter[i]->Activate(false);
	}

	//鍵実体化
	//key = new Key();
	//key->mSprite->SetTexture(gpTextureKey);
	//key->mSprite->mCenter.x = 0.0f;
	//key->mSprite->mCenter.y = -0.2f;
	//key->SetSize(0.25f, 0.25f);

	// フェード
	gpFade = new Fade();

	//ハンコ押す画像
	stanp = new StaticObject();
	stanp->mSprite->SetTexture(gpTextureGear01);
	stanp->SetSize(0.25f, 0.25f);

	//gearはまった時用画像
	for (int i = 0; i < 3; i++)
	{
		Gpgear[i] = new RotateObject();
		Gpgear[i]->mSprite->SetTexture(gpTextureGear01);
		Gpgear[i]->mSprite->mCenter.x = 0.89f + (i * 0.1415f);
		Gpgear[i]->mSprite->mCenter.y = 0.53f;
		Gpgear[i]->SetSize(0.13f, 0.13f);
		Gpgear[i]->Activate(false);
	}

	// カーソルの位置をセットする
	SetCursorPos(1650.0f, 720.0f);

	LoadTexture(L"assets/StopUI.png", &gpTextureStop);	// ポーズUI
	LoadTexture(L"assets/UI_Pose_BG.png", &gpTexturepauseMenu);	//メニュー文字

	LoadTexture(L"assets/Stresult.png", &gpTexturepause);		// ポーズ背景
	LoadTexture(L"assets/UI1gemepose.png", &gpTexturepauseUI);		// ポーズUI
	LoadTexture(L"assets/UI1geme.png", &gpTexturepauseGame);	// ポーズゲーム
	LoadTexture(L"assets/UI1retry.png", &gpTexturepauseRetry);	// ポーズリトライ
	LoadTexture(L"assets/UI1title.png", &gpTexturepauseTitle);	// ポーズタイトル
	LoadTexture(L"assets/UI1select.png", &gpTexturepauseStect);	// ポーズセレクト

	// ポーズUI
	Stoppause = new HitObject();
	Stoppause->mSprite->SetTexture(gpTextureStop);
	Stoppause->mSprite->mCenter.x = -1.2f;
	Stoppause->mSprite->mCenter.y = 0.625f;
	Stoppause->mSprite->mSize.x = 0.25f;
	Stoppause->mSprite->mSize.y = 0.25f;

	// pause背景
	pauseBg = new StaticObject();
	pauseBg->mSprite->SetTexture(gpTexturepause);
	pauseBg->mSprite->mCenter.x = 0.0f;
	pauseBg->mSprite->mCenter.y = 0.0f;
	pauseBg->mSprite->mSize.x = 2.67f;
	pauseBg->mSprite->mSize.y = 1.5f;
	// pauseUI
	pauseUI = new StaticObject();
	pauseUI->mSprite->SetTexture(gpTexturepauseMenu);
	pauseUI->mSprite->mCenter.x = 0.0f;
	pauseUI->mSprite->mCenter.y = 0.0f;
	pauseUI->mSprite->mSize.x = 2.67f;
	pauseUI->mSprite->mSize.y = 1.5f;
	// pauseUIGame
	pauseGa = new HitObject();
	pauseGa->mSprite->SetTexture(gpTexturepauseGame);
	pauseGa->mSprite->mCenter.x = 0.45f;
	pauseGa->mSprite->mCenter.y = 0.45f;
	pauseGa->mSprite->mSize.x = 1.0f;
	pauseGa->mSprite->mSize.y = 0.2f;
	// pauseUIRetry
	pauseRe = new HitObject();
	pauseRe->mSprite->SetTexture(gpTexturepauseRetry);
	pauseRe->mSprite->mCenter.x = 0.45f;
	pauseRe->mSprite->mCenter.y = 0.15f;
	pauseRe->mSprite->mSize.x = 1.0f;
	pauseRe->mSprite->mSize.y = 0.2f;
	// pauseUITitle
	pauseTi = new HitObject();
	pauseTi->mSprite->SetTexture(gpTexturepauseTitle);
	pauseTi->mSprite->mCenter.x = 0.45f;
	pauseTi->mSprite->mCenter.y = -0.15f;
	pauseTi->mSprite->mSize.x = 1.0f;
	pauseTi->mSprite->mSize.y = 0.2f;
	// pauseUIStageSelect
	pauseSt = new HitObject();
	pauseSt->mSprite->SetTexture(gpTexturepauseStect);
	pauseSt->mSprite->mCenter.x = 0.45f;
	pauseSt->mSprite->mCenter.y = -0.45f;
	pauseSt->mSprite->mSize.x = 1.0f;
	pauseSt->mSprite->mSize.y = 0.2f;
	// ポーズメニュー初期化
	pausemenu = false;
	//位置調整
	GearUI01 = new StaticObject();
	GearUI01->mSprite->SetTexture(gpTextureGearUI01);
	GearUI01->mSprite->mCenter.x = 0.9f;
	GearUI01->mSprite->mCenter.y = -0.15f;
	GearUI01->mSprite->mSize.x = 0.2f;
	GearUI01->mSprite->mSize.y = 0.2f;

	GearUI02 = new StaticObject();
	GearUI02->mSprite->SetTexture(gpTextureGearUI02);
	GearUI02->mSprite->mCenter.x = 1.2f;
	GearUI02->mSprite->mCenter.y = -0.15f;
	GearUI02->SetSize(0.2f, 0.2f);

	GearUI03 = new StaticObject();
	GearUI03->mSprite->SetTexture(gpTextureGearUI03);
	GearUI03->mSprite->mCenter.x = 1.05f;
	GearUI03->mSprite->mCenter.y = -0.315f;
	GearUI03->SetSize(0.2f, 0.2f);

	GearUI04 = new StaticObject();
	GearUI04->mSprite->SetTexture(gpTextureGearUI04);
	GearUI04->mSprite->mCenter.x = 1.2f;
	GearUI04->mSprite->mCenter.y = -0.48f;
	GearUI04->SetSize(0.2f, 0.2f);

	GearUI05 = new StaticObject();
	GearUI05->mSprite->SetTexture(gpTextureGearUI05);
	GearUI05->mSprite->mCenter.x = 0.9f;
	GearUI05->mSprite->mCenter.y = -0.48f;
	GearUI05->SetSize(0.2f, 0.2f);
}

Stage1_2::~Stage1_2()
{
	// 機械人形
	delete dool;
	// ギア生成
	delete gj;
	delete jumpGear;
	delete catchiGear;
	delete upGear;
	delete emptiGear;
	// ドール
	delete doolMg;
	// ゴール
	//delete goal;
	// 石
	delete stone;
	// 背景
	for (int i = 0; i < 2; i++)
	{
		delete background[i];
	}
	// マップチップ
	delete UI01;
	delete UI02;
	delete UI03;
	delete UI04;
	delete UI05;
	delete UI06;

	delete GearUI01;
	delete GearUI02;
	delete GearUI03;
	delete GearUI04;
	delete GearUI05;

	// マップチップ用
	delete maptip;
	// 鍵
	//delete key;
	// ギアのはまっている画像
	for (int i = 0; i < 3; i++)
	{
		delete Gpgear[i];
		delete latter[i];
	}
	// フェード
	delete gpFade;
	// マウス
	delete gpMouse;
	//ステージギア
	delete StageGearX;
	delete StageGearY;
	delete StageX;
	delete StageY;
	delete StageX2;

	// テクスチャの解放
	COM_SAFE_RELEASE(gpTextureMapTipWall);
	COM_SAFE_RELEASE(gpTextureMapTipHasigo);
	COM_SAFE_RELEASE(gpTextureGoldGear01);
	COM_SAFE_RELEASE(gpTextureGoldGear02);
	COM_SAFE_RELEASE(gpTextureGoldGear03);
	COM_SAFE_RELEASE(gpTextureGoldGear04);
	COM_SAFE_RELEASE(gpTextureGoldGear05);
	COM_SAFE_RELEASE(gpTextureGear01);
	COM_SAFE_RELEASE(gpTextureGear02);
	COM_SAFE_RELEASE(gpTextureGear03);
	COM_SAFE_RELEASE(gpTextureGear04);
	COM_SAFE_RELEASE(gpTextureGear05);
	COM_SAFE_RELEASE(gpTextureBackGround);
	COM_SAFE_RELEASE(gpTextureFe01);
	COM_SAFE_RELEASE(gpTextureUI01);
	COM_SAFE_RELEASE(gpTextureUI02);
	COM_SAFE_RELEASE(gpTextureUI03);
	COM_SAFE_RELEASE(gpTextureUI04);
	COM_SAFE_RELEASE(gpTextureUI05);
	COM_SAFE_RELEASE(gpTextureDoolFallRight);
	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureBox);
	COM_SAFE_RELEASE(gpTextureDool01);
	COM_SAFE_RELEASE(gpTextureDoolFallLeft);
	COM_SAFE_RELEASE(gpTextureStone);

	for (int i = 0; i < MAX_GP; i++)
	{
		COM_SAFE_RELEASE(gpTextureDoolWalk[i]);

		COM_SAFE_RELEASE(gpTextureDoolJump[i]);

		COM_SAFE_RELEASE(RgpTextureDoolWalk[i]);

		COM_SAFE_RELEASE(RgpTextureDoolJump[i]);

		COM_SAFE_RELEASE(gpTextureDoolPushu[i]);

		COM_SAFE_RELEASE(gpTextureDoolNoboru[i]);
	}

	COM_SAFE_RELEASE(gpTextureMouse02);

	// ポーズ系
	delete Stoppause;
	delete pauseBg;
	delete pauseUI;
	delete pauseGa;
	delete pauseRe;
	delete pauseTi;
	delete pauseSt;

	COM_SAFE_RELEASE(gpTextureStop);
	COM_SAFE_RELEASE(gpTexturepause);
	COM_SAFE_RELEASE(gpTexturepauseUI);
	COM_SAFE_RELEASE(gpTexturepauseGame);
	COM_SAFE_RELEASE(gpTexturepauseRetry);
	COM_SAFE_RELEASE(gpTexturepauseTitle);
	COM_SAFE_RELEASE(gpTexturepauseStect);
}

void Stage1_2::Update()
{
	// デルタタイムが想定外の値になった場合の処理
	if (gDeltaTime >= 100)
	{
		gDeltaTime = 0;
	}

	GearUI01->Activate(false);
	GearUI02->Activate(false);
	GearUI03->Activate(false);
	GearUI04->Activate(false);
	GearUI05->Activate(false);

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

	// ポーズメニューUIをクリックしたら
	if (gpMouse->mHitCircle->IsHit(Stoppause->mHitCircle) == true)
	{
		if (Input_GetKeyTrigger(VK_LBUTTON))
			pausemenu = true;	// ポーズメニューモードにする
	}

	if (pausemenu == true)
	{	// ポーズメニューなら

		// ゲームに戻る場合
		if (gpMouse->mHitCircle->IsHit(pauseGa->mHitCircle) == true)
		{
			if (Input_GetKeyTrigger(VK_LBUTTON))
				pausemenu = false;	// ゲームに戻る
		}
		// リトライする場合
		if (gpMouse->mHitCircle->IsHit(pauseRe->mHitCircle) == true)
		{
			if (Input_GetKeyTrigger(VK_LBUTTON))
			{
				oldsceneNo = 7;	// 現在のシーン番号
				sceneNo = 17;	// リトライシーンへ飛ぶ
			}
		}
		// タイトルに戻る場合
		if (gpMouse->mHitCircle->IsHit(pauseTi->mHitCircle) == true)
		{
			if (Input_GetKeyTrigger(VK_LBUTTON))
			{
				sceneNo = 1;	// リトライシーンへ飛ぶ
			}
		}
		// ステージ選択に戻る場合
		if (gpMouse->mHitCircle->IsHit(pauseSt->mHitCircle) == true)
		{
			if (Input_GetKeyTrigger(VK_LBUTTON))
			{
				sceneNo = 2;	// リトライシーンへ飛ぶ
			}
		}
		pauseBg->Update();
		pauseUI->Update();
		pauseGa->Update();
		pauseRe->Update();
		pauseTi->Update();
		pauseSt->Update();
	}
	else
	{	// それ以外なら
		Stoppause->Update();

		//生成したギアの更新処理
		for (int i = 0; i < MAX_GEAR; i++)
		{
			gj->mGear[i]->Update();
			jumpGear->mGear[i]->Update();
			catchiGear->mGear[i]->Update();
			upGear->mGear[i]->Update();
			StageGearX->mGear[i]->Update();
			emptiGear->mGear[i]->Update();
			StageGearY->mGear[i]->Update();

			if (gpMouse->mHitCircle->IsHit(catchiGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					catchiGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPower = true;
					LatterLightPower = true;
				}
			}
		}

		//CSprite更新処理	
		gpFade->Update();

		//ここに処理を書く	
		dool->Update();
		//goal->Update();
		stone->Update();
		//key->Update();

		//この条件式を書いたら時間を止めたら止まる
		if (BaseScene::TimeCheck == false)
		{
			gj->Update();
			jumpGear->Update();
			catchiGear->Update();
			upGear->Update();
			emptiGear->Update();
			doolMg->Update();
		}
		stanp->Update();
		StageGearX->Update();
		StageGearY->Update();
		for (int i = 0; i < 2; i++)
		{
			background[i]->Update();
		}
		UI01->Update();
		UI02->Update();
		UI03->Update();
		UI04->Update();
		UI05->Update();
		UI06->Update();
		GearUI01->Update();
		GearUI02->Update();
		GearUI03->Update();
		GearUI04->Update();
		GearUI05->Update();
		StageX->Update();
		StageY->Update();
		StageX2->Update();

		for (int i = 0; i < 3; i++)
		{
			Gpgear[i]->Update();
			latter[i]->Update();
		}
		for (int i = 0; i < maptip->mapObj.size(); i++)
		{
			maptip->mapObj[i]->Update();

			if (maptip->mapObj[i]->mapNo == 6)
			{
				StageX->SetPosition(maptip->mapObj[i]->mSprite->mCenter.x, maptip->mapObj[i]->mSprite->mCenter.y);
			}
			if (maptip->mapObj[i]->mapNo == 7)
			{
				StageY->SetPosition(maptip->mapObj[i]->mSprite->mCenter.x, maptip->mapObj[i]->mSprite->mCenter.y);
			}
			if (maptip->mapObj[i]->mapNo == 8)
			{
				StageX2->SetPosition(maptip->mapObj[i]->mSprite->mCenter.x, maptip->mapObj[i]->mSprite->mCenter.y);
			}

			if (maptip->mapObj[i]->mapNo == 5)
			{
				if (LatterLightPower)
				{
					maptip->mapObj[i]->mSprite->mColor.r = 3.0f;
				}
				else
				{
					maptip->mapObj[i]->mSprite->mColor.r = 1.0f;
				}
			}
		}

		////ステージギアを掴む
		//if (gpMouse->mHitCircle->IsHit(StageGearX->mHitCircle) == true)
		//{
		//	if (clickCheck == true)
		//	{
		//		StageGearX->SetPosition(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
		//	}
		//}
		//if (gpMouse->mHitCircle->IsHit(StageGearY->mHitCircle) == true)
		//{
		//	if (clickCheck == true)
		//	{
		//		StageGearY->SetPosition(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
		//	}
		//}


		//ギアを掴む処理
		for (int i = 0; i < MAX_GEAR; i++)
		{
			//ギア生成		

			if (gpMouse->mHitCircle->IsHit(gj->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					gj->Janereter();
				}
				GearUI01->Activate(true);
			}
			if (gpMouse->mHitCircle->IsHit(jumpGear->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					jumpGear->Janereter();
				}
				GearUI03->Activate(true);
			}

			if (gpMouse->mHitCircle->IsHit(catchiGear->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					catchiGear->Janereter();
				}
				GearUI05->Activate(true);
			}

			if (gpMouse->mHitCircle->IsHit(upGear->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					upGear->Janereter();
				}
				GearUI02->Activate(true);
			}

			if (gpMouse->mHitCircle->IsHit(emptiGear->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					emptiGear->Janereter();
				}
				GearUI04->Activate(true);
			}

			if (gpMouse->mHitCircle->IsHit(StageGearX->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					StageGearX->Janereter();
				}
			}

			if (gpMouse->mHitCircle->IsHit(StageGearY->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;
					StageGearY->Janereter();
				}
			}

			//生成ギアを掴む
			if (gpMouse->mHitCircle->IsHit(gj->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					gj->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPower = true;
				}
			}
			if (gpMouse->mHitCircle->IsHit(jumpGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					jumpGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPower = true;
				}
			}

			if (gpMouse->mHitCircle->IsHit(upGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					upGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPower = true;
				}
			}

			if (gpMouse->mHitCircle->IsHit(emptiGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					emptiGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPower = true;
				}
			}

			if (gpMouse->mHitCircle->IsHit(StageGearX->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					StageGearX->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPowerX = true;
				}
			}
			if (gpMouse->mHitCircle->IsHit(StageGearY->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					StageGearY->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
					LightPowerY = true;
				}
			}

			//反転用スイッチ
			if (gpMouse->mHitCircle->IsHit(UI05->mHitCircle) == true)
			{
				if (clickCheck == true && clickCheckAfter == false)
				{
					clickCheckAfter = true;

					if (mapHitChange)
					{
						mapHitChange = false;
					}
					else
					{
						mapHitChange = true;
					}
				}
			}

			//ギアがドールに入る
			if (doolMg->mHitCircle->IsHit(gj->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					XA_Play(SOUND_LABEL_SE001, 0.5f);
					clickCheckAfter = false;
					gj->mGear[i]->Activate(false);
					doolMg->SetGear(dool, gj->mGear[i]);
					Gpgear[0]->mSprite->SetTexture(gpTextureGear02);
					Gpgear[0]->Activate(true);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					gj->mGear[i]->Activate(false);
				}
			}

			if (doolMg->mHitCircle->IsHit(emptiGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					XA_Play(SOUND_LABEL_SE001, 0.5f);
					clickCheckAfter = false;
					emptiGear->mGear[i]->Activate(false);
					dool->GearDelete();
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					emptiGear->mGear[i]->Activate(false);
				}
			}

			if (doolMg->mHitCircle->IsHit(jumpGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					jumpGear->mGear[i]->Activate(false);
					doolMg->SetGear(dool, jumpGear->mGear[i]);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					jumpGear->mGear[i]->Activate(false);
				}
			}

			if (doolMg->mHitCircle->IsHit(catchiGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					catchiGear->mGear[i]->Activate(false);
					doolMg->SetGear(dool, catchiGear->mGear[i]);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					catchiGear->mGear[i]->Activate(false);
				}
			}

			if (doolMg->mHitCircle->IsHit(upGear->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					upGear->mGear[i]->Activate(false);
					doolMg->SetGear(dool, upGear->mGear[i]);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					upGear->mGear[i]->Activate(false);
				}
			}

			if (StageX->mHitCircle->IsHit(StageGearX->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					XA_Play(SOUND_LABEL_SE010, 0.8f);
					clickCheckAfter = false;
					ModeMoveFloorX = true;
					StageGearX->mGear[i]->Activate(false);
					StageX->Activate(false);
					BaseScene::Stop(5);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					StageGearX->mGear[i]->Activate(false);
				}
			}

			if (StageX2->mHitCircle->IsHit(StageGearX->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == true)
				{
					XA_Play(SOUND_LABEL_SE010, 0.8f);
					clickCheckAfter = false;
					ModeMoveFloorX2 = true;
					StageGearX->mGear[i]->Activate(false);
					StageX2->Activate(false);
					BaseScene::Stop(5);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					StageGearX->mGear[i]->Activate(false);
				}
			}

			if (StageY->mHitCircle->IsHit(StageGearY->mGear[i]->mHitCircle) == true)
			{
				if (clickCheck == false)
				{
					XA_Play(SOUND_LABEL_SE010, 0.8f);
					clickCheckAfter = false;
					ModeMoveFloorY = true;
					//StageGearY->Activate(false);
					StageGearY->mGear[i]->Activate(false);
					StageY->Activate(false);
					BaseScene::Stop(5);
				}
			}
			else
			{
				if (clickCheck == false)
				{
					clickCheckAfter = false;
					StageGearY->mGear[i]->Activate(false);
				}
			}
		}

		////押したらハンコが押される
		//if (clickCheck == true && clickCheckAfter == false)
		//{
		//	clickCheckAfter = true;
		//	stanp->mSprite->mColor.a = 1.0f;
		//	stanp->SetPosition(gpMouse->mSprite->mCenter.x - 0.1f, gpMouse->mSprite->mCenter.y - 0.1f);
		//}

		//毎フレーム透明にしていく
		stanp->mSprite->mColor.a -= 0.003f;

		for (int i = 0; i < 3; i++)
		{
			//入ってないなら画像を消す
			//それ以外なら対応した画像に変更
			if (dool->mGearSlot[i].nGear_no == 0)
			{
				Gpgear[i]->Activate(false);
			}
			if (dool->mGearSlot[i].nGear_no == 1)
			{
				Gpgear[i]->Activate(true);
				Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear03);
			}
			if (dool->mGearSlot[i].nGear_no == 2)
			{
				Gpgear[i]->Activate(true);
				Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear02);
			}
			if (dool->mGearSlot[i].nGear_no == 3)
			{
				Gpgear[i]->Activate(true);
				Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear05);
			}
			if (dool->mGearSlot[i].nGear_no == 4)
			{
				Gpgear[i]->Activate(true);
				Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear01);
			}
		}

		//ドールの中をいじる処理置き場
		//壁に当たった処理呼び出し
		doolMg->WallHit(dool, maptip);
		//ゴールに着いたとき
		//doolMg->GoalHit(dool, goal, key);
		//鍵をに当たったとき
		//doolMg->KeyHit(dool, key);
		//石に躓いた時
		doolMg->StoneGear(stone, dool);
		//ドールが真ん中まできてマップスクロールするとき
		doolMg->DoolPosMapScroll(dool, maptip);

		//床を右に動かす
		if (ModeMoveFloorX)
		{
			maptip->MovefloorX();
		}
		if (ModeMoveFloorX2)
		{
			maptip->MovefloorX2();
		}
		//床を上に動かす
		if (ModeMoveFloorY)
		{
			maptip->MovefloorY();
		}

		if (LightPower)
		{
			UI04->mSprite->mColor.r = 3.0f;
		}
		else
		{
			UI04->mSprite->mColor.r = 1.0f;
		}

		if (LightPowerX)
		{
			StageX->mSprite->mColor.r = 3.0f;
			StageX2->mSprite->mColor.r = 3.0f;
		}
		else
		{
			StageX->mSprite->mColor.r = 1.0f;
			StageX2->mSprite->mColor.r = 1.0f;
		}

		if (LightPowerY)
		{
			StageY->mSprite->mColor.r = 3.0f;
		}
		else
		{
			StageY->mSprite->mColor.r = 1.0f;
		}

		LightPower = false;
		LightPowerX = false;
		LightPowerY = false;
		LatterLightPower = false;

		//ドールが0以上なら
		if (latterCount > 0)
		{
			switch (latterCount)
			{
			case 1:
				latter[0]->Activate(true);
				break;
			case 2:
				latter[1]->Activate(true);
				break;
			case 3:
				latter[2]->Activate(true);
				break;
			default:
				break;
			}
		}
	}

	if (mapHitChange)
	{
		UI05->mSprite->SetTexture(gpTextureUI07);
	}
	else
	{
		UI05->mSprite->SetTexture(gpTextureUI06);
	}

	BaseScene::StopFlame();

	//ドール処理	
	//dool->GeraIdCheck();	
}

void Stage1_2::Draw()
{
	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	// 背景
	for (int i = 0; i < 2; i++)
	{
		background[i]->Draw();
	}
	// マップチップ
	for (int i = 0; i < maptip->mapObj.size(); i++)
	{
		maptip->mapObj[i]->Draw();
	}
	
	//石
	if (stone->active == true)
		stone->Draw();
	// いろいろ
	doolMg->Draw();
	StageX->Draw();
	StageX2->Draw();
	StageY->Draw();
	//key->Draw();
	//goal->Draw();	
	// UI
	UI01->Draw();
	UI04->Draw();
	UI03->Draw();
	UI02->Draw();
	UI06->Draw();
	UI05->Draw();

	dool->Draw();
	// ギア
	gj->Draw();
	jumpGear->Draw();
	catchiGear->Draw();
	upGear->Draw();
	emptiGear->Draw();
	GearUI01->Draw();
	GearUI04->Draw();
	GearUI03->Draw();
	GearUI02->Draw();
	GearUI05->Draw();

	//stanp->Draw();
	StageGearX->Draw();
	StageGearY->Draw();	

	Stoppause->Draw();	// ポーズギア

	for (int i = 0; i < MAX_GEAR; i++)
	{
		gj->mGear[i]->Draw();
		jumpGear->mGear[i]->Draw();
		catchiGear->mGear[i]->Draw();
		upGear->mGear[i]->Draw();
		StageGearX->mGear[i]->Draw();
		StageGearY->mGear[i]->Draw();
		emptiGear->mGear[i]->Draw();
	}
	for (int i = 0; i < 3; i++)
	{
		Gpgear[i]->Draw();
		latter[i]->Draw();
	}

	// ポーズ系
	if (pausemenu == true)
	{
		pauseBg->Draw();
		pauseUI->Draw();
		pauseGa->Draw();
		pauseRe->Draw();
		pauseTi->Draw();
		pauseSt->Draw();
	}

	// マウス
	gpMouse->Draw();
}

