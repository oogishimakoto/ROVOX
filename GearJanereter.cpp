#include "GearJanereter.h"

GearJanereter::GearJanereter(GEAR_ID Dogear)
{
	//クラス生成
	for (int i = 0; i < GEARCOUNT; i++)
	{
		mGear[i] = new Gear(Dogear);
		//表示消す
		mGear[i]->Activate(false);
		//画面外にする
		mGear[i]->mSprite->mCenter = { 0,0 };		

		mGear[i]->mSprite->SetTexture(gpTextureGear01);
	}
}

GearJanereter::~GearJanereter()
{
	//消す
	for (int i = 0; i < GEARCOUNT; i++)
	{
		delete mGear[i];
	}

	COM_SAFE_RELEASE(gpTextureGear01);
	COM_SAFE_RELEASE(gpTextureGoldGear01);
	COM_SAFE_RELEASE(gpTextureGoldGear02);
	COM_SAFE_RELEASE(gpTextureGoldGear03);
}

void GearJanereter::Janereter()
{
	//保持しているギアの数を減らす
	mGearCount-=1;

	if (mGearCount >= 0)
	{
		//ギアを表示
		mGear[mGearCount]->Activate(true);
		//生成する位置にギアを置く
		mGear[mGearCount]->mSprite->mCenter.x = this->mSprite->mCenter.x;
		mGear[mGearCount]->mSprite->mCenter.y = this->mSprite->mCenter.y;
	}	
}

void GearJanereter::SetmGearCount(int _gearCount)
{
}
