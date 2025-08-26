#include "CSprite.h"

CSprite::CSprite()
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

CSprite::~CSprite()
{
	//���̃N���X�ō쐬�����I�u�W�F�N�g���������

	//���_�o�b�t�@���
	COM_SAFE_RELEASE(mVertexBuffer);
}

void CSprite::Update()
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

	//���_�f�[�^�����߂�
	vx[0] = { xLeft, yTop, 0.0f, 0.0f,mColor };//����
	vx[1] = { xRight, yTop, 1.0f, 0.0f,mColor };//�E��
	vx[2] = { xRight, yBottom, 1.0f, 1.0f,mColor };//�E��
	vx[3] = vx[2];//�E��
	vx[4] = { xLeft, yBottom, 0.0f, 1.0f,mColor };//����
	vx[5] = vx[0];//����

	//DIRECT3D�\���̂ɃA�N�Z�X����
	DIRECT3D* d3d = Direct3D_Get();

	//���_�f�[�^��VRAM�ɑ���(�w�i01)
	d3d->context->UpdateSubresource(mVertexBuffer, 0, NULL, vx,//����f�[�^�̔z��(=�A�h���X)
		0, 0);
}

void CSprite::Draw()
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

void CSprite::SetTexture(ID3D11ShaderResourceView * pTexture)
{
	this->mTexture = pTexture;
}

void CSprite::SetColor(RGBA color)
{
	mColor = color;
}
