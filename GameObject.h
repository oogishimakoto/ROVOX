#pragma once
#include"CSprite.h"

class GameObject
{
public:
	//�`�揈��
	CSprite* mSprite;

	virtual void Update();
	virtual void Draw();

	bool active = true;//�\���A��\��

	virtual void Activate(bool state);//�A�N�e�B�u��؂�ւ���

	void SetPosition(float, float);
	void SetSize(float, float);
};

