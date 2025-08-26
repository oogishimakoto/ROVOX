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

#define MAX_GEAR 5
#define MAX_GP 8

extern int sceneNo;

class Dool;
class DoolManager;

class GameScene :
	public BaseScene
{
public:
	//������
	GameScene();

	//���
	~GameScene();

	void Update();//�X�V����
	void Draw();//�`�揈��

private:
	//�V�[���ŕK�v�Ȃ���

	//�e�N�X�`���p�̕ϐ�	
	ID3D11ShaderResourceView* gpTextureGear02;			//�M�A�摜02�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureGear03;			//�M�A�摜03�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureBackGround;		//�w�i�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI01;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI02;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI03;			//UI�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureUI04;			//UI�摜01�̃e�N�X�`��
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

	//�}�b�v�`�b�v�p
	MaptipFileRead* maptip;

	//��
	Key* key;

	//�M�A���͂܂����Ƃ��p�摜
	//�O����΂悢����
	RotateObject* Gpgear[3];

	//�X�e�[�W�M�A�p
	//�E�ɓ������p
	RotateObject* StageGearX;
	//��ɓ������p
	RotateObject* StageGearY;

	// �t�F�[�h
	Fade* gpFade;

	//�n���R�������摜
	StaticObject* stanp;

	//true�Ȃ珰���E�ɓ�����
	bool ModeMoveFloorX = false;

	//true�Ȃ珰����ɓ�����
	bool ModeMoveFloorY = false;

};

