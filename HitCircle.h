#pragma once
#include"GameObject.h"

// �~���m�̓����蔻��̈��\������N���X
class HitCircle
{
public:

	// ���肷��
	// �����Ŏ󂯎�����~�Ǝ������g�i�~�j���Փ˂��Ă邩true/false�ŕԂ�
	bool IsHit(HitCircle* pOther);
	
	//�Q�ƌ^
	float& mCenterX; // �~�̒��S�_���W�iX�j
	float& mCenterY; // �~�̒��S�_���W�iY�j
	float mHankei; // �~�̔��a
	bool active = true;//�����蔻��؂�ւ���

	//���S���W�Ɣ��a���Z�b�g����R���X�g���N�^
	HitCircle(float& x,float& y,float hankei);
};

