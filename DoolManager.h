#pragma once
#include"Dool.h"
#include"Gear.h"
#include"AnimHitObject.h"
#include"AStone.h"
#include"MaptipFileRead.h"
#include"Goal.h"
#include"Key.h"

class Goal;
class Dool;
#define MAX_GP 8

//押してる画像
extern ID3D11ShaderResourceView* gpTextureDoolPushu[MAX_GP];

//登る
extern ID3D11ShaderResourceView* gpTextureDoolNoboru[MAX_GP];

//マップがスクロールするか歩くか
extern bool modeScrollChange;

extern bool reverseDool;

extern bool debugMode;

class DoolManager : public AnimHitObject
{
private:
	//セットされているギアをすべて削除
	GEAR_ID geardelete;

	MapObject obj[800];

	bool keyget = false;	

public:
	//ドールにギアをセットする
	void SetGear(Dool* dool, Gear* gear);

	//ドールと石があった時の処理
	void StoneGear(AStone* stone, Dool* dool);

	//マップの壁と当たった処理
	void WallHit(Dool* dool, MaptipFileRead* map);

	//ゴール当たった処理
	void GoalHit(Dool* dool, Goal* goal, Key* key);

	//鍵に当たった処理
	void KeyHit(Dool* dool, Key* key);

	//ドールの位置でマップスクロールするか決める
	void DoolPosMapScroll(Dool* dool, MaptipFileRead* map);	

	//一回当たったらそのオブジェクトを記憶する
	int saveObj = 0;

	//左向きでも落ちれるようにするする
	bool flg = true;
};

