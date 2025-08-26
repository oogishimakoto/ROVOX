#pragma once
#include<iostream>
#include"Direct3d.h"
#include"WICTextureLoader.h"
#include"Operation.h"

// ���͏��擾
#include"input.h"
// �T�E���h
#include "xa2.h"

using namespace std;

extern bool clickCheck;
extern bool clickCheckAfter;

#define SCREEN_WIDTH (1920)//�E�B���h�E�̕�
#define SCREEN_HEIGHT (1080)//�E�B���h�E�̍���

class BaseScene
{
private:
	int Time = 0;
	int TimeCount = 0;
public:
	//������
	BaseScene();
	//���
	virtual ~BaseScene();

	//���ꂼ��̃V�[���N���X�̓��e�����s����
	virtual void Update() = 0;
	virtual void Draw() = 0;

	// �e�N�X�`����ǂݍ��ފ֐�
	// �������F�e�N�X�`���̃t�@�C����
	// �������F�ǂݍ��񂾃e�N�X�`�����i�[����ϐ��̃A�h���X
	void LoadTexture(const wchar_t* fileName,
		ID3D11ShaderResourceView** outTexture);	

	void Stop(int _time);
	void StopFlame();

protected:	
	//�}�E�X
	Operation* gpMouse;

	bool TimeCheck = false;
};

