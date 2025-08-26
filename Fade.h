#pragma once
#include"StaticObject.h"

extern ID3D11ShaderResourceView* gpTextureFe01;//フェードアウト画像01のテクスチャ
extern DWORD gDeltaTime;

class Fade
{
public:
	StaticObject* mPanel;//画面にかぶせる
	Fade();
	~Fade();

	void Update();
	void Draw();

	//開始関数
	void FadeIn();
	void FadeOut();

	//現在のフェードの状態
	enum 
	{
		NONE,//何もしていない
		FADE_IN,//フェードイン中
		FADE_OUT,//フェードアウト中
	};

	int mState;
};

