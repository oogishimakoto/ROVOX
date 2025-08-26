#pragma once
#include"Direct3d.h"
#include<math.h>

// �����N���X
	// �F��\���\����
struct RGBA
{
	float r, g, b, a;
};

//���_�f�[�^�p�̍\����
struct VERTEX2D
{
	float x, y;//���_�̍��W
	float u, v;//�e�N�X�`����UV���W
	RGBA Color;
};

struct XY2D
{
	float x, y, w, h;
};

class RotateSprite
{
public:
	//�R���X�g���N�^
	RotateSprite();

	//�f�X�g���N�^
	~RotateSprite();

	//�����[�v�̏���
	virtual void Update();

	//�`�揈��,�ݒ�
	void Draw();

	//�O������e�N�X�`�����󂯎��֐�
	void SetTexture(ID3D11ShaderResourceView* pTexture);

	// ���_�̐F��ݒ肷��֐�
	void SetColor(RGBA color);

	//���̃X�v���C�g�Ŏg���e�N�X�`���ϐ�
	ID3D11ShaderResourceView* mTexture;

	//���̃X�v���C�g�Ŏg�����_�o�b�t�@�ϐ�
	ID3D11Buffer* mVertexBuffer;	

	XY2D mCenter;
	XY2D mSize;

	// ���_�̐F�������Ă����ϐ�
	RGBA mColor;	

	//��]���邩�ǂ���
	//true���
	bool DoRotate = true;

	//���x�񂷂�
	float rotation = 0;

	//��葱���邩�ǂ���
	//true��葱����
	bool DoingRotate = true;

	//��鑬�x
	float rotateSpeed = 0.75f;
};

