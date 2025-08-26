#pragma once
#include<Windows.h>
#include<stdio.h>
#define WINCOUNT 3

// 関数のプロトタイプ宣言
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

void Game_Draw();

//ゲームの処理を書くための関数
void Game_Update();

//ゲームの初期化
void Game_Init();

//void DrawQuad(float x, float y ,float p,int count);

extern struct VERTEX2D;

