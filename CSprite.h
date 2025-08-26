#pragma once

#include"Direct3d.h"

//�X�v���C�g�̕`���S������
class CSprite
{
public:
	//�R���X�g���N�^(������)
	CSprite();

	//�f�X�g���N�^(���)
	~CSprite();

	//�����[�v�̏���
	virtual void Update();

	//�`�揈��,�ݒ�
	void Draw();

	//�O������e�N�X�`�����󂯎��֐�
	void SetTexture(ID3D11ShaderResourceView* pTexture);

	//���̃X�v���C�g�Ŏg���e�N�X�`���ϐ�
	ID3D11ShaderResourceView* mTexture;

	//���̃X�v���C�g�Ŏg�����_�o�b�t�@�ϐ�
	ID3D11Buffer* mVertexBuffer;

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
		float x, y,w,h;
	};	

	XY2D mCenter;
	XY2D mSize;

	// ���_�̐F�������Ă����ϐ�
	RGBA mColor;

	// ���_�̐F��ݒ肷��֐�
	void SetColor(RGBA color);

	//��]
	float rotation = 0;
};

