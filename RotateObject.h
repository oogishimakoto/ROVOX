#pragma once
#include"RotateSprite.h"
#include"HitCircle.h"
#include"HitSquare.h"

class RotateObject
{
public:

	RotateObject();

	~RotateObject();

	//描画処理
	RotateSprite* mSprite;

	virtual void Update();
	void Draw();

	bool active = true;//表示、非表示

	void Activate(bool state);//アクティブを切り替える

	void SetPosition(float, float);
	void SetSize(float, float);

	//当たり判定(円)
	HitCircle* mHitCircle;

	//当たり判定(四角)
	HitSquare* mHitSquare;

	//回すか
	void SetDoRotate(bool _r);

	//廻り続けるか
	void SetDoingRotate(bool _r);
};

