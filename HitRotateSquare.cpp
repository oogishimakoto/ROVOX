#include "HitRotateSquare.h"
#include"directxmath.h"

HitRotateSquare::HitRotateSquare(float & x, float & y, float& w, float& h) : HitSquare(x, y, w, h)
{
	rot = 0.0F;									// ���̉�]�p�x��������
}

double HitRotateSquare::sqrt(double x)
{
	double s1 = 1, s2;
	if (x <= 0) {
		return 0;/*�G���[����*/
	}
	do
	{
		s2 = s1;
		s1 = (x / s1 + s1) / 2;
	} while (s1 != s2);
	return s1;
}

// ���Ɛ��̌�������
// l1x1,l1y1:�����P�̎n�_�@�@l1x2,l1y2:�����P�̏I�_
// l2x1,l2y1:�����Q�̎n�_�@�@l2x2,l2y2:�����Q�̏I�_
bool HitRotateSquare::HitCheck(float l1x1, float l1y1, float l1x2, float l1y2, float l2x1, float l2y1, float l2x2, float l2y2)
{
	VECT p1, v1, p2, v3, t;
	float x, z1, z2, len;

	// �����̎n�_�ƌ������Z�o
	p1.x = l1x1;				p1.y = l1y1;			// �����̎n�_
	v1.x = l1x2 - l1x1;		v1.y = l1y2 - l1y1;	// ����

	len = sqrt(v1.x * v1.x + v1.y * v1.y);			// ���K��
	v1.x /= len;				v1.y /= len;


	// ������������������̎n�_�ƁA�n�_���璼���ɐ����Ɍ���钼����̓_�̍��W�����߂�
	p2.x = l2x1;				p2.y = l2y1;				// �����蔻������������̎n�_

	// �����ɐ����Ɍ����_�����߂鉉�Z
	t.x = p2.x - p1.x;			t.y = p2.y - p1.y;			// ��������
	x = v1.x * t.x + v1.y * t.y;							// (�����̍s�œ��ς��Ă܂�) 
															// 
	v3.x = p1.x + v1.x * x;	v3.y = p1.y + v1.y * x;	// �����܂�

	t.x = v3.x - p2.x;			t.y = v3.y - p2.y;					// �n�_���狁�߂�������̓_�ւ̃x�N�g�����Z�o
	if (sqrt(t.x * t.x + t.y * t.y) < HITLENGTH) 
		return true;	// ���߂��_�܂ł̋����� HITLENGTH �ȉ�����������ɓ����������ɂ���

	z1 = v1.x * t.y - v1.y * t.x;			// �n�_���狁�߂��_�ւ̃x�N�g���ƁA�����̌����Ƃ̊O�ς����߂Ă����E�E�E�@


	// ������������������̏I�_�ƁA�I�_���璼���ɐ����Ɍ���钼����̓_�̍��W�����߂�
	p2.x = l2x2;				p2.y = l2y2;				// �����蔻������������̏I�_

	// �����ɐ����Ɍ����_�����߂鉉�Z
	t.x = p2.x - p1.x;			t.y = p2.y - p1.y;			// ��������
	x = v1.x * t.x + v1.y * t.y;							// (�����̍s�œ��ς��Ă܂�) 
															// 
	v3.x = p1.x + v1.x * x;	v3.y = p1.y + v1.y * x;	// �����܂�

	t.x = v3.x - p2.x;			t.y = v3.y - p2.y;					// �I�_���狁�߂�������̓_�ւ̃x�N�g�����Z�o
	if (sqrt(t.x * t.x + t.y * t.y) < HITLENGTH) 
		return true;	// ���߂��_�܂ł̋����� HITLENGTH �ȉ�����������ɓ����������ɂ���

	z2 = v1.x * t.y - v1.y * t.x;			// �I�_���狁�߂��_�ւ̃x�N�g���ƁA�����̌����Ƃ̊O�ς����߂Ă����E�E�E�A


	// �@�ƇA�ŋ��߂��x�N�g���̌������Ⴄ�ꍇ�͌�����������������ƌ��������؋�
	return (z1 >= 0.0F && z2 < 0.0F) || (z2 >= 0.0F && z1 < 0.0F);
}

void HitRotateSquare::RotateSquare()
{
	//// ���̎l�ӂƓ_�Ƃ̓����蔻��(�_�̈ړ��O�ƈړ�������Ԑ������̎l�ӂƌ����������𔻒�)
	//if (HitCheck(px[0], py[0], px[1], py[1], bx, by, x, y) == true ||
	//	HitCheck(px[1], py[1], px[2], py[2], bx, by, x, y) == true ||
	//	HitCheck(px[2], py[2], px[3], py[3], bx, by, x, y) == true ||
	//	HitCheck(px[3], py[3], px[0], py[0], bx, by, x, y) == true)
	//{

	//}
	rot += ROTSPEED;								// ���̉�]�p�x��ύX
	if (rot > PI_2) rot -= PI_2;					// ������Ă�����␳������
	//x

	right = this->mCenterX;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY / 2.0f);
	px[0] = left;				// ���̎l���̍��W�̏����l���Z�b�g
	px[1] = +right;
	px[2] = +right;
	px[3] = left;

	//y
	py[0] = up;
	py[1] = up;
	py[2] = down;
	py[3] = down;
	for (i = 0; i < 4; i++)						// ���̎l���̍��W�����݂̉�]�p�x�ɍ��킹�ĉ�]
	{
		f = px[i] * cos(rot) - py[i] * sin(rot);
		py[i] = px[i] * sin(rot) + py[i] * cos(rot);
		px[i] = f;

		px[i] += BOXX;		// ��]������A�\�����������W�Ɉړ�
		py[i] += BOXY;
	}
}


