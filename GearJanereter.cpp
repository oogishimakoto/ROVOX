#include "GearJanereter.h"

GearJanereter::GearJanereter(GEAR_ID Dogear)
{
	//�N���X����
	for (int i = 0; i < GEARCOUNT; i++)
	{
		mGear[i] = new Gear(Dogear);
		//�\������
		mGear[i]->Activate(false);
		//��ʊO�ɂ���
		mGear[i]->mSprite->mCenter = { 0,0 };		

		mGear[i]->mSprite->SetTexture(gpTextureGear01);
	}
}

GearJanereter::~GearJanereter()
{
	//����
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
	//�ێ����Ă���M�A�̐������炷
	mGearCount-=1;

	if (mGearCount >= 0)
	{
		//�M�A��\��
		mGear[mGearCount]->Activate(true);
		//��������ʒu�ɃM�A��u��
		mGear[mGearCount]->mSprite->mCenter.x = this->mSprite->mCenter.x;
		mGear[mGearCount]->mSprite->mCenter.y = this->mSprite->mCenter.y;
	}	
}

void GearJanereter::SetmGearCount(int _gearCount)
{
}
