#pragma once
#include"StaticObject.h"

extern ID3D11ShaderResourceView* gpTextureFe01;//�t�F�[�h�A�E�g�摜01�̃e�N�X�`��
extern DWORD gDeltaTime;

class Fade
{
public:
	StaticObject* mPanel;//��ʂɂ��Ԃ���
	Fade();
	~Fade();

	void Update();
	void Draw();

	//�J�n�֐�
	void FadeIn();
	void FadeOut();

	//���݂̃t�F�[�h�̏��
	enum 
	{
		NONE,//�������Ă��Ȃ�
		FADE_IN,//�t�F�[�h�C����
		FADE_OUT,//�t�F�[�h�A�E�g��
	};

	int mState;
};

