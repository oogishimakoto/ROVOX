#pragma once
#include "HitSquare.h"

#define PI			(3.14159265358)		// ��
#define PI_2		(3.14159265358)		// �Q��
#define BOXSIZE		(128)				// ���̑傫��
#define BOXX		(320)				// ���̂w���W
#define BOXY		(240)				// ���̂x���W
#define ROTSPEED	(PI_2 / 180)		// ���̉�]���x
#define SPEED		(8.0F)				// �_�̑��x
#define HITLENGTH	(4.0F)				// ���̕ǂƂ̓����蔻��̕�

class HitRotateSquare :
	public HitSquare
{
private:
	float rot = 0.0F;				// ���̉�]�p�x
	float x, y, sx, sy;	// ���̒��𓮂��_�̍��W�Ƒ��x
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

