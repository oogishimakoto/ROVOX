//=============================================================================
//
// サウンド処理 [xa2.h]
//
//=============================================================================
#ifndef _XAUDIO2_H_
#define _XAUDIO2_H_

#include <xaudio2.h>

// サウンドファイル
typedef enum
{
	SOUND_LABEL_BGM000 = 0,		// タイトルBGM
	SOUND_LABEL_BGM001,			// ステージselectBGM
	SOUND_LABEL_BGM002,			// 1-1~1-2ステージBGM
	SOUND_LABEL_BGM003,			// 1-3ステージBGM
	SOUND_LABEL_BGM004,			// 2-1~2-2ステージBGM
	SOUND_LABEL_BGM005,			// 2-3ステージBGM
	SOUND_LABEL_SE000,			// ジャンプSE
	SOUND_LABEL_SE001,			// マウスclickSE
	SOUND_LABEL_SE002,			// UI選択決定SE
	SOUND_LABEL_SE003,			// 歩くSE
	SOUND_LABEL_SE004,			// 落下ダウンSE
	SOUND_LABEL_SE005,			// ステージUISE
	SOUND_LABEL_SE006,			// サンプルSE
	SOUND_LABEL_SE007,			// サンプルSE
	SOUND_LABEL_SE008,			// ゲームクリアSE
	SOUND_LABEL_SE009,			// ページSE
	SOUND_LABEL_SE010,			// 動く床SE
	SOUND_LABEL_SE011,			// 手紙SE
	SOUND_LABEL_MAX,
} SOUND_LABEL;

//*****************************************************************************
// プロトタイプ宣言
//*****************************************************************************

// ゲームループ開始前に呼び出すサウンドの初期化処理
HRESULT XA_Initialize(void);

// ゲームループ終了後に呼び出すサウンドの解放処理
void XA_Release(void);

// 引数で指定したサウンドを再生する+音量を設定するデフォルト（1.0f）
void XA_Play(SOUND_LABEL label, float Volume);

// 引数で指定したサウンドを停止する
void XA_Stop(SOUND_LABEL label);

// 引数で指定したサウンドの再生を再開する
void XA_Resume(SOUND_LABEL label);

#endif
