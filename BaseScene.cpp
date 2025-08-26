#include "BaseScene.h"

BaseScene::BaseScene()
{	
	
}

BaseScene::~BaseScene()
{
}

void BaseScene::LoadTexture(const wchar_t * fileName, ID3D11ShaderResourceView ** outTexture)
{
	HRESULT hr;
	DIRECT3D* d3d = Direct3D_Get();
	// テクスチャを読み込む
	hr = DirectX::CreateWICTextureFromFile(d3d->device,
		fileName,
		NULL,
		outTexture);

	// エラー処理
	if (FAILED(hr)) // 失敗した場合
	{
		MessageBox(NULL, "テクスチャ読み込み失敗", "エラー発生", MB_OK);
	}
}

void BaseScene::Stop(int _time)
{
	Time = _time * 60;
	TimeCheck = true;
}

void BaseScene::StopFlame()
{
	if (TimeCheck == true)
	{
		if (TimeCount < Time)
		{
			TimeCheck = true;
			TimeCount++;
		}
		else
		{
			TimeCount = 0;
			Time = 0;
			TimeCheck = false;
		}
	}
}
