#pragma once
#include"CSprite.h"

class GameObject
{
public:
	//描画処理
	CSprite* mSprite;

	virtual void Update();
	virtual void Draw();

	bool active = true;//表示、非表示

	virtual void Activate(bool state);//アクティブを切り替える

	void SetPosition(float, float);
	void SetSize(float, float);
};

