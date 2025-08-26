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

	bool active = true;//表示、非表示

	void Activate(bool state);//アクティブを切り替える
};

