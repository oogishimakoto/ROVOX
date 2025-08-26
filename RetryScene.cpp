#include "RetryScene.h"

//switch�p���ʔԍ�
extern int sceneNo;

extern int oldsceneNo;

RetryScene::RetryScene()
{
	//�}�E�X�摜�ǂݍ���
	LoadTexture(L"assets/MouseN.png", &gpTextureMouse01);
	LoadTexture(L"assets/MouseY.png", &gpTextureMouse02);

	//�}�E�X���̉�
	gpMouse = new Operation();
	gpMouse->mSprite->SetTexture(gpTextureMouse01);
	gpMouse->SetSize(0.35f, 0.35f);

	// ���U���g�e�N�X�`��
	LoadTexture(L"assets/Now_loading/nowloading.png", &gpTextureRetryBg);
	ReyryBg = new StaticObject();
	ReyryBg->mSprite->SetTexture(gpTextureRetryBg);
	ReyryBg->mSprite->mCenter.x = 0.0f;
	ReyryBg->mSprite->mCenter.y = 0.0f;
	ReyryBg->SetSize(2.66f, 1.5f);

	LoadTexture(L"assets/Now_loading/walk_1.png", &gpTextureDoll_bg[0]);
	LoadTexture(L"assets/Now_loading/walk_2.png", &gpTextureDoll_bg[1]);
	LoadTexture(L"assets/Now_loading/walk_3.png", &gpTextureDoll_bg[2]);
	LoadTexture(L"assets/Now_loading/walk_4.png", &gpTextureDoll_bg[3]);
	LoadTexture(L"assets/Now_loading/walk_5.png", &gpTextureDoll_bg[4]);
	LoadTexture(L"assets/Now_loading/walk_6.png", &gpTextureDoll_bg[5]);
	LoadTexture(L"assets/Now_loading/walk_7.png", &gpTextureDoll_bg[6]);
	LoadTexture(L"assets/Now_loading/walk_8.png", &gpTextureDoll_bg[7]);
	Doll_Bg = new StaticObject();
	Doll_Bg->mSprite->SetTexture(gpTextureDoll_bg[0]);
	Doll_Bg->mSprite->mCenter.x = 0.72f;
	Doll_Bg->mSprite->mCenter.y = -0.35f;
	Doll_Bg->SetSize(0.75f, 0.75f);

	mTime = 0.0f;

	//�ړ��p�A�j���[�V�����ϐ�
	//�ړ����̃A�j���[�V�����̐؂�ւ��ԍ�
	AnimCoount = 0;

	//�A�j���[�V�����؂�ւ����x
	AnimSpeed = 1.4f;

	//�ړ��p�A�j���[�V�����ϐ�
	//�ړ����̃A�j���[�V�����̐؂�ւ��ԍ�
	AnimCoountValues = 0.0f;
}

RetryScene::~RetryScene()
{
	// �}�E�X���
	delete gpMouse;

	// �h�[���w�i���
	delete Doll_Bg;

	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureMouse02);

	COM_SAFE_RELEASE(gpTextureRetryBg);

	for(int i = 0; i < MAX_DOLL_BG; i++)
		COM_SAFE_RELEASE(gpTextureDoll_bg[i]);
}

void RetryScene::Update()
{
	mTime++;
	//�}�E�X�N���b�N�����特��炷
	if (Input_GetKeyTrigger(VK_LBUTTON))
	{
		XA_Play(SOUND_LABEL_SE001, 0.45f);
	}

	if (Input_GetKeyDown(VK_LBUTTON))
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse02);
	}
	else
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse01);
	}

	//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
	AnimCoountValues += AnimSpeed;

	if (AnimCoountValues > ANIMATIONVALUES)
	{
		AnimCoount++;

		if (AnimCoount == 0)
		{
			Doll_Bg->mSprite->mCenter.x = 0.72f;
		}
		if (AnimCoount == 1)
		{
			Doll_Bg->mSprite->mCenter.x = 0.48f;
		}
		if (AnimCoount == 2)
		{
			Doll_Bg->mSprite->mCenter.x = 0.24f;
		}
		if (AnimCoount == 3)
		{
			Doll_Bg->mSprite->mCenter.x = 0.0f;
		}
		if (AnimCoount == 4)
		{
			Doll_Bg->mSprite->mCenter.x = -0.24f;
		}
		if (AnimCoount == 5)
		{
			Doll_Bg->mSprite->mCenter.x = -0.48f;
		}
		if (AnimCoount == 6)
		{
			Doll_Bg->mSprite->mCenter.x = -0.72f;
		}
		if (AnimCoount == 7)
		{
			Doll_Bg->mSprite->mCenter.x = -0.96f;
		}
		if (AnimCoount == 8)
		{
			AnimCoount = 0;
			sceneNo = oldsceneNo;
		}
		AnimCoountValues = 0;

	}
	Doll_Bg->mSprite->SetTexture(gpTextureDoll_bg[AnimCoount]);

	ReyryBg->Update();
	Doll_Bg->Update();

	gpMouse->Update();
}

void RetryScene::Draw()
{
	//DIRECT3D�\���̂ɃA�N�Z�X����
	DIRECT3D* d3d = Direct3D_Get();

	ReyryBg->Draw();
	Doll_Bg->Draw();
	gpMouse->Draw();
}
