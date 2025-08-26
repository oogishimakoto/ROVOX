#pragma once
#include "BaseScene.h"
#include "AnimHitObject.h"
#include"Dool.h"

class TitleScene :
	public BaseScene
{
public:	

	TitleScene();

	~TitleScene();

	void Update();//�X�V����
	void Draw();//�`�揈��

private:
	//�V�[���ŕK�v�Ȃ���
	ID3D11ShaderResourceView* gpTextureDool01;//�h�[���摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureTitle01;//result�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureTitleLogo;//result�摜01�̃e�N�X�`��


	ID3D11ShaderResourceView* gpTextureHosi01;//result�摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureMouse01;			//�}�E�X�摜�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureMouse02;			//�}�E�X�摜�̃e�N�X�`��

	//CSprite�N���X�̕ϐ�
	StaticObject* Titele;

	StaticObject* Titlelogo;

	AnimHitObject* StartLogo;

	AnimHitObject* endLogo;
};

