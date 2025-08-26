#pragma once
#include "BaseScene.h"
#include"Dool.h"
#include"Gear.h"
#include"GearJanereter.h"
#include"DoolManager.h"
#include"Goal.h"
#include"AStone.h"
#include"MaptipFileRead.h"
#include"Key.h"
#include "StaticObject.h"
#include"Fade.h"

#define MAX_GEAR 75
#define MAX_GP 8

//switch用識別番号
extern int sceneNo;

extern int oldsceneNo;

class Dool;
class DoolManager;
class Stage3_1 :
	public BaseScene
{
public:
	//初期化
	Stage3_1();

	//解放
	~Stage3_1();

	void Update();//更新処理
	void Draw();//描画処理

private:

	// pause用
	ID3D11ShaderResourceView* gpTextureStop;			//Stopのテクスチャ	
	ID3D11ShaderResourceView* gpTexturepause;			//pause背景のテクスチャ	
	ID3D11ShaderResourceView* gpTexturepauseUI;			//pauseUIのテクスチャ	
	ID3D11ShaderResourceView* gpTexturepauseGame;		//pauseゲームのテクスチャ	
	ID3D11ShaderResourceView* gpTexturepauseRetry;		//pauseリトライのテクスチャ	
	ID3D11ShaderResourceView* gpTexturepauseTitle;		//pauseタイトルのテクスチャ	
	ID3D11ShaderResourceView* gpTexturepauseStect;		//pauseセレクトのテクスチャ	
	ID3D11ShaderResourceView* gpTexturepauseMenu;		//pauseセレクトのテクスチャ	

	HitObject* Stoppause;
	StaticObject* pauseBg;
	StaticObject* pauseUI;
	HitObject* pauseGa;
	HitObject* pauseRe;
	HitObject* pauseTi;
	HitObject* pauseSt;

	//シーンで必要なもの

	//テクスチャ用の変数	
	ID3D11ShaderResourceView* gpTextureGear02;			//ギア画像02のテクスチャ
	ID3D11ShaderResourceView* gpTextureGear03;			//ギア画像03のテクスチャ
	ID3D11ShaderResourceView* gpTextureGear04;			//ステージギア画像03のテクスチャ
	ID3D11ShaderResourceView* gpTextureGear05;			//ステージギア画像03のテクスチャ
	ID3D11ShaderResourceView* gpTextureBackGround;		//背景画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI01;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI02;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI03;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI04;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI05;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI06;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI07;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureMouse01;			//マウス画像のテクスチャ
	ID3D11ShaderResourceView* gpTextureMouse02;			//マウス画像のテクスチャ
	ID3D11ShaderResourceView* gpTextureDool01;			//ドール画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureDoolFallLeft;	//左向きにこける画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureStone;			//石画像01のテクスチャ	

	//CSpriteクラスの変数
	Dool* dool;

	//ギア生成
	GearJanereter* gj;
	GearJanereter* jumpGear;
	GearJanereter* catchiGear;
	GearJanereter* upGear;
	GearJanereter* emptiGear;

	//ドール管理クラス
	DoolManager* doolMg;

	//ゴール
	//class Goal* goal;

	//石
	AStone* stone;

	//背景
	StaticObject* background[2];

	//UI
	HitObject* UI01;
	StaticObject* UI02;
	StaticObject* UI03;
	StaticObject* UI04;
	StaticObject* UI06;
	//切り替えよう
	HitObject* UI05;

	StaticObject* GearUI01;
	StaticObject* GearUI02;
	StaticObject* GearUI03;
	StaticObject* GearUI04;
	StaticObject* GearUI05;

	//マップチップ用
	MaptipFileRead* maptip;

	//鍵
	Key* key;

	//ギアがはまったとき用画像
	//三つあればよいかな
	RotateObject* Gpgear[3];

	//ステージギア用
	//右に動かす用
	GearJanereter* StageGearX;
	//上に動かす用
	GearJanereter* StageGearY;

	// フェード
	Fade* gpFade;

	//ハンコ押した画像
	StaticObject* stanp;

	//手紙用
	StaticObject* latter[3];

	HitObject* StageX;
	HitObject* StageX2;
	HitObject* StageX3;
	HitObject* StageY;

	//trueなら床を右に動かす
	bool ModeMoveFloorX = false;
	bool ModeMoveFloorX2 = false;
	bool ModeMoveFloorX3 = false;

	//trueなら床を上に動かす
	bool ModeMoveFloorY = false;

	// はめるスロットを赤くする
	bool LightPower = false;

	// 手紙を赤くする
	bool LatterLightPower = false;

	//ステージギア
	bool LightPowerX = false;
	bool LightPowerY = false;

	// ポーズメニューか？
	bool pausemenu = false;
};





