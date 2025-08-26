#pragma once
#include"GameObject.h"

// 円同士の当たり判定領域を表現するクラス
class HitCircle
{
public:

	// 判定する
	// 引数で受け取った円と自分自身（円）が衝突してるかtrue/falseで返す
	bool IsHit(HitCircle* pOther);
	
	//参照型
	float& mCenterX; // 円の中心点座標（X）
	float& mCenterY; // 円の中心点座標（Y）
	float mHankei; // 円の半径
	bool active = true;//当たり判定切り替える

	//中心座標と半径をセットするコンストラクタ
	HitCircle(float& x,float& y,float hankei);
};

