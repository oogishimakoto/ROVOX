#pragma once
//----------------------------------------------------------------------------------
// ヘッダーファイルの取得
#include"WindowProja.h"
#include"Direct3d.h"			// Direct3D設定
#include"WICTextureLoader.h"	// テクスチャ読み込み
#include"input.h"				// 入力情報
#include"xa2.h"					// 音楽
#include<debugapi.h>
#include<stdio.h>
#include <windowsx.h>
#include<stdlib.h>
#include<time.h>				// 時間取得

// ゲーム管理
#include"GameManager.h"

// fps
#include "FrameRateCalculator.h"	// fps固定クラス

#pragma comment(lib,"winmm.lib")//timeGetTime関数のため

//----------------------------------------------------------------------------------
// マクロ定義
#ifdef _DEBUG
#define SCREEN_WIDTH (1920)//ウィンドウの幅
#define SCREEN_HEIGHT (1080)//ウィンドウの高さ
#else
#define SCREEN_WIDTH (1920 * 0.8f)//ウィンドウの幅
#define SCREEN_HEIGHT (1080 * 0.8f)//ウィンドウの高さ
#endif // _DEBUG

#define FPS (60)			// 固定fpsの数値

//定数定義
#define CLASS_NAME "HEWエレガント"	// ウィンドウクラスの名前
#define WINDOW_NAME "GearDoll_isELEGANT!!!"	// ウィンドウバーに表示する名前

// FPSオブジェクト生成
//fps計測用オブジェクト
FrameRateCalculator fr;
// fpsオブジェクトで使う変数
int fps = FPS;	// 設定数値fpsで動作させる
int cnt = 0;	// fps加算用変数

//マウスクリック関係
//今クリックしているか
bool clickCheck = false;
//前のフレームのクリック情報を保存する
bool clickCheckAfter = false;
//一個でもクリックしているか
bool clickOnce[MAX_GEAR] = { false };

//デルタタイム用変数
DWORD gDeltaTime;

// ゲーム管理
GameManager Game;

//座標保存
POINT cursor;

bool endcheck = false;