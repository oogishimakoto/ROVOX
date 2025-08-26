#pragma once
#include "CSprite.h"

//�L�����̕����\���ϐ�
enum DIRECTION
{
	DOWN, LEFT, RIGHT, UP
};

class AnimationSprite :	public CSprite
{
public:
	//�A�j���[�V�����@�\
	void Update() override;

	float mAnimCnt;//�A�j���e�[�u���̓Y����

	float mAnimSpeed = 0.006;//�A�j���[�V�����Đ����x

	//�A�j���e�[�u��
	//�����o�ϐ���static:�݌v�}��ɒ��ڕϐ������
	static constexpr int mAnimTable[] = { 0,1,2,1,-1 };

	DIRECTION mDirection;
};

