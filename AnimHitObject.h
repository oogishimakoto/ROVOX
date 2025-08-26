#pragma once
#include "GameObject.h"
#include"AnimationSprite.h"
#include"HitCircle.h"
#include"HitSquare.h"


class AnimHitObject :	public GameObject
{
public:
	//�R���X�g���N�^
	AnimHitObject();

	//�f�X�g���N�^
	~AnimHitObject();

	//�A�j���[�V����
	AnimationSprite* mAnimSprite;

	//�����蔻��(�~)
	HitCircle* mHitCircle;	

	//�����蔻��(�l�p)
	HitSquare* mHitSquare;

	//�����蔻�肠�邩�Ȃ���
	void Activate(bool state) override;
};

