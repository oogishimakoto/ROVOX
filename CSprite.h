#pragma once

#include"Direct3d.h"

//スプライトの描画を担当する
class CSprite
{
public:
	//コンストラクタ(初期化)
	CSprite();

	//デストラクタ(解放)
	~CSprite();

	//毎ループの処理
	virtual void Update();

	//描画処理,設定
	void Draw();

	//外部からテクスチャを受け取る関数
	void SetTexture(ID3D11ShaderResourceView* pTexture);

	//このスプライトで使うテクスチャ変数
	ID3D11ShaderResourceView* mTexture;

	//このスプライトで使う頂点バッファ変数
	ID3D11Buffer* mVertexBuffer;

	// 内部クラス
	// 色を表す構造体
	struct RGBA
	{
		float r, g, b, a;
	};

	//頂点データ用の構造体
	struct VERTEX2D
	{
		float x, y;//頂点の座標
		float u, v;//テクスチャのUV座標
		RGBA Color;
	};

	struct XY2D
	{
		float x, y,w,h;
	};	

	XY2D mCenter;
	XY2D mSize;

	// 頂点の色を持っておく変数
	RGBA mColor;

	// 頂点の色を設定する関数
	void SetColor(RGBA color);

	//回転
	float rotation = 0;
};

