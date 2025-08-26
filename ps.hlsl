// ピクセルシェーダーのメイン関数　→　ポリゴンを描画するのに必要なピクセル数だけ呼び出される
// 各ピクセルの色を指定するのが役目
// 戻り値：　このピクセルを何色にしたいか、RGBAで指定する
// 引数inputPos：　頂点の座標。ピクセルシェーダーではあまり意味を持たない。

//グローバル変数　シェーダーのグローバル変数は読み取り専用
//テクスチャ
Texture2D gTexture : register(t0);

//サンプラー
SamplerState gSampler : register(s0);

float4 ps_main( float4 inputPos : SV_POSITION,float2 uv : TEXCOORD ,float4 col : COLOR) : SV_Target
{
	//float color = inputPos.x / 640.0f;

	//float4 pixelColor = float4(color, color, color, 1.0f); // RGBA  0.0f-1.0f

	//if (inputPos.x < 320.0f)
	//{
	//	pixelColor = float4(1.0f, 0.0f, 1.0f, 1.0f); // RGBA  0.0f-1.0f
	//}
	//else
	//{
	//	pixelColor = float4(1.0f, 1.0f, 0.0f, 1.0f); // RGBA  0.0f-1.0f
	//}

	float4 pixelColor = gTexture.Sample(gSampler,uv);

	// 頂点の色を混ぜる
	pixelColor *= col;

	return pixelColor;
}
