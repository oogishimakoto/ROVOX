//***********************************************************************
// �}�E�X���W�ɉ摜��`�悷��N���X
// R5_01_14 Yasuda
//***********************************************************************
#pragma once
#include "AnimHitObject.h"

class Operation :
	public AnimHitObject
{
public:
	//�X�V����
	void Update() override;

private:

//----------------------------------------------------------------------------------
// �}�N����`
#ifdef _DEBUG
#define SCREEN_WIDTH (1920.0f)//�E�B���h�E�̕�
#define SCREEN_HEIGHT (1080.0f)//�E�B���h�E�̍���
#else
#define SCREEN_WIDTH (1920 * 0.8f)//�E�B���h�E�̕�
#define SCREEN_HEIGHT (1080 * 0.8f)//�E�B���h�E�̍���
#endif // _DEBUG

};

