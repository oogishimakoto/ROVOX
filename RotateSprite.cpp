#include "RotateSprite.h"

#define PI 3.141592653589793	// 円周率

//回転するために
//中心点は回転に入れない

RotateSprite::RotateSprite()
{
	HRESULT hr;
	DIRECT3D* d3d = Direct3D_Get(); // まずDIRECT3D構造体にアクセス

	D3D11_BUFFER_DESC bufferDesc;
	bufferDesc.ByteWidth = sizeof(VERTEX2D) * 6; // VRAMに確保するデータサイズ。通常は今から送る頂点データのサイズ。
	//bufferDesc.ByteWidth = sizeof(quad); // VRAMに確保するデータサイズ。通常は今から送る頂点データのサイズ。
	bufferDesc.Usage = D3D11_USAGE_DEFAULT;
	bufferDesc.BindFlags = D3D11_BIND_VERTEX_BUFFER; // 頂点バッファ作成をここで指定。
	bufferDesc.CPUAccessFlags = 0;
	bufferDesc.MiscFlags = 0;
	bufferDesc.StructureByteStride = 0;

	// バッファ作成関数（デバイスクラスのメンバ関数）を呼び出し
	hr = d3d->device->CreateBuffer(&bufferDesc, NULL, &mVertexBuffer);

	if (FAILED(hr))
	{
		// 頂点バッファの作成に失敗したときの処理をここに書く
	}

	mCenter.x = 0;
	mCenter.y = 0;
	mSize.x = 1.0f;
	mSize.y = 1.0f;

	mColor = { 1.0f,1.0f,1.0f,1.0f };
}

RotateSprite::~RotateSprite()
{
	//このクラスで作成したオブジェクトを解放する

	//頂点バッファ解放
	COM_SAFE_RELEASE(mVertexBuffer);
}

void RotateSprite::Update()
{
	//charX,charYがスプライトの中心点
	//中心点から4頂点の座標を計算する
	float charWidth = mSize.x;//キャラクターの横の長さ
	float charHight = mSize.y;//キャラクターの縦の長さ
	float xLeft = mCenter.x - charWidth / 2.0f;//スプライトの左端x
	float xRight = xLeft + charWidth;//スプライトの右端x
	float yTop = mCenter.y + charHight / 2.0f;//スプライトの上端y
	float yBottom = yTop - charHight;//スプライトの下端y

	VERTEX2D vx[6];

	//廻り続けか
	if (DoingRotate)
	{
		rotation += rotateSpeed;
	}	

	//回転の軸
	float cx = this->mCenter.x;
	float cy = this->mCenter.y;

	//回転
	//左上
	float xd = ((cos(PI / 180 * rotation) * (xLeft - cx)) - (sin(PI / 180 * rotation) * (yTop - cy))) + cx;
	float yd = ((sin(PI / 180 * rotation) * (xLeft - cx)) + (cos(PI / 180 * rotation) * (yTop - cy))) + cy;
	//右下
	float xd1 = ((cos(PI / 180 * rotation) * (xRight - cx)) - (sin(PI / 180 * rotation) * (yBottom - cy))) + cx;
	float yd1 = ((sin(PI / 180 * rotation) * (xRight - cx)) + (cos(PI / 180 * rotation) * (yBottom - cy))) + cy;
	//左下
	float xd2 = ((cos(PI / 180 * rotation) * (xLeft - cx)) - (sin(PI / 180 * rotation) * (yBottom - cy))) + cx;
	float yd2 = ((sin(PI / 180 * rotation) * (xLeft - cx)) + (cos(PI / 180 * rotation) * (yBottom - cy))) + cy;
	//右上
	float xd3 = ((cos(PI / 180 * rotation) * (xRight - cx)) - (sin(PI / 180 * rotation) * (yTop - cy))) + cx;
	float yd3 = ((sin(PI / 180 * rotation) * (xRight - cx)) + (cos(PI / 180 * rotation) * (yTop - cy))) + cy;

	////回転
	////左上
	//float xd = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	////右下
	//float xd1 = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd1 = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	////左下
	//float xd2 = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd2 = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	////右上
	//float xd3 = (cos(PI / 180 * rotation) * (cx)) - (sin(PI / 180 * rotation) * (cy));
	//float yd3 = (sin(PI / 180 * rotation) * (cx)) + (cos(PI / 180 * rotation) * (cy));
	//
	//回すかどうか
	if (DoRotate)
	{
		//回転
		//頂点データを決める
		vx[0] = { xd, yd, 0.0f, 0.0f,mColor };//左上
		vx[1] = { xd3, yd3, 1.0f, 0.0f,mColor };//右上
		vx[2] = { xd1, yd1, 1.0f, 1.0f,mColor };//右下
		vx[3] = vx[2];//右下
		vx[4] = { xd2, yd2, 0.0f, 1.0f,mColor };//左下
		vx[5] = vx[0];//左上
	}
	else
	{
		//頂点データを決める
		vx[0] = { xLeft, yTop, 0.0f, 0.0f,mColor };//左上
		vx[1] = { xRight, yTop, 1.0f, 0.0f,mColor };//右上
		vx[2] = { xRight, yBottom, 1.0f, 1.0f,mColor };//右下
		vx[3] = vx[2];//右下
		vx[4] = { xLeft, yBottom, 0.0f, 1.0f,mColor };//左下
		vx[5] = vx[0];//左上
	}

	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	//頂点データをVRAMに送る(背景01)
	d3d->context->UpdateSubresource(mVertexBuffer, 0, NULL, vx,//送るデータの配列名(=アドレス)
		0, 0);
}

void RotateSprite::Draw()
{
	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	//**************************************************************
	//背景の描画
	//描画に使う頂点バッファを選択する
	UINT stride = sizeof(VERTEX2D);
	UINT offset = 0;

	//描画に使う頂点バッファを選択する
	d3d->context->IASetVertexBuffers(0, 1, &mVertexBuffer, &stride, &offset);

	//ピクセルシェーダーに使うテクスチャを渡す
	d3d->context->PSSetShaderResources(0, 1, &mTexture);

	//サンプラーステートを設定（セット）
	d3d->context->PSSetSamplers(0, 1, &d3d->samplerPoint);//ポイント補完

	//描画命令
	d3d->context->Draw(6, 0);//第一引数は頂点数
	//***************************************************************

}

void RotateSprite::SetTexture(ID3D11ShaderResourceView * pTexture)
{
	mTexture = pTexture;
}

void RotateSprite::SetColor(RGBA color)
{
	mColor = color;
}
