#include "AnimationSprite.h"

extern DWORD gDeltaTime;//グローバル変数の中を見れる

void AnimationSprite::Update()
{		
	//キャラクター操作
	//float moveSpeed = 0.001f;
	float moveDistance = mAnimSpeed * gDeltaTime;//移動する距離

	//最後のコマを超えたら戻す
	if (mAnimTable[(int)mAnimCnt] == -1)
	{
		mAnimCnt = 0.0f;
	}

	float frameYoko = mAnimTable[(int)mAnimCnt];//テーブルのデータからコマ番号を取り出す	

	float frameTate = mDirection;//キャラの方向を縦のコマ番号にする。

	XY2D uv;

	/*uv.w = 0.33f;
	uv.h = 0.25f;*/
	uv.w = 1;
	uv.h = 1;

	float uLeft = frameYoko * uv.w;
	float uRight = uLeft + uv.w;
	float vTop = frameTate * uv.h;
	float vBottom = vTop + uv.h;

	//charX,charYがスプライトの中心点
	//中心点から4頂点の座標を計算する
	float charWidth = mSize.x;//キャラクターの横の長さ
	float charHight = mSize.y;//キャラクターの縦の長さ
	float xLeft = mCenter.x - charWidth / 2.0f;//スプライトの左端x
	float xRight = xLeft + charWidth;//スプライトの右端x
	float yTop = mCenter.y + charHight / 2.0f;//スプライトの上端y
	float yBottom = yTop - charHight;//スプライトの下端y

	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	VERTEX2D vx[6];

	//頂点データを決める
	vx[0] = { xLeft, yTop, uLeft, vTop,mColor };//左上
	vx[1] = { xRight, yTop, uRight, vTop ,mColor};//右上
	vx[2] = { xRight, yBottom, uRight, vBottom ,mColor};//右下
	vx[3] = vx[2];//右下
	vx[4] = { xLeft, yBottom, uLeft,vBottom,mColor };//左下
	vx[5] = vx[0];//左上

	//頂点データをVRAMに送る(背景01)
	d3d->context->UpdateSubresource(mVertexBuffer, 0, NULL, vx,//送るデータの配列名(=アドレス)
		0, 0);
}
