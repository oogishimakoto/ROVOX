#pragma once
#include "BaseScene.h"
#include "AnimHitObject.h"
#include"Dool.h"

class GameSelectScene :
	public BaseScene
{
public:

	GameSelectScene();

	~GameSelectScene();

	void Update();//�X�V����
	void Draw();//�`�揈��

private:
	//�V�[���ŕK�v�Ȃ���
	ID3D11ShaderResourceView* gpTextureDool01;//�h�[���摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureSelect01;//result�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureSelect02;//select�pUI�摜02�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureSelect03;//select�pUI�摜03�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureSelect04;//select�pUI�摜04�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureStageSelect01;//result�摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureStageSelectMove01;//result�摜01�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureStageSelectMove02;//result�摜01�̃e�N�X�`��

	ID3D11ShaderResourceView* gpTextureBackGround;//�w�i�摜01�̃e�N�X�`��


	ID3D11ShaderResourceView* gpTextureMouse01;			//�}�E�X�摜�̃e�N�X�`��
	ID3D11ShaderResourceView* gpTextureMouse02;			//�}�E�X�摜�̃e�N�X�`��

		//CSprite�N���X�̕ϐ�
	StaticObject* stageSelect;

	StaticObject* backGround;

	AnimHitObject* select;
	AnimHitObject* select2;
	AnimHitObject* select3;
	AnimHitObject* select4;

	AnimHitObject* selectMoveR;
	AnimHitObject* selectMoveL;
};

