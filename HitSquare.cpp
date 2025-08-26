#include "HitSquare.h"

bool HitSquare::IsSquareHit(HitSquare *  pOther)
{
	if (active == false || pOther->active == false)
	{
		return false;
	}

	//------------------------------------------------
	//＜注意＞mCenter.xは中央をとってません
	//		　右端を取っています
	//-------------------------------------------------
	right = this->mCenterX ;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY/ 2.0f);

	other_right = pOther->mCenterX;
	other_left = pOther->mCenterX - (pOther->mSizeX);
	other_up = pOther->mCenterY + (pOther->mSizeY / 2.0f);
	other_down = pOther->mCenterY - (pOther->mSizeY /2.0f);

	if (up >= other_down && right >= other_left && left <= other_right && down <= other_up)
	{
		return true;
	}	
		
}

int HitSquare::IsSquareHitPlace(HitSquare * pOther)
{
	//------------------------------------------------
	//＜注意＞mCenter.xは中央をとってません
	//		　右端を取っています
	//-------------------------------------------------
	right = this->mCenterX;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY / 2.0f);

	other_right = pOther->mCenterX ;
	other_left = pOther->mCenterX - (pOther->mSizeX);
	other_up = pOther->mCenterY + (pOther->mSizeY / 2.0f);
	other_down = pOther->mCenterY - (pOther->mSizeY / 2.0f);

	other_hit_place_right = pOther->mCenterX - (pOther->mSizeX / 10.0f);
	other_hit_place_left = pOther->mCenterX - (pOther->mSizeX - (pOther->mSizeX / 10.0f));
	other_hit_place_up = pOther->mCenterY + ((pOther->mSizeY / 2.0f) - (pOther->mSizeY / 10.0f));
	other_hit_place_down = pOther->mCenterY - ((pOther->mSizeY / 2.0f) - (pOther->mSizeY / 10.0f));

	if (up >= other_down && right >= other_left && left <= other_right && down <= other_up)
	{
		//オブジェクトの右部分が当たっている
		if (right < other_hit_place_left)
			return HIT_LEFT;		//当たっている側のオブジェクトのどこに触れているかを返す

		//オブジェクトの下部分が当たっている
		if (down > other_hit_place_up)
			return HIT_UP;

		//オブジェクト(プレイヤー)の上部分が当たっている
		if (up < other_hit_place_down)
			return HIT_DOWN;

		//オブジェクトの左部分が当たっている
		if (left > other_hit_place_right)
			return HIT_RIGHT;

	}

}

bool HitSquare::IsSquareHitMapChip(HitSquare *  pOther)
{
	if (active == false || pOther->active == false)
	{
		return false;
	}

	//------------------------------------------------
	//＜注意＞mCenter.xは中央をとってません
	//		　右端を取っています
	//-------------------------------------------------
	right = this->mCenterX;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY / 2.0f);

	//旧
	//other_right = pOther->mCenterX - (pOther->mSizeX + pOther->mSizeX / 1.8f);

	other_right = pOther->mCenterX - (pOther->mSizeX + pOther->mSizeX /4.0f);
	other_left = pOther->mCenterX - (pOther->mSizeX / 1.1f);
	other_up = pOther->mCenterY + (pOther->mSizeY / 2.3f);
	other_down = pOther->mCenterY - (pOther->mSizeY / 2.0f);

	if (up >= other_down && right >= other_left && left <= other_right && down <= other_up)
	{
		return true;
	}

}


