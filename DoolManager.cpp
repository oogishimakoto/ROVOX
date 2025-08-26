#include "DoolManager.h"

extern DWORD gDeltaTime;

extern bool mapHitChange;

extern int latterCount;

void DoolManager::SetGear(Dool * dool, Gear * gear)
{	
	dool->GearDist(gear->GetGear());

	if (gear->GetGearNo() == 0)
	{
		dool->SetmReverse(false);
	}
}

////とりあえずexternで妥協
extern ID3D11ShaderResourceView* gpTextureDoolFallRight;//右向きにこけるドール画像01のテクスチャ

void DoolManager::StoneGear(AStone * stone, Dool * dool)
{

}


void DoolManager::WallHit(Dool * dool, MaptipFileRead * map )
{
	for (int i = 0; i < map->mapObj.size(); i++)
	{
		//壁かどうか
		if (map->mapObj[i]->mapNo == 1 || map->mapObj[i]->mapNo == 3 || map->mapObj[i]->mapNo == 6 || map->mapObj[i]->mapNo == 8
			|| map->mapObj[i]->mapNo == 9 || map->mapObj[i]->mapNo == 10 || map->mapObj[i]->mapNo == 11)
		{
			//----------------------------------------------------------------------------
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{
				//左
				if ( dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_LEFT && flg == true)
				{

					if (modeScrollChange == true)
					{
						dool->mSprite->mCenter.x = dool->GetOldPosX();
					}

					if (modeScrollChange == false)
					{
						dool->mSprite->mCenter.x = map->mapObj[i]->mSprite->mCenter.x - (map->mapObj[i]->mSprite->mSize.x);
					}

					//壁に当たったらスクロールする処理を止める
					modeScrollChange = true;

					//左に行く
					//mapHitChange = false;
					if (dool->priority == true)
					{
						//押してる状態で当たった		
						if (map->mapObj[i]->mapNo == 3 && dool->GetGearNo(3) == 3)
						{
							//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
							dool->AnimWalkChangeValues += dool->AnimWalkSpeed*gDeltaTime;

							if (dool->AnimWalkChangeValues > 1)
							{
								dool->AnimWalkCoount++;
								//画像番号が超えないようにする
								if (dool->AnimWalkCoount == 8)
								{
									dool->AnimWalkCoount = 0;
								}
								dool->AnimWalkChangeValues = 0;
							}
							//歩くアニメーションをしないようにする
							dool->SetCanWalkAnim(false);

							dool->mSprite->SetTexture(gpTextureDoolPushu[dool->AnimWalkCoount]);

							map->mapObj[i]->mSprite->mCenter.x = dool->mSprite->mCenter.x + (dool->mSprite->mSize.x / 2);
						}
					}
				}

				//右
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_RIGHT)
				{
					if (modeScrollChange == true)
					{
						dool->mSprite->mCenter.x = dool->GetOldPosX();
					}

					if (modeScrollChange == false)
					{
						dool->mSprite->mCenter.x = map->mapObj[i]->mSprite->mCenter.x + (map->mapObj[i]->mSprite->mSize.x);
					}

					//右に行く
					//壁に当たったらスクロールする処理を止める
					modeScrollChange = true;
				}

				//下
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_DOWN)
				{
					//壁に当たったらスクロールする処理を止める
					//地面に着くまでジャンプをできないようにする
					dool->mSprite->mCenter.y = dool->GetOldPosY();
					dool->SetCanJump(false);
				}

				//上
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_UP /*&& map->mapObj[i]->mapNo != 2*/)
				{	
					//壁に当たったらスクロールする処理を止める
					modeScrollChange = true;
					//地面に着いたら重力を初期化する
					dool->mSprite->mCenter.y = dool->GetOldPosY();
					dool->SetAcc(0);
					dool->SetAccCheck(false);
					dool->SetCanJump(true);	
					dool->SetAnimJumpChangeValues(0);
					dool->SetAnimJumpCoount(0);
					dool->canJumpAnim = true;	//ジャンプアニメーションの判定
					dool->jump_seCount = true;	//ジャンプのse用
					dool->walk_seCount = true;
					flg = false;//左に当たらないようにする

					//dool->SetVel(0);

					//一定のの高さより高いところから落ちたらコケる画像に変える
					if (dool->GearCrash() == true)
					{
						dool->mSprite->SetTexture(gpTextureDoolFallRight);
						dool->GearDelete();
						XA_Play(SOUND_LABEL_SE006,0.5f);
					}
										
					dool->HighAcc = 0.0f;
				}		
				else
				{
					flg = true;
				}
			}
			
			//壁にめり込んだら
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{			
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) != dool->mHitSquare->HIT_RIGHT)
				{
					if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) != dool->mHitSquare->HIT_LEFT)
					{
						dool->SetAcc(0);
						dool->SetVel(map->mapObj[i]->mSprite->mCenter.y + (map->mapObj[i]->mSprite->mSize.y * 1.5));
					}
				}
			}
		}

		//梯子があって登るギアがあったら
		if (/*map->mapObj[i]->mSprite->mCenter.x  > dool->mSprite->mCenter.x && */map->mapObj[i]->mapNo == 2 && dool->GetGearNo(1) == 1
			&& dool->mSprite->mCenter.x >= map->mapObj[i]->mSprite->mCenter.x - 0.01f	// 左側の判定
			&& dool->mSprite->mCenter.x <= map->mapObj[i]->mSprite->mCenter.x + 0.01f)	// 右
		{
			dool->walk_seCount = false;
			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->SetWalk(0.0f);
				dool->UpHashigo = true;
				dool->SetAcc(false);
				dool->SetAcc(0.0f);
				dool->canJumpAnim = false;

				dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);
			}

			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->SetWalk(0.0f);
				dool->UpHashigo = true;
				dool->SetAcc(false);
				dool->SetAcc(0.0f);

				dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);

				//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
				dool->AnimNoboruChangeValues += dool->AnimNoboruSpeed*gDeltaTime;

				if (dool->AnimNoboruChangeValues > 1)
				{
					dool->AnimNoboruCount++;
					XA_Play(SOUND_LABEL_SE005, 0.5f);
					//画像番号が超えないようにする
					if (dool->AnimNoboruCount == 8)
					{
						dool->AnimNoboruCount = 0;
					}
					dool->AnimNoboruChangeValues = 0;
				}
				dool->mSprite->SetTexture(gpTextureDoolNoboru[dool->AnimNoboruCount]);

			}
		}

		//梯子があって登るギアがあったら
		if (map->mapObj[i]->mapNo == 13 && dool->GetGearNo(1) == 1 && dool->mSprite->mCenter.x >= map->mapObj[i]->mSprite->mCenter.x - 0.01&&dool->mSprite->mCenter.x <= map->mapObj[i]->mSprite->mCenter.x + 0.01)
		{
			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->SetWalk(0.0f);
				dool->UpHashigo = true;
				dool->SetAcc(false);
				dool->SetAcc(0.0f);
				dool->canJumpAnim = false;
				dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);
			}

			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->SetWalk(0.0f);
				dool->UpHashigo = true;
				dool->SetAcc(false);
				dool->SetAcc(0.0f);
				dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);

				//上
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_UP /*&& map->mapObj[i]->mapNo != 2*/)
				{
					//壁に当たったらスクロールする処理を止める
					modeScrollChange = true;
					//地面に着いたら重力を初期化する
					dool->mSprite->mCenter.y = dool->GetOldPosY();
					dool->SetAcc(0);
					dool->SetAccCheck(false);
					dool->SetCanJump(true);
					dool->SetAnimJumpChangeValues(0);
					dool->SetAnimJumpCoount(0);
				}

				//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
				dool->AnimNoboruChangeValues += dool->AnimNoboruSpeed*gDeltaTime;

				if (dool->AnimNoboruChangeValues > 1)
				{
					dool->AnimNoboruCount++;
					//画像番号が超えないようにする
					if (dool->AnimNoboruCount == 8)
					{
						dool->AnimNoboruCount = 0;
					}
					dool->AnimNoboruChangeValues = 0;
				}
				dool->mSprite->SetTexture(gpTextureDoolNoboru[dool->AnimNoboruCount]);
			}

			if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_UP)
			{
				dool->UpHashigo = true;
				dool->SetVel(map->mapObj[i]->mSprite->mCenter.y + (map->mapObj[i]->mSprite->mSize.y * 1.7));
				dool->SetAcc(0);
				dool->SetAccCheck(false);
				dool->SetCanJump(true);
				dool->SetAnimJumpChangeValues(0);
				dool->SetAnimJumpCoount(0);
				dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);
			}
		}
		//梯子があって登るギアがあったら
		if (map->mapObj[i]->mapNo == 7 && dool->GetGearNo(1) == 1 && dool->mSprite->mCenter.x >= map->mapObj[i]->mSprite->mCenter.x - 0.01&&dool->mSprite->mCenter.x <= map->mapObj[i]->mSprite->mCenter.x + 0.01)
		{
			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->SetWalk(0.0f);
				dool->UpHashigo = true;
				dool->SetAcc(false);
				dool->SetAcc(0.0f);
				if (dool->mSprite->mCenter.y < map->mapObj[i]->mSprite->mCenter.y + (map->mapObj[i]->mSprite->mSize.y / 0.125))
				{
					dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);
				}		
			}

			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->UpHashigo = true;
				dool->SetVel(map->mapObj[i]->mSprite->mCenter.y + (map->mapObj[i]->mSprite->mSize.y * 1.7));
				dool->SetAnimJumpChangeValues(0);
				dool->SetAnimJumpCoount(0);
				dool->SetVel(dool->mSprite->mCenter.y += 0.0005f);
			}

			//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
			dool->AnimNoboruChangeValues += dool->AnimNoboruSpeed*gDeltaTime;

			if (dool->AnimNoboruChangeValues > 1)
			{
				dool->AnimNoboruCount++;
				//画像番号が超えないようにする
				if (dool->AnimNoboruCount == 8)
				{
					dool->AnimNoboruCount = 0;
				}
				dool->AnimNoboruChangeValues = 0;
			}
			dool->mSprite->SetTexture(gpTextureDoolNoboru[dool->AnimNoboruCount]);
		}	
		if (map->mapObj[i]->mapNo == 7 || map->mapObj[i]->mapNo == 2)
		{
			if (dool->mHitCircle->IsHit(map->mapObj[i]->mHitCircle) == true)
			{
				dool->talk = 1;
			}
		}		

		//キャッチが入ってたら
		if (dool->GetGearNo(3) == 3)
		{
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{				
				if (map->mapObj[i]->mapNo == 5)
				{
					//一回当たった番号には当たらない
					if (saveObj != i)
					{
						XA_Play(SOUND_LABEL_SE011, 0.8f);

						//当たった番号を入れる
						saveObj = i;
						keyget = true;
						map->mapObj[i]->active = false;

						//手紙の数を数える
						latterCount++;
					}				
				}
			}
		}
		else
		{
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{
				if (map->mapObj[i]->mapNo == 5)
				{
					dool->talk = 2;
				}
			}
		}
		
		//ゴールに到着
		if (map->mapObj[i]->mapNo == 4 )
		{
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{
				if (keyget == true)
				{
					//シーン変更
					sceneNo = 4;
				}
			}
		}
	}


}

