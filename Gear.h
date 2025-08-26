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
	//�R���X�g���N�^
	//�����łǂ̍s�����邩���߂�
	Gear();
	Gear(GEAR_ID Dogear);

	//�f�X�g���N�^
	~Gear();

	//�Q�b�^�[�Z�b�^�[
	GEAR_ID GetGear();

	//�M�A�𓮂����֐�
	//�����F�}�E�X���W
	void MoveGear(float _xpos, float _ypos);

	int GetGearNo();	

	void Update();

private:

	GEAR_ID mDogear;
};

