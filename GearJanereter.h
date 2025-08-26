#pragma once
#include"Gear.h"
#include"RotateObject.h"

#define GEARCOUNT 75

extern ID3D11ShaderResourceView* gpTextureGear01;			//ギア画像01のテクスチャ

extern ID3D11ShaderResourceView* gpTextureGoldGear02;		//ギア画像02のテクスチャ(金色)

extern ID3D11ShaderResourceView* gpTextureGoldGear03;		//ギア画像03のテクスチャ(金色)

extern ID3D11ShaderResourceView* gpTextureGoldGear01;		//ギア画像01のテクスチャ(金色)

class GearJanereter : public RotateObject
{
public:
	//コンスト、デスト
	GearJanereter(GEAR_ID Dogear);
	~GearJanereter();

	//生成関数
	void Janereter();

	//セッター
	void SetmGearCount(int _gearCount);

	//持っているギア	
	Gear* mGear[GEARCOUNT];

private:
	//持てるギアの数
	int mGearCount = GEARCOUNT;	
};

