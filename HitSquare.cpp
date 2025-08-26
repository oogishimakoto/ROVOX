#include "HitSquare.h"

bool HitSquare::IsSquareHit(HitSquare *  pOther)
{
	if (active == false || pOther->active == false)
	{
		return false;
	}

	//------------------------------------------------
	//�����Ӂ�mCenter.x�͒������Ƃ��Ă܂���
	//		�@�E�[������Ă��܂�
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
	//�����Ӂ�mCenter.x�͒������Ƃ��Ă܂���
	//		�@�E�[������Ă��܂�
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
		//�I�u�W�F�N�g�̉E�������������Ă���
		if (right < other_hit_place_left)
			return HIT_LEFT;		//�������Ă��鑤�̃I�u�W�F�N�g�̂ǂ��ɐG��Ă��邩��Ԃ�

		//�I�u�W�F�N�g�̉��������������Ă���
		if (down > other_hit_place_up)
			return HIT_UP;

		//�I�u�W�F�N�g(�v���C���[)�̏㕔�����������Ă���
		if (up < other_hit_place_down)
			return HIT_DOWN;

		//�I�u�W�F�N�g�̍��������������Ă���
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
	//�����Ӂ�mCenter.x�͒������Ƃ��Ă܂���
	//		�@�E�[������Ă��܂�
	//-------------------------------------------------
	right = this->mCenterX;
	left = this->mCenterX - (this->mSizeX);
	up = this->mCenterY + (this->mSizeY / 2.0f);
	down = this->mCenterY - (this->mSizeY / 2.0f);

	//��
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
	//�����Ӂ�mCenter.x�͒������Ƃ��Ă܂���
	//		�@�E�[������Ă��܂�
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
		//�I�u�W�F�N�g�̉E�������������Ă���
		if (right < other_hit_place_left)
			return HIT_LEFT;		//�������Ă��鑤�̃I�u�W�F�N�g�̂ǂ��ɐG��Ă��邩��Ԃ�

		//�I�u�W�F�N�g�̉��������������Ă���
		if (down > other_hit_place_up)
			return HIT_UP;

		//�I�u�W�F�N�g�̍��������������Ă���
		if (left > other_hit_place_right)
			return HIT_RIGHT;

		//�I�u�W�F�N�g(�v���C���[)�̏㕔�����������Ă���
		if (up < other_hit_place_down)
			return HIT_DOWN;
	}

}

bool HitSquare::CheckSquareHit(HitSquare * obj1, HitSquare * obj2)
{
	right = obj1->mCenterX + obj1->mSizeX / 5.0f;	//�v���C���[�̉E��
	left = obj1->mCenterX - obj1->mSizeX / 5.0f;		//�v���C���[�̍���
	up = obj1->mCenterY + obj1->mSizeY / 2.5f;		//�v���C���[�̏��
	down = obj1->mCenterY - obj1->mSizeY / 2.5f;		//�v���C���[�̉���
	other_right = obj2->mCenterX + obj2->mSizeX / 2.0f;	//�������Ă���I�u�W�F�N�g�̉E��
	other_left = obj2->mCenterX - obj2->mSizeX / 2.0f;	//�������Ă���I�u�W�F�N�g�̍���
	other_up = obj2->mCenterY + obj2->mSizeY / 2.0f;		//�������Ă���I�u�W�F�N�g�̏��
	other_down = obj2->mCenterY - obj2->mSizeY / 2.0f;	//�������Ă���I�u�W�F�N�g�̉���
	
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
			return 1;	// ��
		}
		else
		{
			return 2; //�@�E
		}

	}
	
	if (posY >= 0.0f)
	{
		return 3;	// ��
	}
	else
	{
		return 4; //�@��
	}

	return false;
}

HitSquare::HitSquare(float & x, float & y, float& w, float& h) : mCenterX(x), mCenterY(y),mSizeX(w),mSizeY(h)
{
}
