#pragma once
#include"Gear.h"
#include"RotateObject.h"

#define GEARCOUNT 75

extern ID3D11ShaderResourceView* gpTextureGear01;			//�M�A�摜01�̃e�N�X�`��

extern ID3D11ShaderResourceView* gpTextureGoldGear02;		//�M�A�摜02�̃e�N�X�`��(���F)

extern ID3D11ShaderResourceView* gpTextureGoldGear03;		//�M�A�摜03�̃e�N�X�`��(���F)

extern ID3D11ShaderResourceView* gpTextureGoldGear01;		//�M�A�摜01�̃e�N�X�`��(���F)

class GearJanereter : public RotateObject
{
public:
	//�R���X�g�A�f�X�g
	GearJanereter(GEAR_ID Dogear);
	~GearJanereter();

	//�����֐�
	void Janereter();

	//�Z�b�^�[
	void SetmGearCount(int _gearCount);

	//�����Ă���M�A	
	Gear* mGear[GEARCOUNT];

private:
	//���Ă�M�A�̐�
	int mGearCount = GEARCOUNT;	
};

