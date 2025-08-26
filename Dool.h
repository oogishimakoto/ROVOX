#pragma once
#include"AnimHitObject.h"
#include"Gear.h"
#include"GameScene.h"

#define ANIMATIONVALUES 60

class StaticObject;

class Dool : public AnimHitObject
{
private:		
	//�M�A�͂߂�ꏊ���Ԗڂ�
	int mGcount = 0;
	//�t��]���ǂ���
	bool mReverse = true;

	//�����x
	float acc = -0.00001f;

	//�W�����v�ł��邩
	bool canAcc = true;

	//y�����̑��x
	float vel = -0.15f;

	//�������傫���Ȃ��ƒn�ʂ��痣��Ȃ��̂ň�x�����l�������������
	float onceVelAdd = 0.017f;
	//�����ꂽ���`�F�b�N����悤
	bool onceVelCheck = false;

	//�W�����v�ł��邩
	bool canJump = false;
	bool JumpState = false;

	float old_posX = 0;
	float old_posY = 0;	

	//�W�����v�p�A�j���[�V�����ϐ�
	int AnimJumpCount = 0;
	float AnimJumpSpeed = 0.7f;
	float AnimJumpChangeValues = 0.0f;	

	//�������x
	float walk = 8.0f;

	//�����A�j���[�V�����̔���
	bool canWalkAnim = false;

	//�ύX���Ȃ����x=Set���Ȃ�
	//����̒l���}�b�v�X�N���[����܂��ړ�������Ƃ��ɑ������
	float speed = 800.0f;

	//���鎞�̉����x
	float dashAcc = 1.0f;

	StaticObject* talkObj;

public:

	//�X�V����
	void Update() override;

	//�`�揈��
	void Draw() override;

	//������
	Dool();

	//���
	~Dool();

	//�s��
	void Act();

	//�M�A����
	void GeraIdCheck();

	//�M�A�i�[
	void GearDist(GEAR_ID _id);

	//�_�b�V��
	void Dashu();

	//�W�����v
	void Jump();

	//�Z�b�^�[
	void SetmReverse(bool _reverse);

	void SetOldPosX(float);
	void SetOldPosY(float);

	void SetCanJump(bool _jump);

	void SetAcc(float _acc);
	void SetVel(float _vel);

	void SetWalk(float _walk);

	void SetAnimJumpCoount(int _count);
	void SetAnimJumpChangeValues(float _values);

	void SetAccCheck(bool _acc);

	void SetCanWalkAnim(bool _WalkAnim);	//�h�[���̕����A�j���[�V�������ł��邩�̔�����Z�b�g

	//�Q�b�^�[
	float GetOldPosX();
	float GetOldPosY();

	bool GetCanJump();

	float GetSpeed();

	float GetWalk();

	//�M�A�����ׂĊO��(aoi��)
	void GearDelete();

	//�w�肵���M�A���������炻�̒l��Ԃ�
	//�Ȃ�������0��Ԃ�
	//�����F�~�����M�A�̔ԍ�
	int GetGearNo(int _no);

	//�ړ��p�A�j���[�V�����ϐ�
	//�ړ����̃A�j���[�V�����̐؂�ւ��ԍ�
	int AnimWalkCoount = 0;

	//�A�j���[�V�����؂�ւ����x
	float AnimWalkSpeed = 1.4f;

	//�A�j���[�V�����؂�ւ���l
	float AnimWalkChangeValues = 0.0f;

	//�M�A���͂߂�Ƃ���
	GEAR_ID mGearSlot[3];

	//��q��o�邩�ǂ����ňړ����邩���߂�
	bool UpHashigo = false;

	//�o��p�A�j���[�V�����ϐ�
	int AnimNoboruCount = 0;
	float AnimNoboruSpeed = 0.05f;
	float AnimNoboruChangeValues = 0.0f;

	//�h�[���̍s���̗D�揇
	//�u�o�遨�W�����v�������������v�̏��ԂɗD�悵�ĉ摜���A�j���[�V����������
	bool priority = true;

	//�W�����v�A�j���[�V�����̔���
	bool canJumpAnim = true;

	//������̍������痎������R�P��摜�ɕς��ăM�A���O��
	float HighAcc;

	float HighAccCheck = 0.4f;	// �_�E��4�i
	float JumpHighAccCheck = 2.0f;	//�W�����v4�i

	// �_�E������֐�
	bool GearCrash();
	
	//se�J�E���g�p
	bool jump_seCount = true;
	bool walk_seCount = true;

	//��b�p�̕ϐ�
	int talk = 0;
};

