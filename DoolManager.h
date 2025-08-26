#pragma once
#include"Dool.h"
#include"Gear.h"
#include"AnimHitObject.h"
#include"AStone.h"
#include"MaptipFileRead.h"
#include"Goal.h"
#include"Key.h"

class Goal;
class Dool;
#define MAX_GP 8

//�����Ă�摜
extern ID3D11ShaderResourceView* gpTextureDoolPushu[MAX_GP];

//�o��
extern ID3D11ShaderResourceView* gpTextureDoolNoboru[MAX_GP];

//�}�b�v���X�N���[�����邩������
extern bool modeScrollChange;

extern bool reverseDool;

extern bool debugMode;

class DoolManager : public AnimHitObject
{
private:
	//�Z�b�g����Ă���M�A�����ׂč폜
	GEAR_ID geardelete;

	MapObject obj[800];

	bool keyget = false;	

public:
	//�h�[���ɃM�A���Z�b�g����
	void SetGear(Dool* dool, Gear* gear);

	//�h�[���Ɛ΂����������̏���
	void StoneGear(AStone* stone, Dool* dool);

	//�}�b�v�̕ǂƓ�����������
	void WallHit(Dool* dool, MaptipFileRead* map);

	//�S�[��������������
	void GoalHit(Dool* dool, Goal* goal, Key* key);

	//���ɓ�����������
	void KeyHit(Dool* dool, Key* key);

	//�h�[���̈ʒu�Ń}�b�v�X�N���[�����邩���߂�
	void DoolPosMapScroll(Dool* dool, MaptipFileRead* map);	

	//��񓖂������炻�̃I�u�W�F�N�g���L������
	int saveObj = 0;

	//�������ł��������悤�ɂ��邷��
	bool flg = true;
};

