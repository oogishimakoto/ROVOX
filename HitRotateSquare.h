#pragma once
#include "HitSquare.h"

#define PI			(3.14159265358)		// π
#define PI_2		(3.14159265358)		// ２π
#define BOXSIZE		(128)				// 箱の大きさ
#define BOXX		(320)				// 箱のＸ座標
#define BOXY		(240)				// 箱のＹ座標
#define ROTSPEED	(PI_2 / 180)		// 箱の回転速度
#define SPEED		(8.0F)				// 点の速度
#define HITLENGTH	(4.0F)				// 箱の壁との当たり判定の幅

class HitRotateSquare :
	public HitSquare
{
private:
	float rot = 0.0F;				// 箱の回転角度
	float x, y, sx, sy;	// 箱の中を動く点の座標と速度
	float bx, by, px[4], py[4], f;
	int i;
public:
	HitRotateSquare(float & x, float & y, float& w, float& h);

	double sqrt(double x);

	bool HitCheck(float l1x1, float l1y1, float l1x2, float l1y2,
		float l2x1, float l2y1, float l2x2, float l2y2);

	virtual void RotateSquare();

	typedef struct tagVECT
	{
		float x, y;
	} VECT;
};

