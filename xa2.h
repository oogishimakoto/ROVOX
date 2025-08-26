//=============================================================================
//
// �T�E���h���� [xa2.h]
//
//=============================================================================
#ifndef _XAUDIO2_H_
#define _XAUDIO2_H_

#include <xaudio2.h>

// �T�E���h�t�@�C��
typedef enum
{
	SOUND_LABEL_BGM000 = 0,		// �^�C�g��BGM
	SOUND_LABEL_BGM001,			// �X�e�[�WselectBGM
	SOUND_LABEL_BGM002,			// 1-1~1-2�X�e�[�WBGM
	SOUND_LABEL_BGM003,			// 1-3�X�e�[�WBGM
	SOUND_LABEL_BGM004,			// 2-1~2-2�X�e�[�WBGM
	SOUND_LABEL_BGM005,			// 2-3�X�e�[�WBGM
	SOUND_LABEL_SE000,			// �W�����vSE
	SOUND_LABEL_SE001,			// �}�E�XclickSE
	SOUND_LABEL_SE002,			// UI�I������SE
	SOUND_LABEL_SE003,			// ����SE
	SOUND_LABEL_SE004,			// �����_�E��SE
	SOUND_LABEL_SE005,			// �X�e�[�WUISE
	SOUND_LABEL_SE006,			// �T���v��SE
	SOUND_LABEL_SE007,			// �T���v��SE
	SOUND_LABEL_SE008,			// �Q�[���N���ASE
	SOUND_LABEL_SE009,			// �y�[�WSE
	SOUND_LABEL_SE010,			// ������SE
	SOUND_LABEL_SE011,			// �莆SE
	SOUND_LABEL_MAX,
} SOUND_LABEL;

//*****************************************************************************
// �v���g�^�C�v�錾
//*****************************************************************************

// �Q�[�����[�v�J�n�O�ɌĂяo���T�E���h�̏���������
HRESULT XA_Initialize(void);

// �Q�[�����[�v�I����ɌĂяo���T�E���h�̉������
void XA_Release(void);

// �����Ŏw�肵���T�E���h���Đ�����+���ʂ�ݒ肷��f�t�H���g�i1.0f�j
void XA_Play(SOUND_LABEL label, float Volume);

// �����Ŏw�肵���T�E���h���~����
void XA_Stop(SOUND_LABEL label);

// �����Ŏw�肵���T�E���h�̍Đ����ĊJ����
void XA_Resume(SOUND_LABEL label);

#endif
