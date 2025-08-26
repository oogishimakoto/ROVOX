#pragma once
#include "SceneManager.h"

// �Q�[���S�̘̂g�ƂȂ�N���X�B
// ���̃N���X�ɃQ�[�����ŕK�v�ƂȂ�I�u�W�F�N�g����������B
// ���̃N���X��static�ȃN���X�Ƃ��Đ݌v���邽�߁A
// ���̃N���X��public�����o�[�ɂ̓v���O�����̂ǂ�����ł��A�N�Z�X�ł���B

class GameManager
{

public: // �N���X�̌��J�v�f

	GameManager();

	// �����o�[�֐�
	void Initialize();	// �Q�[���̏���������
	void Update();		// �X�V����
	void Draw();		// �`�揈��
	void Relesase();	// �������

private: // �N���X�̔���J�v�f
	SceneManager* sceneMg;

	//�����V�[���������new���Ȃ��悤�ɂ���
	int sceneNoAfetr = 0;

	enum SCENE_ID
	{
		NONE,  // �ǂ̉�ʂł��Ȃ����
		TITLE, // �^�C�g�����
		STAGESELECT, // �X�e�[�W���
		STAGE, // �X�e�[�W���
		CLEAR,
		END,//�I���
		STAGE1_1,
		STAGE1_2,
		STAGE1_3,
		STAGESELECT2, // �X�e�[�W���
		STAGE2_1,
		STAGE2_2,
		STAGE2_3,
		STAGESELECT3, // �X�e�[�W���
		STAGE3_1,
		STAGE3_2,
		STAGE3_3,
		RETRY,
	};

	// �X�e�[�W�Z���N�g�����ł����������H
	bool SelectChosen = false;
};
