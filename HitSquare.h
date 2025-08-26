#pragma once
#include "GameObject.h"

//�l�p�`�̓����蔻��
class HitSquare 
{
private:
	//�����蔻��p�ϐ�
	float right;	//�v���C���[�̉E��
	float left;		//�v���C���[�̍���
	float up;		//�v���C���[�̏��
	float down;		//�v���C���[�̉���
	float other_right;	//�������Ă���I�u�W�F�N�g�̉E��
	float other_left;	//�������Ă���I�u�W�F�N�g�̍���
	float other_up;		//�������Ă���I�u�W�F�N�g�̏��
	float other_down;	//�������Ă���I�u�W�F�N�g�̉���

	float other_hit_place_right;	//�������Ă���I�u�W�F�N�g�̕����𒲂ׂ�
	float other_hit_place_left;		//�������Ă���I�u�W�F�N�g�̕����𒲂ׂ�
	float other_hit_place_up;		//�������Ă���I�u�W�F�N�g�̕����𒲂ׂ�
	float other_hit_place_down;		//�������Ă���I�u�W�F�N�g�̕����𒲂ׂ�

public:
	bool IsSquareHit(HitSquare*  pOther);
	int IsSquareHitPlace(HitSquare*  pOther);
	//�}�b�v�`�b�v�p
	bool IsSquareHitMapChip(HitSquare*  pOther);
	int IsSquareHitPlaceMapChip(HitSquare*  pOther);

	//�ėp�����蔻��
	bool CheckSquareHit(HitSquare * obj1, HitSquare * obj2);
	int CheckSquareHitPlace(HitSquare * obj1, HitSquare * obj2);


	//�Q�ƌ^
	float& mCenterX; // �l�p�`�̒��S�_���W�iX�j
	float& mCenterY; // �l�p�`�̒��S�_���W�iY�j
	float& mSizeX; // �l�p�`�̃T�C�Y�iW�j
	float& mSizeY; // �l�p�`�̃T�C�Y�iH�j
		
	enum HitPlace
	{
		HIT_UP,		//0
		HIT_DOWN,	//1
		HIT_LEFT,	//2
		HIT_RIGHT	//3
	};

	bool active = true;//�����蔻��؂�ւ���

	//����������true��Ԃ�
	HitSquare(float& x, float& y,float& w, float& h);
};

