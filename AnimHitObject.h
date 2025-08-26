#pragma once
#include "GameObject.h"
#include"AnimationSprite.h"
#include"HitCircle.h"
#include"HitSquare.h"


class AnimHitObject :	public GameObject
{
public:
	//コンストラクタ
	AnimHitObject();

	//デストラクタ
	~AnimHitObject();

	//アニメーション
	AnimationSprite* mAnimSprite;

	//当たり判定(円)
	HitCircle* mHitCircle;	

	//当たり判定(四角)
	HitSquare* mHitSquare;

	//当たり判定あるかないか
	void Activate(bool state) override;
};

