#include "RotateSprite.h"

#define PI 3.141592653589793	// �~����

//��]���邽�߂�
//���S�_�͉�]�ɓ���Ȃ�

RotateSprite::RotateSprite()
{
	HRESULT hr;
	DIRECT3D* d3d = Direct3D_Get(); // �܂�DIRECT3D�\���̂ɃA�N�Z�X

	D3D11_BUFFER_DESC bufferDesc;
	bufferDesc.ByteWidth = sizeof(VERTEX2D) * 6; // VRAM�Ɋm�ۂ���f�[�^�T�C�Y�B�ʏ�͍����瑗�钸�_�f�[�^�̃T�C�Y�B
	//bufferDesc.ByteWidth = sizeof(quad); // VRAM�Ɋm�ۂ���f�[�^�T�C�Y�B�ʏ�͍����瑗�钸�_�f�[�^�̃T�C�Y�B
	bufferDesc.Usage = D3D11_USAGE_DEFAULT;
	bufferDesc.BindFlags = D3D11_BIND_VERTEX_BUFFER; // ���_�o�b�t�@�쐬�������Ŏw��B
	bufferDesc.CPUAccessFlags = 0;
	bufferDesc.MiscFlags = 0;
	bufferDesc.StructureByteStride = 0;

	// �o�b�t�@�쐬�֐��i�f�o�C�X�N���X�̃����o�֐��j���Ăяo��
	hr = d3d->device->CreateBuffer(&bufferDesc, NULL, &mVertexBuffer);

	if (FAILED(hr))
	{
		// ���_�o�b�t�@�̍쐬�Ɏ��s�����Ƃ��̏����������ɏ���
	}

	mCenter.x = 0;
	mCenter.y = 0;
	mSize.x = 1.0f;
	mSize.y = 1.0f;

	mColor = { 1.0f,1.0f,1.0f,1.0f };
}

RotateSprite::~RotateSprite()
{
	//���̃N���X�ō쐬�����I�u�W�F�N�g���������

	//���_�o�b�t�@���
	COM_SAFE_RELEASE(mVertexBuffer);
}

void RotateSprite::Update()
{
	//charX,charY���X�v���C�g�̒��S�_
	//���S�_����4���_�̍��W���v�Z����
	float charWidth = mSize.x;//�L�����N�^�[�̉��̒���
	float charHight = mSize.y;//�L�����N�^�[�̏c�̒���
	float xLeft = mCenter.x - charWidth / 2.0f;//�X�v���C�g�̍��[x
	float xRight = xLeft + charWidth;//�X�v���C�g�̉E�[x
	float yTop = mCenter.y + charHight / 2.0f;//�X�v���C�g�̏�[y
	float yBottom = yTop - charHight;//�X�v���C�g�̉��[y

	VERTEX2D vx[6];

	//��葱����
	if (DoingRotate)
	{
		rotation += rotateSpeed;
	}	

	//��]�̎�
	float cx = this->mCenter.x;
	float cy = this->mCenter.y;

	//��]
	//����
	float xd = ((cos(PI / 180 * rotation) * (xLeft - cx)) - (sin(PI / 180 * rotation) * (yTop - cy))) + cx;
	float yd = ((sin(PI / 180 * rotation) * (xLeft - cx)) + (cos(PI / 180 * rotation) * (yTop - cy))) + cy;
	//�E��
	float xd1 = ((cos(PI / 180 * rotation) * (xRight - cx)) - (sin(PI / 180 * rotation) * (yBottom - cy))) + cx;
	float yd1 = ((sin(PI / 180 * rotation) * (xRight - cx)) + (cos(PI / 180 * rotation) * (yBottom - cy))) + cy;
	//����
	float xd2 = ((cos(PI / 180 * rotation) * (xLeft - cx)) - (sin(PI / 180 * rotation) * (yBottom - cy))) + cx;
	float yd2 = ((sin(PI / 180 * rotation) * (xLeft - cx)) + (cos(PI / 180 * rotation) * (yBottom - cy))) + cy;
	//�E��
	float xd3 = ((cos(PI / 180 * rotation) * (xRight - cx)) - (sin(PI / 180 * rotation) * (yTop - cy))) + cx;
	float yd3 = ((sin(PI / 180 * rotation) * (xRight - cx)) + (cos(PI / 180 * rotation) * (yTop - cy))) + cy;

	////��]
	////����
	//float xd = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	////�E��
	//float xd1 = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd1 = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	////����
	//float xd2 = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd2 = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	////�E��
	//float xd3 = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd3 = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	//
	//�񂷂��ǂ���
	if (DoRotate)
	{
		//��]
		//���_�f�[�^�����߂�
		vx[0] = { xd, yd, 0.0f, 0.0f,mColor };//����
		vx[1] = { xd3, yd3, 1.0f, 0.0f,mColor };//�E��
		vx[2] = { xd1, yd1, 1.0f, 1.0f,mColor };//�E��
		vx[3] = vx[2];//�E��
		vx[4] = { xd2, yd2, 0.0f, 1.0f,mColor };//����
		vx[5] = vx[0];//����
	}
	else
	{
		//���_�f�[�^�����߂�
		vx[0] = { xLeft, yTop, 0.0f, 0.0f,mColor };//����
		vx[1] = { xRight, yTop, 1.0f, 0.0f,mColor };//�E��
		vx[2] = { xRight, yBottom, 1.0f, 1.0f,mColor };//�E��
		vx[3] = vx[2];//�E��
		vx[4] = { xLeft, yBottom, 0.0f, 1.0f,mColor };//����
		vx[5] = vx[0];//����
	}

	//DIRECT3D�\���̂ɃA�N�Z�X����
	DIRECT3D* d3d = Direct3D_Get();

	//���_�f�[�^��VRAM�ɑ���(�w�i01)
	d3d->context->UpdateSubresource(mVertexBuffer, 0, NULL, vx,//����f�[�^�̔z��(=�A�h���X)
		0, 0);
}

void RotateSprite::Draw()
{
	//DIRECT3D�\���̂ɃA�N�Z�X����
	DIRECT3D* d3d = Direct3D_Get();

	//**************************************************************
	//�w�i�̕`��
	//�`��Ɏg�����_�o�b�t�@��I������
	UINT stride = sizeof(VERTEX2D);
	UINT offset = 0;

	//�`��Ɏg�����_�o�b�t�@��I������
	d3d->context->IASetVertexBuffers(0, 1, &mVertexBuffer, &stride, &offset);

	//�s�N�Z���V�F�[�_�[�Ɏg���e�N�X�`����n��
	d3d->context->PSSetShaderResources(0, 1, &mTexture);

	//�T���v���[�X�e�[�g��ݒ�i�Z�b�g�j
	d3d->context->PSSetSamplers(0, 1, &d3d->samplerPoint);//�|�C���g�⊮

	//�`�施��
	d3d->context->Draw(6, 0);//�������͒��_��
	//***************************************************************

}

void RotateSprite::SetTexture(ID3D11ShaderResourceView * pTexture)
{
	mTexture = pTexture;
}

void RotateSprite::SetColor(RGBA color)
{
	mColor = color;
}
