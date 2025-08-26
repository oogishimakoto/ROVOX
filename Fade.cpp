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

	const float speed = 0.001f;//フェード速度

	//mStateの値によって、フェード処理を行う
	switch (mState)
	{
	case NONE:
		break;
	case FADE_IN:		
		//パネルのRGBAのAの値を減らす
		alpha -= 0.001f * gDeltaTime;

		//alphaが0.0f以下になったら終了
		if (alpha <= 0.0f)
			mState = NONE;

		break;
	case FADE_OUT:
		//パネルのRGBAのAの値を増やす
		alpha += 0.001f * gDeltaTime;

		//alphaが1.0f以上になったら終了
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
