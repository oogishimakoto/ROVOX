#include "HitRotateSquare.h"
#include"directxmath.h"

HitRotateSquare::HitRotateSquare(float & x, float & y, float& w, float& h) : HitSquare(x, y, w, h)
{
	rot = 0.0F;									// 箱の回転角度を初期化
}

double HitRotateSquare::sqrt(double x)
{
	double s1 = 1, s2;
	if (x <= 0) {
		return 0;/*エラー処理*/
	}
	do
	{
		s2 = s1;
		s1 = (x / s1 + s1) / 2;
	} while (s1 != s2);
	return s1;
}

// 線と線の交差判定
// l1x1,l1y1:直線１の始点　　l1x2,l1y2:直線１の終点
// l2x1,l2y1:直線２の始点　　l2x2,l2y2:直線２の終点
bool HitRotateSquare::HitCheck(float l1x1, float l1y1, float l1x2, float l1y2, float l2x1, float l2y1, float l2x2, float l2y2)
{
	VECT p1, v1, p2, v3, t;
	float x, z1, z2, len;

	// 直線の始点と向きを算出
	p1.x = l1x1;				p1.y = l1y1;			// 直線の始点
	v1.x = l1x2 - l1x1;		v1.y = l1y2 - l1y1;	// 向き

	len = sqrt(v1.x * v1.x + v1.y * v1.y);			// 正規化
	v1.x /= len;				v1.y /= len;


	// 交差判定をしたい線の始点と、始点から直線に垂直に交わる直線上の点の座標を求める
	p2.x = l2x1;				p2.y = l2y1;				// 当たり判定をしたい線の始点

	// 直線に垂直に交わる点を求める演算
	t.x = p2.x - p1.x;			t.y = p2.y - p1.y;			// ここから
	x = v1.x * t.x + v1.y * t.y;							// (←この行で内積してます) 
															// 
	v3.x = p1.x + v1.x * x;	v3.y = p1.y + v1.y * x;	// ここまで

	t.x = v3.x - p2.x;			t.y = v3.y - p2.y;					// 始点から求めた直線上の点へのベクトルを算出
	if (sqrt(t.x * t.x + t.y * t.y) < HITLENGTH) 
		return true;	// 求めた点までの距離が HITLENGTH 以下だったら線に当たった事にする

	z1 = v1.x * t.y - v1.y * t.x;			// 始点から求めた点へのベクトルと、直線の向きとの外積を求めておく・・・①


	// 交差判定をしたい線の終点と、終点から直線に垂直に交わる直線上の点の座標を求める
	p2.x = l2x2;				p2.y = l2y2;				// 当たり判定をしたい線の終点

	// 直線に垂直に交わる点を求める演算
	t.x = p2.x - p1.x;			t.y = p2.y - p1.y;			// ここから
	x = v1.x * t.x + v1.y * t.y;							// (←この行で内積してます) 
															// 
	v3.x = p1.x + v1.x * x;	v3.y = p1.y + v1.y * x;	// ここまで

	t.x = v3.x - p2.x;			t.y = v3.y - p2.y;					// 終点から求めた直線上の点へのベクトルを算出
	if (sqrt(t.x * t.x + t.y * t.y) < HITLENGTH) 
		return true;	// 求めた点までの距離が HITLENGTH 以下だったら線に当たった事にする

	z2 = v1.x * t.y - v1.y * t.x;			// 終点から求めた点へのベクトルと、直線の向きとの外積を求めておく・・・②


	// ①と②で求めたベクトルの向きが違う場合は交差判定をしたい線と交差した証拠
	return (z1 >= 0.0F && z2 < 0.0F) || (z2 >= 0.0F && z1 < 0.0F);
}

void HitRotateSquare::RotateSquare()
{
	//// 箱の四辺と点との当たり判定(点の移動前と移動後を結ぶ線が箱の四辺と交差したかを判定)
	//if (HitCheck(px[0], py[0], px[1], py[1], bx, by, x, y) == true ||
	//	HitCheck(px[1], py[1], px[2], py[2], bx, by, x, y) == true ||
	//	HitCheck(px[2], py[2], px[3], py[3], bx, by, x, y) == true ||
	//	HitCheck(px[3], py[3], px[0], py[0], bx, by, x, y) == true)
	//{

	//}
	rot += ROTSPEED;								// 箱の回転角度を変更
	if (rot > PI_2) rot -= PI_2;					// 一周していたら補正をする
	//x

	right = this->mCenterX;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY / 2.0f);
	px[0] = left;				// 箱の四隅の座標の初期値をセット
	px[1] = +right;
	px[2] = +right;
	px[3] = left;

	//y
	py[0] = up;
	py[1] = up;
	py[2] = down;
	py[3] = down;
	for (i = 0; i < 4; i++)						// 箱の四隅の座標を現在の回転角度に合わせて回転
	{
		f = px[i] * cos(rot) - py[i] * sin(rot);
		py[i] = px[i] * sin(rot) + py[i] * cos(rot);
		px[i] = f;

		px[i] += BOXX;		// 回転した後、表示したい座標に移動
		py[i] += BOXY;
	}
}


