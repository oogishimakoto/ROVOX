#pragma once
#include "BaseScene.h"
#include "StaticObject.h"

class RetryScene :
	public BaseScene
{
public:
	RetryScene();

	~RetryScene();

	void Update();//更新処理
	void Draw();//描画処理

private:
	ID3D11ShaderResourceView* gpTextureMouse01;			//マウス画像のテクスチャ
	ID3D11ShaderResourceView* gpTextureMouse02;			//マウス画像のテクスチャ

#define MAX_DOLL_BG 8
	// リトライ画像
	ID3D11ShaderResourceView* gpTextureRetryBg;
	ID3D11ShaderResourceView* gpTextureDoll_bg[MAX_DOLL_BG];

	StaticObject* ReyryBg;	// 背景
	StaticObject* Doll_Bg;	// ドール背景

	float mTime = 0.0f;

#define ANIMATIONVALUES 18

	//移動用アニメーション変数
	//移動時のアニメーションの切り替え番号
	int AnimCoount = 0;

	//アニメーション切り替え速度
	float AnimSpeed = 12.6f;

	//移動用アニメーション変数
	//移動時のアニメーションの切り替え番号
	float AnimCoountValues = 0.0f;
};