int HitSquare::IsSquareHitPlaceMapChip(HitSquare * pOther)
{
	//------------------------------------------------
	//＜注意＞mCenter.xは中央をとってません
	//		　右端を取っています
	//-------------------------------------------------
	right = this->mCenterX ;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY / 2.0f);

	other_right = pOther->mCenterX - (pOther->mSizeX + pOther->mSizeX / 4.0f);
	other_left = pOther->mCenterX - (pOther->mSizeX / 1.1f);
	other_up = pOther->mCenterY + (pOther->mSizeY / 2.0f);
	other_down = pOther->mCenterY - (pOther->mSizeY / 2.0f);

	other_hit_place_right = pOther->mCenterX - ((pOther->mSizeX + pOther->mSizeX / 4.0f) + (pOther->mSizeX / 10.0f));
	other_hit_place_left = pOther->mCenterX - ((pOther->mSizeX / 1.1f) - (pOther->mSizeX / 10.0f));
	other_hit_place_up = pOther->mCenterY + ((pOther->mSizeY / 2.0f) - (pOther->mSizeY / 10.0f));
	other_hit_place_down = pOther->mCenterY - ((pOther->mSizeY / 2.0f) - (pOther->mSizeY / 10.0f));

	if (up >= other_down && right >= other_left && left <= other_right && down <= other_up)
	{
		//オブジェクトの右部分が当たっている
		if (right < other_hit_place_left)
			return HIT_LEFT;		//当たっている側のオブジェクトのどこに触れているかを返す

		//オブジェクトの下部分が当たっている
		if (down > other_hit_place_up)
			return HIT_UP;

		//オブジェクトの左部分が当たっている
		if (left > other_hit_place_right)
			return HIT_RIGHT;

		//オブジェクト(プレイヤー)の上部分が当たっている
		if (up < other_hit_place_down)
			return HIT_DOWN;
	}

}

bool HitSquare::CheckSquareHit(HitSquare * obj1, HitSquare * obj2)
{
	right = obj1->mCenterX + obj1->mSizeX / 5.0f;	//プレイヤーの右辺
	left = obj1->mCenterX - obj1->mSizeX / 5.0f;		//プレイヤーの左辺
	up = obj1->mCenterY + obj1->mSizeY / 2.5f;		//プレイヤーの上辺
	down = obj1->mCenterY - obj1->mSizeY / 2.5f;		//プレイヤーの下辺
	other_right = obj2->mCenterX + obj2->mSizeX / 2.0f;	//当たっているオブジェクトの右辺
	other_left = obj2->mCenterX - obj2->mSizeX / 2.0f;	//当たっているオブジェクトの左辺
	other_up = obj2->mCenterY + obj2->mSizeY / 2.0f;		//当たっているオブジェクトの上辺
	other_down = obj2->mCenterY - obj2->mSizeY / 2.0f;	//当たっているオブジェクトの下辺
	
	if (right >= other_left &&
		other_right >= left &&
		up >= other_down &&
		other_up >= down) {
		return true;
	}

	//if (obj1->mCenterX + obj1->mSizeX / 3.3f >= obj2->mCenterX - obj2->mSizeX / 3.3f &&
	//	obj2->mCenterX + obj2->mSizeX / 3.3f >= obj1->mCenterX - obj1->mSizeX / 3.3f &&
	//	obj1->mCenterY + obj1->mSizeY / 2.0f >= obj2->mCenterY - obj2->mSizeY / 2.0f &&
	//	obj2->mCenterY + obj2->mSizeY / 2.0f >= obj1->mCenterY - obj1->mSizeY / 2.0f) {
	//	return true;
	//}
	return false;

}

int HitSquare::CheckSquareHitPlace(HitSquare * obj1, HitSquare * obj2)
{
	const long  posX = obj1->mCenterX - obj2->mCenterX;
	const long  posY = obj1->mCenterY - obj2->mCenterY;

	if ((posX) >= 1) 
	{
		if (posX >= 0.0f)
		{
			return 1;	// 左
		}
		else
		{
			return 2; //　右
		}

	}
	
	if (posY >= 0.0f)
	{
		return 3;	// 上
	}
	else
	{
		return 4; //　下
	}

	return false;
}

HitSquare::HitSquare(float & x, float & y, float& w, float& h) : mCenterX(x), mCenterY(y),mSizeX(w),mSizeY(h)
{
}
