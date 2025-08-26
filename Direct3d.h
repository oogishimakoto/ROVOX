#ifndef DIRECT3D_H

#define DIRECT3D_H

#include"WindowProja.h"
#include<d3d11.h>//DirectX11ヘッダファイル

//解放マクロの定義
#define COM_SAFE_RELEASE(o) if(o!=NULL){o->Release();o=NULL;}

//Direct3D関係をまとめる構造体
typedef struct
{
	ID3D11Device* device;//デバイス 機能を使えるようにする役割
	ID3D11DeviceContext* context;//コンテキスト
	IDXGISwapChain* swapChain;//スワップチェイン
	ID3D11RenderTargetView* renderTarget;//レンダーターゲット

	ID3D11VertexShader* vertexShader;//頂点シェーダー
	ID3D11PixelShader* pixelShader;//ピクセルシェーダ
	ID3D11InputLayout* inputLayout;//インプットレイアウト

	ID3D11BlendState* blendAlpha;//アルファブレンディング用ブレンドステート
	ID3D11SamplerState* samplerPoint;//ポイント補完のサンプラーステート
}DIRECT3D;

//プロトタイプ宣言

//DirectX3Dの初期化関数
BOOL Direct3D_Init(HWND hWnd);

//DirectX3Dの解放関数
void Direct3D_Release();

//DIRECT3D構造体の実体のアドレスを返す関数
DIRECT3D* Direct3D_Get();

#endif