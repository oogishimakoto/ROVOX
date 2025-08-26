#include "Fade.h"

Fade::Fade()
{
	mPanel = new StaticObject();
	mPanel->SetSize(2.8f, 2.0f);
	mPanel->mSprite->SetTexture(gpTextureFe01);
	CSprite::RGBA color = { 0.0f,0.0f,0.0f,1.0f };
	mPanel->mSprite->SetColor(color);
	mState = NONE;
}

Fade::~Fade()
{
	delete mPanel;
}

void Fade::Update()
{
	float& alpha = mPanel->mSprite->mColor.a;

	const float speed = 0.001f;//�t�F�[�h���x

	//mState�̒l�ɂ���āA�t�F�[�h�������s��
	switch (mState)
	{
	case NONE:
		break;
	case FADE_IN:		
		//�p�l����RGBA��A�̒l�����炷
		alpha -= 0.001f * gDeltaTime;

		//alpha��0.0f�ȉ��ɂȂ�����I��
		if (alpha <= 0.0f)
			mState = NONE;

		break;
	case FADE_OUT:
		//�p�l����RGBA��A�̒l�𑝂₷
		alpha += 0.001f * gDeltaTime;

		//alpha��1.0f�ȏ�ɂȂ�����I��
		if (alpha >= 1.0f)
			mState = NONE;

		break;
	default:
		break;
	}

	mPanel->Update();
}

void Fade::Draw()
{
	mPanel->Draw();
}

void Fade::FadeIn()
{
	if(mState==NONE)
		mState = FADE_IN;
}

void Fade::FadeOut()
{
	if(mState == NONE)
		mState = FADE_OUT;
}
