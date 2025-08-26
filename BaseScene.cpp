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
	// �e�N�X�`����ǂݍ���
	hr = DirectX::CreateWICTextureFromFile(d3d->device,
		fileName,
		NULL,
		outTexture);

	// �G���[����
	if (FAILED(hr)) // ���s�����ꍇ
	{
		MessageBox(NULL, "�e�N�X�`���ǂݍ��ݎ��s", "�G���[����", MB_OK);
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
