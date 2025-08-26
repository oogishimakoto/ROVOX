#pragma once
#include"BaseScene.h"
#include"GameScene.h"
#include"ResultScene.h"
#include"TitleScene.h"
#include"Stage1_1.h"
#include"Stage1_2.h"
#include"Stage1_3.h"
#include"GameSelectScene.h"
#include"GameSelectScene2.h"
#include"Stage2_1.h"
#include"Stage2_2.h"
#include"Stage2_3.h"
#include"GameSelectScene3.h"
#include"Stage3_1.h"
#include"Stage3_2.h"
#include"Stage3_3.h"
#include"RetryScene.h"

class SceneManager
{
public:
	enum SCENE
	{
		TITLE,
		GAMESELECT,
		GAME,
		RESULT,
		STAGE1_1,
		STAGE1_2,
		STAGE1_3,
		GAMESELECT2,
		STAGE2_1,
		STAGE2_2,
		STAGE2_3,
		GAMESELECT3,
		STAGE3_1,
		STAGE3_2,
		STAGE3_3,
		RETRY,
	};

	//コンストラクタ
	SceneManager();

	//デストラクタ
	~SceneManager();

	//引数でシーンを変える
	static void ChanegeScene(SCENE scene);

	static void Update();//更新処理
	static void Draw();//描画処理

	//現在のシーン
	static BaseScene* m_pScene;

protected:	
};


