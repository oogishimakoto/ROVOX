
// 頂点シェーダーのメイン関数
//役割：各頂点を一律、移動、回転、拡大縮小する
// 戻り値：このシェーダーの処理の結果を、頂点のデータとして戻す
// 引数 inputPos：VRAMに転送された頂点データ（のうち１つ）が渡される

//return用の構造体
struct VS_OUTPUT
{
	float4 pos : SV_Position;
	float2 uv : TEXCOORD;
	float4 col : COLOR;
};

VS_OUTPUT vs_main(float4 inputPos : POSITION,float2 uv : TEX,float4 col : COL)
{
	VS_OUTPUT output;

	output.pos = inputPos;  //return用の変数に移し替える
	output.uv = uv;
	output.col = col;

	//拡大縮小　画面全体に行うので、カメラのズーム機能のようになる
	/*output.pos.x *= 1.0f;
	output.pos.y *= 1.0f;*/

	//画面が横長なので表示の比率を調整する
	output.pos.x *= 480.0f / 640.0f;
	output.pos.y *= 640.0f / 480.0f;
	//output.pos.x *= 1080.0f / 1920.0f;
	//output.pos.x *= 1.0f / 1.0f;

    return output;
}