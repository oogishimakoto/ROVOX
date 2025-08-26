#pragma once
#include "GameObject.h"

//四角形の当たり判定
class HitSquare 
{
private:
	//当たり判定用変数
	float right;	//プレイヤーの右辺
	float left;		//プレイヤーの左辺
	float up;		//プレイヤーの上辺
	float down;		//プレイヤーの下辺
	float other_right;	//当たっているオブジェクトの右辺
	float other_left;	//当たっているオブジェクトの左辺
	float other_up;		//当たっているオブジェクトの上辺
	float other_down;	//当たっているオブジェクトの下辺

	float other_hit_place_right;	//当たっているオブジェクトの方向を調べる
	float other_hit_place_left;		//当たっているオブジェクトの方向を調べる
	float other_hit_place_up;		//当たっているオブジェクトの方向を調べる
	float other_hit_place_down;		//当たっているオブジェクトの方向を調べる

public:
	bool IsSquareHit(HitSquare*  pOther);
	int IsSquareHitPlace(HitSquare*  pOther);
	//マップチップ用
	bool IsSquareHitMapChip(HitSquare*  pOther);
	int IsSquareHitPlaceMapChip(HitSquare*  pOther);

	//汎用当たり判定
	bool CheckSquareHit(HitSquare * obj1, HitSquare * obj2);
	int CheckSquareHitPlace(HitSquare * obj1, HitSquare * obj2);


	//参照型
	float& mCenterX; // 四角形の中心点座標（X）
	float& mCenterY; // 四角形の中心点座標（Y）
	float& mSizeX; // 四角形のサイズ（W）
	float& mSizeY; // 四角形のサイズ（H）
		
	enum HitPlace
	{
		HIT_UP,		//0
		HIT_DOWN,	//1
		HIT_LEFT,	//2
		HIT_RIGHT	//3
	};

	bool active = true;//当たり判定切り替える

	//当たったらtrueを返す
	HitSquare(float& x, float& y,float& w, float& h);
};

