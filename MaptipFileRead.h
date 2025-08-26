#pragma once
#include<stdlib.h>
#include<stdio.h>
#include<vector>
#include <iostream>
#include <string>
#include <sstream>
#include<fstream>
#include<stdexcept>
#include"MapObject.h"

using namespace std;

extern ID3D11ShaderResourceView* gpTextureFe01;//�t�F�[�h�A�E�g�摜01�̃e�N�X�`��
extern ID3D11ShaderResourceView* gpTextureMapTipWall;//�}�b�v�`�b�v�p�摜
extern ID3D11ShaderResourceView* gpTextureMapTipHasigo;//�}�b�v�`�b�v�p�摜
extern ID3D11ShaderResourceView* gpTextureBox;			//���̉摜�̃e�N�X�`��
extern ID3D11ShaderResourceView* gpTextureBox2;			//���̉摜�̃e�N�X�`��
extern ID3D11ShaderResourceView* gpTextureBox3;			//���̉摜�̃e�N�X�`��
extern ID3D11ShaderResourceView* gpTextureGoal;			//���̉摜�̃e�N�X�`��
extern ID3D11ShaderResourceView* gpTextureKey;			//���̉摜�̃e�N�X�`��
extern ID3D11ShaderResourceView* gpTextureMapTipHouse03;	//�}�b�v�`�b�v�p�摜��q

enum MAPNO
{
	NONE,
	STAGE,
	HASHIGO,
	BOX,
	GOAL,
	KEY,
	MOVEX,
	MOVEY,
	MOVEX2,
	MOVEX3,
	MOVEX4,
	MOVEX5,
	NOTHASHIGO
};

class MaptipFileRead
{
public:
	//�R���X�g���N�^
	//�����F�ǂݍ��݂����t�@�C���̖��O
	MaptipFileRead(const char* const _file_name)
		:file_path{ _file_name }, fileData{ nullptr }
	{
	}

	//�f�X�g���N�^
	//�ǂݍ��񂾃t�@�C�������
	~MaptipFileRead();

	//�ǂݍ��񂾃f�[�^�̃A�h���X��int�^�ϒ��z��̃|�C���^�Ƃ��ĕԂ�
	inline int* data(void) const { return this->fileData->data(); }

	//�ǂݍ��񂾃f�[�^�̗v�f�����擾
	inline const size_t size(void) const { return this->fileData->size(); }

	//�t�@�C���̒��g��ǂ�
	int readFile();

	//�}�b�v�I�u�W�F�N�g�Ɋi�[����
	void mapObjSet();

	//�}�b�v�X�N���[��
	//�h�[���̈ړ����x�ňړ�����
	void mapScroll(float _speed);

	vector<MapObject*>mapObj;

	//���𓮂�������
	void MovefloorX();
	void MovefloorX2();
	void MovefloorX3();
	void MovefloorX4();
	void MovefloorX5();
	void MovefloorY();

private:
	//�t�@�C���|�C���^
	FILE *fp;
	errno_t error;	

	//�t�@�C���̒��g�i�[�ϐ�
	vector<int>*fileData;

	const char* const file_path;	//�e�L�X�g�t�@�C���̃p�X(�t�@�C����)		

	//�ړ����鋗����
	float MovePosX = 50.0f;
	float MovePosX2 = 50.0f;
	float MovePosX3 = 50.0f;
	float MovePosX4 = 50.0f;
	float MovePosX5 = 50.0f;

	//�ړ����鋗����
	float MovePosY = 50.0f;

	//�ړ������
	//���̉񐔕������ړ��������s��
	int MoveCount = 100;

	//������ڂ̈ړ���������
	int countX = 0;
	int countX2 = 0;
	int countX3 = 0;
	int countX4 = 0;
	int countX5 = 0;

	int countY = 0;
};

