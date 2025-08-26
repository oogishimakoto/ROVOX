#pragma once
#include<iostream>
#include"Direct3d.h"
#include"WICTextureLoader.h"
#include"Operation.h"

// 入力情報取得
#include"input.h"
// サウンド
#include "xa2.h"

using namespace std;

extern bool clickCheck;
extern bool clickCheckAfter;

#define SCREEN_WIDTH (1920)//ウィンドウの幅
#define SCREEN_HEIGHT (1080)//ウィンドウの高さ

class BaseScene
{
private:
	int Time = 0;
	int TimeCount = 0;
public:
	//初期化
	BaseScene();
	//解放
	virtual ~BaseScene();

	//それぞれのシーンクラスの内容を実行する
	virtual void Update() = 0;
	virtual void Draw() = 0;

	// テクスチャを読み込む関数
	// 第一引数：テクスチャのファイル名
	// 第二引数：読み込んだテクスチャを格納する変数のアドレス
	void LoadTexture(const wchar_t* fileName,
		ID3D11ShaderResourceView** outTexture);	

	void Stop(int _time);
	void StopFlame();

protected:	
	//マウス
	Operation* gpMouse;

	bool TimeCheck = false;
};

