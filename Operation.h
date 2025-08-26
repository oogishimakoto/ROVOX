//***********************************************************************
// マウス座標に画像を描画するクラス
// R5_01_14 Yasuda
//***********************************************************************
#pragma once
#include "AnimHitObject.h"

class Operation :
	public AnimHitObject
{
public:
	//更新処理
	void Update() override;

private:

//----------------------------------------------------------------------------------
// マクロ定義
#ifdef _DEBUG
#define SCREEN_WIDTH (1920.0f)//ウィンドウの幅
#define SCREEN_HEIGHT (1080.0f)//ウィンドウの高さ
#else
#define SCREEN_WIDTH (1920 * 0.8f)//ウィンドウの幅
#define SCREEN_HEIGHT (1080 * 0.8f)//ウィンドウの高さ
#endif // _DEBUG

};

