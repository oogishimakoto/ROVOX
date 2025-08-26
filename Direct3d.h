#ifndef DIRECT3D_H

#define DIRECT3D_H

#include"WindowProja.h"
#include<d3d11.h>//DirectX11�w�b�_�t�@�C��

//����}�N���̒�`
#define COM_SAFE_RELEASE(o) if(o!=NULL){o->Release();o=NULL;}

//Direct3D�֌W���܂Ƃ߂�\����
typedef struct
{
	ID3D11Device* device;//�f�o�C�X �@�\���g����悤�ɂ������
	ID3D11DeviceContext* context;//�R���e�L�X�g
	IDXGISwapChain* swapChain;//�X���b�v�`�F�C��
	ID3D11RenderTargetView* renderTarget;//�����_�[�^�[�Q�b�g

	ID3D11VertexShader* vertexShader;//���_�V�F�[�_�[
	ID3D11PixelShader* pixelShader;//�s�N�Z���V�F�[�_
	ID3D11InputLayout* inputLayout;//�C���v�b�g���C�A�E�g

	ID3D11BlendState* blendAlpha;//�A���t�@�u�����f�B���O�p�u�����h�X�e�[�g
	ID3D11SamplerState* samplerPoint;//�|�C���g�⊮�̃T���v���[�X�e�[�g
}DIRECT3D;

//�v���g�^�C�v�錾

//DirectX3D�̏������֐�
BOOL Direct3D_Init(HWND hWnd);

//DirectX3D�̉���֐�
void Direct3D_Release();

//DIRECT3D�\���̂̎��̂̃A�h���X��Ԃ��֐�
DIRECT3D* Direct3D_Get();

#endif