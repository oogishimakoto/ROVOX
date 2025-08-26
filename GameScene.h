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

#define MAX_GEAR 5
#define MAX_GP 8

extern int sceneNo;

class Dool;
class DoolManager;

class GameScene :
	public BaseScene
{
public:
	//初期化
	GameScene();

	//解放
	~GameScene();

	void Update();//更新処理
	void Draw();//描画処理

private:
	//シーンで必要なもの

	//テクスチャ用の変数	
	ID3D11ShaderResourceView* gpTextureGear02;			//ギア画像02のテクスチャ
	ID3D11ShaderResourceView* gpTextureGear03;			//ギア画像03のテクスチャ
	ID3D11ShaderResourceView* gpTextureBackGround;		//背景画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI01;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI02;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI03;			//UI画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureUI04;			//UI画像01のテクスチャ
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

	//マップチップ用
	MaptipFileRead* maptip;

	//鍵
	Key* key;

	//ギアがはまったとき用画像
	//三つあればよいかな
	RotateObject* Gpgear[3];

	//ステージギア用
	//右に動かす用
	RotateObject* StageGearX;
	//上に動かす用
	RotateObject* StageGearY;

	// フェード
	Fade* gpFade;

	//ハンコ押した画像
	StaticObject* stanp;

	//trueなら床を右に動かす
	bool ModeMoveFloorX = false;

	//trueなら床を上に動かす
	bool ModeMoveFloorY = false;

};

