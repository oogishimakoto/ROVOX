#pragma once
#include "SceneManager.h"

// ゲーム全体の枠となるクラス。
// このクラスにゲーム内で必要となるオブジェクトを持たせる。
// このクラスはstaticなクラスとして設計するため、
// このクラスのpublicメンバーにはプログラムのどこからでもアクセスできる。

class GameManager
{

public: // クラスの公開要素

	GameManager();

	// メンバー関数
	void Initialize();	// ゲームの初期化処理
	void Update();		// 更新処理
	void Draw();		// 描画処理
	void Relesase();	// 解放処理

private: // クラスの非公開要素
	SceneManager* sceneMg;

	//同じシーンを何回もnewしないようにする
	int sceneNoAfetr = 0;

	enum SCENE_ID
	{
		NONE,  // どの画面でもない状態
		TITLE, // タイトル画面
		STAGESELECT, // ステージ画面
		STAGE, // ステージ画面
		CLEAR,
		END,//終わり
		STAGE1_1,
		STAGE1_2,
		STAGE1_3,
		STAGESELECT2, // ステージ画面
		STAGE2_1,
		STAGE2_2,
		STAGE2_3,
		STAGESELECT3, // ステージ画面
		STAGE3_1,
		STAGE3_2,
		STAGE3_3,
		RETRY,
	};

	// ステージセレクトを一回でも入ったか？
	bool SelectChosen = false;
};
