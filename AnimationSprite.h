#pragma once
#include "CSprite.h"

//キャラの方向表す変数
enum DIRECTION
{
	DOWN, LEFT, RIGHT, UP
};

class AnimationSprite :	public CSprite
{
public:
	//アニメーション機能
	void Update() override;

	float mAnimCnt;//アニメテーブルの添え字

	float mAnimSpeed = 0.006;//アニメーション再生速度

	//アニメテーブル
	//メンバ変数にstatic:設計図上に直接変数を作る
	static constexpr int mAnimTable[] = { 0,1,2,1,-1 };

	DIRECTION mDirection;
};

