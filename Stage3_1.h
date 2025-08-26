#pragma once
#include "BaseScene.h"
#include"Dool.h"
#include"Gear.h"
#include"GearJanereter.h"
#include"DoolManager.h"
#include"Goal.h"
#include"AStone.h"
#include"MaptipFileRead.h"
#include"Key.h"
#include "StaticObject.h"
#include"Fade.h"

#define MAX_GEAR 75
#define MAX_GP 8

//switch�p���ʔԍ�
extern int sceneNo;

extern int oldsceneNo;

class Dool;
class DoolManager;
class Stage3_1 :
	public BaseScene
{
public:
	//������
	Stage3_1();

	//���
	~Stage3_1();

	void Update();//�X�V����
	void Draw();//�`�揈��

private:

	// pause�p
	ID3D11ShaderResourceView* gpTextureStop;			//Stop�̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepause;			//pause�w�i�̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepauseUI;			//pauseUI�̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepauseGame;		//pause�Q�[���̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepauseRetry;		//pause���g���C�̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepauseTitle;		//pause�^�C�g���̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepauseStect;		//pause�Z���N�g�̃e�N�X�`��	
	ID3D11ShaderResourceView* gpTexturepauseMenu;		//pause�Z���N�g�̃e�N�X�`��	

	HitObject* Stoppause;
	StaticObject* pauseBg;
	StaticObject* pauseUI;
	HitObject* pauseGa;
	HitObject* pauseRe;
	HitObject* pauseTi;
	HitObject* pauseSt;

	//�V�[���ŕK�v�Ȃ���

	//�e�N�X�`���p�̕ϐ�	
	ID3D11ShaderResourceView* gpTextureGear02;			//�M�A�摜02�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureGear03;			//�M�A�摜03�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureGear04;			//�X�e�[�W�M�A�摜03�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureGear05;			//�X�e�[�W�M�A�摜03�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureBackGround;		//�w�i�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI01;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI02;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI03;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI04;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI05;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI06;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI07;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureMouse01;			//�}�E�X�摜�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureMouse02;			//�}�E�X�摜�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureDool01;			//�h�[���摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureDoolFallLeft;	//�������ɂ�����摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureStone;			//�Ή摜01�̃e�N�X�`��	

	//CSprite�N���X�̕ϐ�
	Dool* dool;

	//�M�A����
	GearJanereter* gj;
	GearJanereter* jumpGear;
	GearJanereter* catchiGear;
	GearJanereter* upGear;
	GearJanereter* emptiGear;

	//�h�[���Ǘ��N���X
	DoolManager* doolMg;

	//�S�[��
	//class Goal* goal;

	//��
	AStone* stone;

	//�w�i
	StaticObject* background[2];

	//UI
	HitObject* UI01;
	StaticObject* UI02;
	StaticObject* UI03;
	StaticObject* UI04;
	StaticObject* UI06;
	//�؂�ւ��悤
	HitObject* UI05;

	StaticObject* GearUI01;
	StaticObject* GearUI02;
	StaticObject* GearUI03;
	StaticObject* GearUI04;
	StaticObject* GearUI05;

	//�}�b�v�`�b�v�p
	MaptipFileRead* maptip;

	//��
	Key* key;

	//�M�A���͂܂����Ƃ��p�摜
	//�O����΂悢����
	RotateObject* Gpgear[3];

	//�X�e�[�W�M�A�p
	//�E�ɓ������p
	GearJanereter* StageGearX;
	//��ɓ������p
	GearJanereter* StageGearY;

	// �t�F�[�h
	Fade* gpFade;

	//�n���R�������摜
	StaticObject* stanp;

	//�莆�p
	StaticObject* latter[3];

	HitObject* StageX;
	HitObject* StageX2;
	HitObject* StageX3;
	HitObject* StageY;

	//true�Ȃ珰���E�ɓ�����
	bool ModeMoveFloorX = false;
	bool ModeMoveFloorX2 = false;
	bool ModeMoveFloorX3 = false;

	//true�Ȃ珰����ɓ�����
	bool ModeMoveFloorY = false;

	// �͂߂�X���b�g��Ԃ�����
	bool LightPower = false;

	// �莆��Ԃ�����
	bool LatterLightPower = false;

	//�X�e�[�W�M�A
	bool LightPowerX = false;
	bool LightPowerY = false;

	// �|�[�Y���j���[���H
	bool pausemenu = false;
};





