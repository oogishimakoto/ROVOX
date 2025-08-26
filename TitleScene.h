#pragma once
#include "BaseScene.h"
#include "AnimHitObject.h"
#include"Dool.h"

class TitleScene :
	public BaseScene
{
public:	

	TitleScene();

	~TitleScene();

	void Update();//更新処理
	void Draw();//描画処理

private:
	//シーンで必要なもの
	ID3D11ShaderResourceView* gpTextureDool01;//ドール画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureTitle01;//result画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureTitleLogo;//result画像01のテクスチャ


	ID3D11ShaderResourceView* gpTextureHosi01;//result画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureMouse01;			//マウス画像のテクスチャ
	ID3D11ShaderResourceView* gpTextureMouse02;			//マウス画像のテクスチャ

	//CSpriteクラスの変数
	StaticObject* Titele;

	StaticObject* Titlelogo;

	AnimHitObject* StartLogo;

	AnimHitObject* endLogo;
};

