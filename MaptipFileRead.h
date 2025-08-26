#pragma once
#include<stdlib.h>
#include<stdio.h>
#include<vector>
#include <iostream>
#include <string>
#include <sstream>
#include<fstream>
#include<stdexcept>
#include"MapObject.h"

using namespace std;

extern ID3D11ShaderResourceView* gpTextureFe01;//フェードアウト画像01のテクスチャ
extern ID3D11ShaderResourceView* gpTextureMapTipWall;//マップチップ用画像
extern ID3D11ShaderResourceView* gpTextureMapTipHasigo;//マップチップ用画像
extern ID3D11ShaderResourceView* gpTextureBox;			//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureBox2;			//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureBox3;			//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureGoal;			//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureKey;			//箱の画像のテクスチャ
extern ID3D11ShaderResourceView* gpTextureMapTipHouse03;	//マップチップ用画像梯子

enum MAPNO
{
	NONE,
	STAGE,
	HASHIGO,
	BOX,
	GOAL,
	KEY,
	MOVEX,
	MOVEY,
	MOVEX2,
	MOVEX3,
	MOVEX4,
	MOVEX5,
	NOTHASHIGO
};

class MaptipFileRead
{
public:
	//コンストラクタ
	//引数：読み込みたいファイルの名前
	MaptipFileRead(const char* const _file_name)
		:file_path{ _file_name }, fileData{ nullptr }
	{
	}

	//デストラクタ
	//読み込んだファイルを解放
	~MaptipFileRead();

	//読み込んだデータのアドレスをint型可変長配列のポインタとして返す
	inline int* data(void) const { return this->fileData->data(); }

	//読み込んだデータの要素数を取得
	inline const size_t size(void) const { return this->fileData->size(); }

	//ファイルの中身を読む
	int readFile();

	//マップオブジェクトに格納する
	void mapObjSet();

	//マップスクロール
	//ドールの移動速度で移動する
	void mapScroll(float _speed);

	vector<MapObject*>mapObj;

	//床を動かす処理
	void MovefloorX();
	void MovefloorX2();
	void MovefloorX3();
	void MovefloorX4();
	void MovefloorX5();
	void MovefloorY();

private:
	//ファイルポインタ
	FILE *fp;
	errno_t error;	

	//ファイルの中身格納変数
	vector<int>*fileData;

	const char* const file_path;	//テキストファイルのパス(ファイル名)		

	//移動する距離横
	float MovePosX = 50.0f;
	float MovePosX2 = 50.0f;
	float MovePosX3 = 50.0f;
	float MovePosX4 = 50.0f;
	float MovePosX5 = 50.0f;

	//移動する距離下
	float MovePosY = 50.0f;

	//移動する回数
	//この回数分だけ移動処理を行う
	int MoveCount = 100;

	//今何回目の移動か数える
	int countX = 0;
	int countX2 = 0;
	int countX3 = 0;
	int countX4 = 0;
	int countX5 = 0;

	int countY = 0;
};

