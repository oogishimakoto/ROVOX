#pragma once
#include "BaseScene.h"
#include "StaticObject.h"

class RetryScene :
	public BaseScene
{
public:
	RetryScene();

	~RetryScene();

	void Update();//�X�V����
	void Draw();//�`�揈��

private:
	ID3D11ShaderResourceView* gpTextureMouse01;			//�}�E�X�摜�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureMouse02;			//�}�E�X�摜�̃e�N�X�`��

#define MAX_DOLL_BG 8
	// ���g���C�摜
	ID3D11ShaderResourceView* gpTextureRetryBg;
	ID3D11ShaderResourceView* gpTextureDoll_bg[MAX_DOLL_BG];

	StaticObject* ReyryBg;	// �w�i
	StaticObject* Doll_Bg;	// �h�[���w�i

	float mTime = 0.0f;

#define ANIMATIONVALUES 18

	//�ړ��p�A�j���[�V�����ϐ�
	//�ړ����̃A�j���[�V�����̐؂�ւ��ԍ�
	int AnimCoount = 0;

	//�A�j���[�V�����؂�ւ����x
	float AnimSpeed = 12.6f;

	//�ړ��p�A�j���[�V�����ϐ�
	//�ړ����̃A�j���[�V�����̐؂�ւ��ԍ�
	float AnimCoountValues = 0.0f;
};

