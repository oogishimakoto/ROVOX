#pragma once
#include "BaseScene.h"
#include "AnimHitObject.h"
#include"Dool.h"
class GameSelectScene2 :
	public BaseScene
{
public:

	GameSelectScene2();

	~GameSelectScene2();

	void Update();//更新処理
	void Draw();//描画処理

private:
	//シーンで必要なもの
	ID3D11ShaderResourceView* gpTextureDool01;//ドール画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureSelect01;//result画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureSelect02;//result画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureSelect03;//result画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureSelect04;//result画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureStageSelectMove01;//select１へ飛ぶように画像01のテクスチャ
	ID3D11ShaderResourceView* gpTextureStageSelectMove02;//select１へ飛ぶように画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureStageSelect01;//result画像01のテクスチャ

	ID3D11ShaderResourceView* gpTextureBackGround;//背景画像01のテクスチャ


	ID3D11ShaderResourceView* gpTextureMouse01;			//マウス画像のテクスチャ
	ID3D11ShaderResourceView* gpTextureMouse02;			//マウス画像のテクスチャ

		//CSpriteクラスの変数
	StaticObject* stageSelect;

	StaticObject* backGround;

	AnimHitObject* select;
	AnimHitObject* select2;
	AnimHitObject* select3;	
	AnimHitObject* select4;
	AnimHitObject* selectMoveR;
	AnimHitObject* selectMoveL;

};

