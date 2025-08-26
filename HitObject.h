#pragma once
#include "GameObject.h"
#include"HitCircle.h"
#include"HitSquare.h"

//アニメーションなし当たり判定付きオブジェクト
class HitObject :
	public GameObject
{
public:
	//コンストラクタ
	HitObject();

	//デストラクタ
	~HitObject();

	//当たり判定(円)
	HitCircle* mHitCircle;

	//当たり判定(四角)
	HitSquare* mHitSquare;

	//当たり判定あるかないか
	void Activate(bool state) override;
};

