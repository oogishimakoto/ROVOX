#include "Operation.h"

extern POINT cursor;

void Operation::Update()
{
	// �I�u�W�F�N�g�̒��S�_����ʂ̂ǐ^�񒆂Ȃ̂ŁA
	// ����[����}�E�X���W���擾���Ă�̂ł���ɍ��킹��
	double saveX = (cursor.x - SCREEN_WIDTH / 2) / (SCREEN_WIDTH / 2);
	double saveY = (-cursor.y + SCREEN_HEIGHT / 2) / (SCREEN_HEIGHT / 2);

	// ��ʃT�C�Y�ƒ��_�V�F�[�_�[�Őݒ肵�Ă���摜�̑傫���̍��������킹��
	saveX *= 640.0f / 480.0f;
	saveY *= 480.0f / 640.0f;

	// �������������ϐ����I�u�W�F�N�g��X���W��Y���W�ɒu��������
	this->mSprite->mCenter.x = saveX;
	this->mSprite->mCenter.y = saveY;

	AnimHitObject::Update();
}