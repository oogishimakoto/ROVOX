#pragma once
#include"RotateSprite.h"
#include"HitCircle.h"
#include"HitSquare.h"

class RotateObject
{
public:

	RotateObject();

	~RotateObject();

	//�`�揈��
	RotateSprite* mSprite;

	virtual void Update();
	void Draw();

	bool active = true;//�\���A��\��

	void Activate(bool state);//�A�N�e�B�u��؂�ւ���

	void SetPosition(float, float);
	void SetSize(float, float);

	//�����蔻��(�~)
	HitCircle* mHitCircle;

	//�����蔻��(�l�p)
	HitSquare* mHitSquare;

	//�񂷂�
	void SetDoRotate(bool _r);

	//��葱���邩
	void SetDoingRotate(bool _r);
};

