#include "GameManager.h"
#include "Direct3D.h"
#include "Input.h"

//switch用識別番号
int sceneNo = 0;

int oldsceneNo = 0;

GameManager::GameManager()
{
	sceneNo = 1;
}

void GameManager::Initialize()
{
	//シーンが別なら
	if (sceneNo != sceneNoAfetr)
	{
		//シーン変更の処理
		switch (sceneNo)
		{
		case NONE:
			break;
		case TITLE:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);
			XA_Stop(SOUND_LABEL_SE008);

			XA_Play(SOUND_LABEL_BGM000,1.0f);
			sceneMg->ChanegeScene(sceneMg->TITLE);
			break;
		case STAGESELECT:
			XA_Stop(SOUND_LABEL_SE008);
			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);

			if(SelectChosen == false)	// 一回目なら
				XA_Play(SOUND_LABEL_BGM001,1.0f);
			else if(SelectChosen == true)	// 一回でも入っていたら
				XA_Resume(SOUND_LABEL_BGM001);
			sceneMg->ChanegeScene(sceneMg->GAMESELECT);
			break;
		case STAGE:
			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);

			XA_Play(SOUND_LABEL_BGM005,1.0f);
			sceneMg->ChanegeScene(sceneMg->GAME);
			break;
		case CLEAR:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);

			XA_Play(SOUND_LABEL_SE008,1.0f);
			sceneMg->ChanegeScene(sceneMg->RESULT);
			break;		
		case STAGE1_1:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);

			XA_Play(SOUND_LABEL_BGM002,0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE1_1);
			break;
		case STAGE1_2:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);

			XA_Play(SOUND_LABEL_BGM002,0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE1_2);
			break;
		case STAGE1_3:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);

			XA_Play(SOUND_LABEL_BGM003,0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE1_3);
			break;

		case STAGESELECT2:
			SelectChosen = true;	// 一回ステージセレクトに入っているのでtrueにする。

			XA_Resume(SOUND_LABEL_BGM001);	// 続きから再生
			sceneMg->ChanegeScene(sceneMg->GAMESELECT2);
			break;
		case STAGE2_1:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM005);

			XA_Play(SOUND_LABEL_BGM004, 0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE2_1);
			break;
		case STAGE2_2:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM005);

			XA_Play(SOUND_LABEL_BGM004, 0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE2_2);
			break;
		case STAGE2_3:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);

			XA_Play(SOUND_LABEL_BGM005, 0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE2_3);
			break;
		case STAGESELECT3:
			SelectChosen = true;	// 一回ステージセレクトに入っているのでtrueにする。

			XA_Resume(SOUND_LABEL_BGM001);	// 続きから再生
			sceneMg->ChanegeScene(sceneMg->GAMESELECT3);
			break;
		case STAGE3_1:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);

			XA_Play(SOUND_LABEL_BGM005, 0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE3_1);
			break;
		case STAGE3_2:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);

			XA_Play(SOUND_LABEL_BGM005, 0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE3_2);
			break;
		case STAGE3_3:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);

			XA_Play(SOUND_LABEL_BGM005, 0.25f);
			sceneMg->ChanegeScene(sceneMg->STAGE3_3);
			break;

		case RETRY:
			SelectChosen = false;	// ステージセレクトのBGMを最初から再生するようにする

			XA_Stop(SOUND_LABEL_BGM000);
			XA_Stop(SOUND_LABEL_BGM001);
			XA_Stop(SOUND_LABEL_BGM002);
			XA_Stop(SOUND_LABEL_BGM003);
			XA_Stop(SOUND_LABEL_BGM004);
			XA_Stop(SOUND_LABEL_BGM005);
			
			sceneMg->ChanegeScene(sceneMg->RETRY);
			break;
		default:
			break;
		}
	}

	sceneNoAfetr = sceneNo;
}

void GameManager::Update()
{
	sceneMg->Update();
}

void GameManager::Draw()
{
	sceneMg->Draw();
}

void GameManager::Relesase()
{
	sceneMg->~SceneManager();
}
