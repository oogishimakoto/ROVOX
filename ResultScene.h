#pragma once
#include "BaseScene.h"
#include"Dool.h"

class ResultScene :
	public BaseScene
{
public:
	//������
	ResultScene();

	//���
	~ResultScene();

	void Update();//�X�V����
	void Draw();//�`�揈��
private:
	//�V�[���ŕK�v�Ȃ���
	ID3D11ShaderResourceView* gpTextureDool01;//�h�[���摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureResult01;//result�摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureBackGround;//�w�i�摜�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureHosi01;//result�摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTexturetitleGoLogo;//result�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureSelectGoLogo;//result�摜01�̃e�N�X�`��


	ID3D11ShaderResourceView* gpTextureMouse01;			//�}�E�X�摜�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureMouse02;			//�}�E�X�摜�̃e�N�X�`��

	//CSprite�N���X�̕ϐ�
	StaticObject* dool;

	StaticObject* result;

	StaticObject* backGround;

	StaticObject* hosi;

	AnimHitObject* titleLogo;
	AnimHitObject* selectLogo;

	//�莆�p
	StaticObject* latter[3];

};

