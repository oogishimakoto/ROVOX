#pragma once
#include"RotateObject.h"

typedef struct
{
	int nGear_no;
	int nGear_tag;
}GEAR_ID;

enum GEAR_NO
{
	REVERSE,
	WALK,
	JUMP,
	CHACH,
	SHAGMU
};

enum GEAR_TAG
{
	NORMAL,
	SABI,
	HISPEC
};

class Gear : public RotateObject
{
public:
	//コンストラクタ
	//引数でどの行動するか決める
	Gear();
	Gear(GEAR_ID Dogear);

	//デストラクタ
	~Gear();

	//ゲッターセッター
	GEAR_ID GetGear();

	//ギアを動かす関数
	//引数：マウス座標
	void MoveGear(float _xpos, float _ypos);

	int GetGearNo();	

	void Update();

private:

	GEAR_ID mDogear;
};

