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

////�Ƃ肠����extern�őË�
extern ID3D11ShaderResourceView* gpTextureDoolFallRight;//�E�����ɂ�����h�[���摜01�̃e�N�X�`��

void DoolManager::StoneGear(AStone * stone, Dool * dool)
{

}


void DoolManager::WallHit(Dool * dool, MaptipFileRead * map )
{
	for (int i = 0; i < map->mapObj.size(); i++)
	{
		//�ǂ��ǂ���
		if (map->mapObj[i]->mapNo == 1 || map->mapObj[i]->mapNo == 3 || map->mapObj[i]->mapNo == 6 || map->mapObj[i]->mapNo == 8
			|| map->mapObj[i]->mapNo == 9 || map->mapObj[i]->mapNo == 10 || map->mapObj[i]->mapNo == 11)
		{
			//----------------------------------------------------------------------------
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{
				//��
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

					//�ǂɓ���������X�N���[�����鏈�����~�߂�
					modeScrollChange = true;

					//���ɍs��
					//mapHitChange = false;
					if (dool->priority == true)
					{
						//�����Ă��Ԃœ�������		
						if (map->mapObj[i]->mapNo == 3 && dool->GetGearNo(3) == 3)
						{
							//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
							dool->AnimWalkChangeValues += dool->AnimWalkSpeed*gDeltaTime;

							if (dool->AnimWalkChangeValues > 1)
							{
								dool->AnimWalkCoount++;
								//�摜�ԍ��������Ȃ��悤�ɂ���
								if (dool->AnimWalkCoount == 8)
								{
									dool->AnimWalkCoount = 0;
								}
								dool->AnimWalkChangeValues = 0;
							}
							//�����A�j���[�V���������Ȃ��悤�ɂ���
							dool->SetCanWalkAnim(false);

							dool->mSprite->SetTexture(gpTextureDoolPushu[dool->AnimWalkCoount]);

							map->mapObj[i]->mSprite->mCenter.x = dool->mSprite->mCenter.x + (dool->mSprite->mSize.x / 2);
						}
					}
				}

				//�E
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

					//�E�ɍs��
					//�ǂɓ���������X�N���[�����鏈�����~�߂�
					modeScrollChange = true;
				}

				//��
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_DOWN)
				{
					//�ǂɓ���������X�N���[�����鏈�����~�߂�
					//�n�ʂɒ����܂ŃW�����v���ł��Ȃ��悤�ɂ���
					dool->mSprite->mCenter.y = dool->GetOldPosY();
					dool->SetCanJump(false);
				}

				//��
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_UP /*&& map->mapObj[i]->mapNo != 2*/)
				{	
					//�ǂɓ���������X�N���[�����鏈�����~�߂�
					modeScrollChange = true;
					//�n�ʂɒ�������d�͂�����������
					dool->mSprite->mCenter.y = dool->GetOldPosY();
					dool->SetAcc(0);
					dool->SetAccCheck(false);
					dool->SetCanJump(true);	
					dool->SetAnimJumpChangeValues(0);
					dool->SetAnimJumpCoount(0);
					dool->canJumpAnim = true;	//�W�����v�A�j���[�V�����̔���
					dool->jump_seCount = true;	//�W�����v��se�p
					dool->walk_seCount = true;
					flg = false;//���ɓ�����Ȃ��悤�ɂ���

					//dool->SetVel(0);

					//���̂̍�����荂���Ƃ��납�痎������R�P��摜�ɕς���
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
			
			//�ǂɂ߂荞�񂾂�
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

		//��q�������ēo��M�A����������
		if (/*map->mapObj[i]->mSprite->mCenter.x  > dool->mSprite->mCenter.x && */map->mapObj[i]->mapNo == 2 && dool->GetGearNo(1) == 1
			&& dool->mSprite->mCenter.x >= map->mapObj[i]->mSprite->mCenter.x - 0.01f	// �����̔���
			&& dool->mSprite->mCenter.x <= map->mapObj[i]->mSprite->mCenter.x + 0.01f)	// �E
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

				//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
				dool->AnimNoboruChangeValues += dool->AnimNoboruSpeed*gDeltaTime;

				if (dool->AnimNoboruChangeValues > 1)
				{
					dool->AnimNoboruCount++;
					XA_Play(SOUND_LABEL_SE005, 0.5f);
					//�摜�ԍ��������Ȃ��悤�ɂ���
					if (dool->AnimNoboruCount == 8)
					{
						dool->AnimNoboruCount = 0;
					}
					dool->AnimNoboruChangeValues = 0;
				}
				dool->mSprite->SetTexture(gpTextureDoolNoboru[dool->AnimNoboruCount]);

			}
		}

		//��q�������ēo��M�A����������
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

				//��
				if (dool->mHitSquare->IsSquareHitPlaceMapChip(map->mapObj[i]->mHitSquare) == dool->mHitSquare->HIT_UP /*&& map->mapObj[i]->mapNo != 2*/)
				{
					//�ǂɓ���������X�N���[�����鏈�����~�߂�
					modeScrollChange = true;
					//�n�ʂɒ�������d�͂�����������
					dool->mSprite->mCenter.y = dool->GetOldPosY();
					dool->SetAcc(0);
					dool->SetAccCheck(false);
					dool->SetCanJump(true);
					dool->SetAnimJumpChangeValues(0);
					dool->SetAnimJumpCoount(0);
				}

				//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
				dool->AnimNoboruChangeValues += dool->AnimNoboruSpeed*gDeltaTime;

				if (dool->AnimNoboruChangeValues > 1)
				{
					dool->AnimNoboruCount++;
					//�摜�ԍ��������Ȃ��悤�ɂ���
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
		//��q�������ēo��M�A����������
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

			//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
			dool->AnimNoboruChangeValues += dool->AnimNoboruSpeed*gDeltaTime;

			if (dool->AnimNoboruChangeValues > 1)
			{
				dool->AnimNoboruCount++;
				//�摜�ԍ��������Ȃ��悤�ɂ���
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

		//�L���b�`�������Ă���
		if (dool->GetGearNo(3) == 3)
		{
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{				
				if (map->mapObj[i]->mapNo == 5)
				{
					//��񓖂������ԍ��ɂ͓�����Ȃ�
					if (saveObj != i)
					{
						XA_Play(SOUND_LABEL_SE011, 0.8f);

						//���������ԍ�������
						saveObj = i;
						keyget = true;
						map->mapObj[i]->active = false;

						//�莆�̐��𐔂���
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
		
		//�S�[���ɓ���
		if (map->mapObj[i]->mapNo == 4 )
		{
			if (dool->mHitSquare->IsSquareHitMapChip(map->mapObj[i]->mHitSquare) == true)
			{
				if (keyget == true)
				{
					//�V�[���ύX
					sceneNo = 4;
				}
			}
		}
	}


}

void DoolManager::GoalHit(Dool * dool, Goal * goal,Key* key)
{
	//�S�[���ɓ���
	if (dool->mHitCircle->IsHit(goal->mHitCircle) == true)
	{
		if (key->GetKey())
		{
			//�V�[���ύX
			goal->StageGoal();
		}
	}
}

void DoolManager::KeyHit(Dool * dool, Key * key)
{
	if (dool->mHitCircle->IsHit(key->mHitCircle) == true)
	{
		//�L���b�`�������Ă���
		if (dool->GetGearNo(3) == 3)
		{
			key->SetKey(true);
			key->mSprite->mCenter.y = 1;
		}
	}
}

void DoolManager::DoolPosMapScroll(Dool * dool, MaptipFileRead * map)
{
	//�}�b�v�̉E�[���ǂ���
	bool mapend = false;

	//�}�b�v�̍��[���ǂ���
	bool mapstart = false;

	if (map->mapObj[0]->mSprite->mCenter.x > -1.28f)
	{
		mapstart = true;
	}

	if (map->mapObj[74]->mSprite->mCenter.x < 1.25f)
	{
		//�}�b�v�[�ɗ�����X�N���[���~�߂�
		modeScrollChange = true;

		mapend = true;
	}

	//�^�񒆖���������}�b�v�X�N���[���ɐ؂�ւ���
	if (dool->mSprite->mCenter.x > 0.0f && mapend == false && mapHitChange == true)
	{
		modeScrollChange = false;

		//�}�b�v�X�N���[������֐����Ă�
		if (modeScrollChange == false)
		{
			map->mapScroll(dool->GetWalk());
		}		
	}

	//�^�񒆂�荶�Ń}�b�v���[����Ȃ��ĕ����������������Ȃ�
	if (dool->mSprite->mCenter.x < 0.0f && mapstart == false && mapHitChange == false)
	{
		modeScrollChange = false;
		//�}�b�v�X�N���[������֐����Ă�
		if (modeScrollChange == false)
		{
			map->mapScroll(dool->GetWalk());
		}
	}
	
	//�^�񒆂�荶�Ȃ�h�[��������
	if (dool->mSprite->mCenter.x < 0.0f)
	{
	}

	if (dool->GetGearNo(4) != 4)
	{
	}

	//�[�ɒ�������L�������ړ�����
	if (mapend == true)
	{
	}
}
