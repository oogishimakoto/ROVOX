#pragma once
//----------------------------------------------------------------------------------
// �w�b�_�[�t�@�C���̎擾
#include"WindowProja.h"
#include"Direct3d.h"			// Direct3D�ݒ�
#include"WICTextureLoader.h"	// �e�N�X�`���ǂݍ���
#include"input.h"				// ���͏��
#include"xa2.h"					// ���y
#include<debugapi.h>
#include<stdio.h>
#include <windowsx.h>
#include<stdlib.h>
#include<time.h>				// ���Ԏ擾

// �Q�[���Ǘ�
#include"GameManager.h"

// fps
#include "FrameRateCalculator.h"	// fps�Œ�N���X

#pragma comment(lib,"winmm.lib")//timeGetTime�֐��̂���

//----------------------------------------------------------------------------------
// �}�N����`
#ifdef _DEBUG
#define SCREEN_WIDTH (1920)//�E�B���h�E�̕�
#define SCREEN_HEIGHT (1080)//�E�B���h�E�̍���
#else
#define SCREEN_WIDTH (1920 * 0.8f)//�E�B���h�E�̕�
#define SCREEN_HEIGHT (1080 * 0.8f)//�E�B���h�E�̍���
#endif // _DEBUG

#define FPS (60)			// �Œ�fps�̐��l

//�萔��`
#define CLASS_NAME "HEW�G���K���g"	// �E�B���h�E�N���X�̖��O
#define WINDOW_NAME "GearDoll_isELEGANT!!!"	// �E�B���h�E�o�[�ɕ\�����閼�O

// FPS�I�u�W�F�N�g����
//fps�v���p�I�u�W�F�N�g
FrameRateCalculator fr;
// fps�I�u�W�F�N�g�Ŏg���ϐ�
int fps = FPS;	// �ݒ萔�lfps�œ��삳����
int cnt = 0;	// fps���Z�p�ϐ�

//�}�E�X�N���b�N�֌W
//���N���b�N���Ă��邩
bool clickCheck = false;
//�O�̃t���[���̃N���b�N����ۑ�����
bool clickCheckAfter = false;
//��ł��N���b�N���Ă��邩
bool clickOnce[MAX_GEAR] = { false };

//�f���^�^�C���p�ϐ�
DWORD gDeltaTime;

// �Q�[���Ǘ�
GameManager Game;

//���W�ۑ�
POINT cursor;

bool endcheck = false;