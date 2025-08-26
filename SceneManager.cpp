#include "SceneManager.h"

//始めは空に
BaseScene * SceneManager::m_pScene = 0;

extern DWORD gDeltaTime;

SceneManager::SceneManager()
{	
	m_pScene = new TitleScene();
}

SceneManager::~SceneManager()
{	
	delete m_pScene;
}

void SceneManager::ChanegeScene(SCENE scene)
{
	if (m_pScene != 0)
	{
		delete m_pScene;
	}

	//引数のシーンを現在のシーンにする
	switch (scene)
	{
	case SceneManager::TITLE:

		m_pScene = new TitleScene();
		
		break;

	case SceneManager::GAMESELECT:

		m_pScene = new GameSelectScene();

		break;
	case SceneManager::GAME:

		m_pScene = new GameScene();

		break;
	case SceneManager::RESULT:

		m_pScene = new ResultScene();

		break;

	case SceneManager::STAGE1_1:

		m_pScene = new Stage1_1();
		break;

	case SceneManager::STAGE1_2:

		m_pScene = new Stage1_2();
		break;

	case SceneManager::STAGE1_3:

		m_pScene = new Stage1_3();
		break;

	case SceneManager::GAMESELECT2:
		m_pScene = new GameSelectScene2();
		break;

	case SceneManager::STAGE2_1:
		m_pScene = new Stage2_1();
		break;

	case SceneManager::STAGE2_2:
		m_pScene = new Stage2_2();
		break;

	case SceneManager::STAGE2_3:
		m_pScene = new Stage2_3();
	break;

	case SceneManager::GAMESELECT3:
		m_pScene = new GameSelectScene3();
		break;

	case SceneManager::STAGE3_1:
		m_pScene = new Stage3_1();
		break;

	case SceneManager::STAGE3_2:
		m_pScene = new Stage3_2();
		break;

	case SceneManager::STAGE3_3:
		m_pScene = new Stage3_3();
		break;

	case SceneManager::RETRY:
		m_pScene = new RetryScene();
		break;

	default:
		break;
	}
}

void SceneManager::Update()
{	
	if (gDeltaTime >= 100)
	{
		gDeltaTime = 0;
	}
	if (gDeltaTime <= 0)
	{
		gDeltaTime = 1;
	}

	//現在のシーンの更新処理
	m_pScene->Update();
}

void SceneManager::Draw()
{
	//DIRECT3D構造体にアクセスする
	DIRECT3D* d3d = Direct3D_Get();

	//画面を塗りつぶす
	float color[4] = { 0.0f,0.0f,0.0f,1.0f };
	d3d->context->ClearRenderTargetView(d3d->renderTarget, color);

	//現在のシーンの描画処理
	m_pScene->Draw();

	//ダブルバッファを切り替える
	d3d->swapChain->Present(0, 0);
}