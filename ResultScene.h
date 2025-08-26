#pragma once
#include "BaseScene.h"
#include"Dool.h"

class ResultScene :
	public BaseScene
{
public:
	//初期化
	ResultScene();

	//解放
	~ResultScene();

	void Update();//更新処理
	void Draw();//描画処理
private:
	//シーンで必要なもの
	ID3D11ShaderResourceView* gpTextureDool01;//ドール画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureResult01;//result画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureBackGround;//背景画像のテクスチャ

	ID3D11ShaderResourceView* gpTextureHosi01;//result画像01のテクスチャ

	ID3D11ShaderResourceView* gpTexturetitleGoLogo;//result画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureSelectGoLogo;//result画像01のテクスチャ


	ID3D11ShaderResourceView* gpTextureMouse01;			//マウス画像のテクスチャ
	ID3D11ShaderResourceView* gpTextureMouse02;			//マウス画像のテクスチャ

	//CSpriteクラスの変数
	StaticObject* dool;

	StaticObject* result;

	StaticObject* backGround;

	StaticObject* hosi;

	AnimHitObject* titleLogo;
	AnimHitObject* selectLogo;

	//手紙用
	StaticObject* latter[3];

};

