#pragma once
#include "GameObject.h"
#include"HitCircle.h"
#include"HitSquare.h"

//�A�j���[�V�����Ȃ������蔻��t���I�u�W�F�N�g
class HitObject :
	public GameObject
{
public:
	//�R���X�g���N�^
	HitObject();

	//�f�X�g���N�^
	~HitObject();

	//�����蔻��(�~)
	HitCircle* mHitCircle;

	//�����蔻��(�l�p)
	HitSquare* mHitSquare;

	//�����蔻�肠�邩�Ȃ���
	void Activate(bool state) override;
};

