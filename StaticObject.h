#pragma once
#include "GameObject.h"
class StaticObject :
	public GameObject
{
public:
	StaticObject();
	~StaticObject();

	virtual void Update();
	void Draw();

	bool active = true;//�\���A��\��

	void Activate(bool state);//�A�N�e�B�u��؂�ւ���
};

