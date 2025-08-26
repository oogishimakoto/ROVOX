#include "CSprite.h"

CSprite::CSprite()
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

CSprite::~CSprite()
{
	//このクラスで作成したオブジェクトを解放する

	//頂点バッファ解放
	COM_SAFE_RELEASE(mVertexBuffer);
}

void CSprite::Update()
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

	//頂点データを決める
	vx[0] = { xLeft, yTop, 0.0f, 0.0f,mColor };//左上
	vx[1] = { xRight, yTop, 1.0f, 0.0f,mColor };//右上
	vx[2] = { xRight, yBottom, 1.0f, 1.0f,mColor };//右下
	vx[3] = vx[2];//右下
	vx[4] = { xLeft, yBottom, 0.0f, 1.0f,mColor };//左下
	vx[5] = vx[0];//左上

	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	//頂点データをVRAMに送る(背景01)
	d3d->context->UpdateSubresource(mVertexBuffer, 0, NULL, vx,//送るデータの配列名(=アドレス)
		0, 0);
}

void CSprite::Draw()
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

void CSprite::SetTexture(ID3D11ShaderResourceView * pTexture)
{
	this->mTexture = pTexture;
}

void CSprite::SetColor(RGBA color)
{
	mColor = color;
}