void DoolManager::GoalHit(Dool * dool, Goal * goal,Key* key)
{
	//ゴールに到着
	if (dool->mHitCircle->IsHit(goal->mHitCircle) == true)
	{
		if (key->GetKey())
		{
			//シーン変更
			goal->StageGoal();
		}
	}
}

void DoolManager::KeyHit(Dool * dool, Key * key)
{
	if (dool->mHitCircle->IsHit(key->mHitCircle) == true)
	{
		//キャッチが入ってたら
		if (dool->GetGearNo(3) == 3)
		{
			key->SetKey(true);
			key->mSprite->mCenter.y = 1;
		}
	}
}

void DoolManager::DoolPosMapScroll(Dool * dool, MaptipFileRead * map)
{
	//マップの右端かどうか
	bool mapend = false;

	//マップの左端かどうか
	bool mapstart = false;

	if (map->mapObj[0]->mSprite->mCenter.x > -1.28f)
	{
		mapstart = true;
	}

	if (map->mapObj[74]->mSprite->mCenter.x < 1.25f)
	{
		//マップ端に来たらスクロール止める
		modeScrollChange = true;

		mapend = true;
	}

	//真ん中迄歩いたらマップスクロールに切り替える
	if (dool->mSprite->mCenter.x > 0.0f && mapend == false && mapHitChange == true)
	{
		modeScrollChange = false;

		//マップスクロールする関数を呼ぶ
		if (modeScrollChange == false)
		{
			map->mapScroll(dool->GetWalk());
		}		
	}

	//真ん中より左でマップ左端じゃなくて歩く方向が左向きなら
	if (dool->mSprite->mCenter.x < 0.0f && mapstart == false && mapHitChange == false)
	{
		modeScrollChange = false;
		//マップスクロールする関数を呼ぶ
		if (modeScrollChange == false)
		{
			map->mapScroll(dool->GetWalk());
		}
	}
	
	//真ん中より左ならドールが歩く
	if (dool->mSprite->mCenter.x < 0.0f)
	{
	}

	if (dool->GetGearNo(4) != 4)
	{
	}

	//端に着いたらキャラが移動する
	if (mapend == true)
	{
	}
}
