#pragma once
#include<Windows.h>
#include<stdio.h>
#define WINCOUNT 3

// �֐��̃v���g�^�C�v�錾
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

void Game_Draw();

//�Q�[���̏������������߂̊֐�
void Game_Update();

//�Q�[���̏�����
void Game_Init();

//void DrawQuad(float x, float y ,float p,int count);

extern struct VERTEX2D;

