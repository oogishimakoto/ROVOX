#include "AnimationSprite.h"

extern DWORD gDeltaTime;//�O���[�o���ϐ��̒��������

void AnimationSprite::Update()
{		
	//�L�����N�^�[����
	//float moveSpeed = 0.001f;
	float moveDistance = mAnimSpeed * gDeltaTime;//�ړ����鋗��

	//�Ō�̃R�}�𒴂�����߂�
	if (mAnimTable[(int)mAnimCnt] == -1)
	{
		mAnimCnt = 0.0f;
	}

	float frameYoko = mAnimTable[(int)mAnimCnt];//�e�[�u���̃f�[�^����R�}�ԍ������o��	

	float frameTate = mDirection;//�L�����̕������c�̃R�}�ԍ��ɂ���B

	XY2D uv;

	/*uv.w = 0.33f;
	uv.h = 0.25f;*/
	uv.w = 1;
	uv.h = 1;

	float uLeft = frameYoko * uv.w;
	float uRight = uLeft + uv.w;
	float vTop = frameTate * uv.h;
	float vBottom = vTop + uv.h;

	//charX,charY���X�v���C�g�̒��S�_
	//���S�_����4���_�̍��W���v�Z����
	float charWidth = mSize.x;//�L�����N�^�[�̉��̒���
	float charHight = mSize.y;//�L�����N�^�[�̏c�̒���
	float xLeft = mCenter.x - charWidth / 2.0f;//�X�v���C�g�̍��[x
	float xRight = xLeft + charWidth;//�X�v���C�g�̉E�[x
	float yTop = mCenter.y + charHight / 2.0f;//�X�v���C�g�̏�[y
	float yBottom = yTop - charHight;//�X�v���C�g�̉��[y

	//DIRECT3D�\���̂ɃA�N�Z�X����
	DIRECT3D* d3d = Direct3D_Get();

	VERTEX2D vx[6];

	//���_�f�[�^�����߂�
	vx[0] = { xLeft, yTop, uLeft, vTop,mColor };//����
	vx[1] = { xRight, yTop, uRight, vTop ,mColor};//�E��
	vx[2] = { xRight, yBottom, uRight, vBottom ,mColor};//�E��
	vx[3] = vx[2];//�E��
	vx[4] = { xLeft, yBottom, uLeft,vBottom,mColor };//����
	vx[5] = vx[0];//����

	//���_�f�[�^��VRAM�ɑ���(�w�i01)
	d3d->context->UpdateSubresource(mVertexBuffer, 0, NULL, vx,//����f�[�^�̔z��(=�A�h���X)
		0, 0);
}
