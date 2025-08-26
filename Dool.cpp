#include "Dool.h"
#include"input.h"
#include "Direct3d.h"

//�h�[���������Ă�摜8��
extern ID3D11ShaderResourceView* gpTextureDoolWalk[MAX_GP];

//�W�����v�摜
extern ID3D11ShaderResourceView* gpTextureDoolJump[MAX_GP];

//�h�[���������Ă�摜8��
extern ID3D11ShaderResourceView* RgpTextureDoolWalk[MAX_GP];
//�W�����v�̉摜8��
extern ID3D11ShaderResourceView* RgpTextureDoolJump[MAX_GP];

//��b�摜
extern ID3D11ShaderResourceView* gpTextureDoolTalk[MAX_GP];

extern DWORD gDeltaTime;

//�}�b�v���X�N���[�����邩������
bool modeScrollChange = true;

//���]���Ă邩�ǂ���
bool reverseDool = false;

//�f�o�b�O���[�h
bool debugMode = false;

//�����邽�тɌ�����ς���
extern bool mapHitChange;


void Dool::Update()
{	
	talkObj->Update();	

	//���t���[��0�ɂ��Ă���
	walk = 0.0f;

	//�����̃A�j���[�V���������邩�ǂ��������߂�
	if (GetGearNo(2) != 2)
		priority = true;

	//�����̃A�j���[�V���������邩�ǂ��������߂�
	if (GetGearNo(3) != 3 && GetGearNo(4) != 4)
		canWalkAnim = true;

	//�s��
	GeraIdCheck();

	// �����x
	if (acc > -0.0001f && canAcc == true)
	{
		acc -= -0.0002f;
	}

	HighAcc += acc;

	//��������������߂�
	if (acc > 0.01f)
	{
		acc = 0.01f;
	}

#ifdef _DEBUG
	// �f�o�b�N�r���h�Ȃ�

	// �E�ړ�
	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown('A'))
	{
		this->mSprite->mCenter.x -= 0.01f;
	}
	// ���ړ�
	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown('D'))
	{
		this->mSprite->mCenter.x += 0.01f * gDeltaTime;
	}

	//���]������
	if (Input_GetKeyTrigger('R'))
	{
		if (mapHitChange)
		{
			mapHitChange = false;
		}
		else
		{
			mapHitChange = true;
		}
	}

	// �E�ړ�
	if (Input_GetKeyDown('A'))
	{
		this->mSprite->mCenter.x -= walk * gDeltaTime;
	}
	// ���ړ�
	if (Input_GetKeyDown('D'))
	{
		this->mSprite->mCenter.x += walk * gDeltaTime;
	}

	//�f�o�b�O���[�h
	if (Input_GetKeyTrigger('B'))
	{
		if (debugMode)
		{
			debugMode = false;
		}
		else
		{
			debugMode = true;
		}
	}

	// ���ړ�
	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown('S'))
	{
		vel -= 0.005f;
		acc = 0;
	}

	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown('W'))
	{
		vel += 0.005f;
		acc = 0;
	}

	// ������肵�Ă��܂��i�ʂ̏����ł����W�̋����I�Ȉړ��������Ă���݂����ł����A�o�O�̌��Ȃ̂Œ����Ă��������j
	if (Input_GetKeyDown(VK_SPACE))
	{
		vel += 0.0005f;
		Jump();
	}

	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown(VK_SPACE))
	{
		vel += 0.0005f;
		acc = 0;
		Jump();
	}

	// �M�A������
	if (Input_GetKeyDown('V'))
	{
		GearDelete();
	}

#endif // _DEBUG

	//�O�̃t���[���̏���ۑ�����
	this->SetOldPosX(this->mSprite->mCenter.x);
	this->SetOldPosY(this->mSprite->mCenter.y);

	if (modeScrollChange == true && UpHashigo == false)
	{		
		//�ړ��ɉ����x������
		walk *= dashAcc;

		dashAcc -= 0.001f;

		if (dashAcc < 1.0f)
		{
			dashAcc = 1.0f;
		}

		// ���̏����H
		if (this->mSprite->mCenter.x > 0.6f)
		{
			this->mSprite->mCenter.x = 0.6f;
		}

		if (this->mSprite->mCenter.x < -1.3f)
		{
			this->mSprite->mCenter.x = -1.3f;
		}

		this->mSprite->mCenter.x += walk;
	}

	//���t���[���d�͂��I���ɂ���
	canAcc = true;

	// �d�́H
	vel -= acc;
	this->mSprite->mCenter.y = vel;

	//��q�ɓo���ĂȂ��Ԃ͏��false�ɂ��Ă���
	this->UpHashigo = false;

	if (this->GearCrash())
	{
		talk = 7;
	}

	talkObj->mSprite->SetTexture(gpTextureDoolTalk[talk]);	

	GameObject::Update();
}

void Dool::Draw()
{
	talkObj->Draw();

	GameObject::Draw();
}

Dool::Dool()
{
	// ���̃M�A�������Ă���̂��H
	for (int i = 0; i < 3; i++)
	{	// 3�X���b�g���A��
		this->mGearSlot[i] = { 0,0 };
	}

	talkObj = new StaticObject();
	talkObj->mSprite->mSize.x = 1.0f;
	talkObj->mSprite->mSize.y
		= 0.2f;
	talkObj->mSprite->mCenter.y = -0.6f;
	talkObj->mSprite->mCenter.x = -0.2f;
	talkObj->mSprite->SetTexture(gpTextureDoolTalk[0]);

	talk = 0;
}

Dool::~Dool()
{
	delete talkObj;

	for (int i = 0; i < MAX_GEAR; i++)
	{
		COM_SAFE_RELEASE(gpTextureDoolWalk[i]);
		COM_SAFE_RELEASE(gpTextureDoolJump[i]);
		COM_SAFE_RELEASE(RgpTextureDoolWalk[i]);
		COM_SAFE_RELEASE(RgpTextureDoolJump[i]);
		COM_SAFE_RELEASE(gpTextureDoolTalk[i]);
	}
}

void Dool::Act()
{
	//�t��]������
	if (mReverse)
	{
		//�X���b�g�̒��̒l�ōs����ς���
		for (int i = 0; i < 3; i++)
		{
			switch (mGearSlot[i].nGear_no)
			{
			case 0:
				break;
			case WALK:
				//�ړ�
				//this->mSprite->mCenter.x += 0.00001f;
				break;
			case JUMP:		
				//�W�����v	
				if (this->jump_seCount == true)
				{
					XA_Play(SOUND_LABEL_SE000,0.5f);
					this->jump_seCount = false;
				}
				Jump();
				priority = false;
				break;
			case CHACH:

				break;
			case SHAGMU:
				//�ړ�
				if (mapHitChange == true)
				{
					walk = 0.001f;
				}
				else
				{
					walk = -0.001f;
				}				
				if (priority == true)
				{
					if (canWalkAnim == true)
					{
						
						if (mapHitChange == true)
						{							
							//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
							this->AnimWalkChangeValues += this->AnimWalkSpeed*gDeltaTime;

							if (this->AnimWalkChangeValues > ANIMATIONVALUES)
							{
								this->AnimWalkCoount++;
								
								//�摜�ԍ��������Ȃ��悤�ɂ���
								if (this->AnimWalkCoount == 3)
								{
									if (this->walk_seCount == true)
									{
										XA_Play(SOUND_LABEL_SE003, 0.5f);
									}
								}
								if (this->AnimWalkCoount == 6)
								{
									if (this->walk_seCount == true)
									{
										XA_Play(SOUND_LABEL_SE003, 0.5f);
									}
								}
								if (this->AnimWalkCoount == 8)
								{
									this->AnimWalkCoount = 0;

								}
								this->AnimWalkChangeValues = 0;
							}
							this->mSprite->SetTexture(gpTextureDoolWalk[this->AnimWalkCoount]);
						}
						else
						{
							//�����Ă���Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
							this->AnimWalkChangeValues += this->AnimWalkSpeed*gDeltaTime;

							if (this->AnimWalkChangeValues > ANIMATIONVALUES)
							{
								
								this->AnimWalkCoount++;

								//�摜�ԍ��������Ȃ��悤�ɂ���
								if (this->AnimWalkCoount == 3)
								{
									XA_Play(SOUND_LABEL_SE003, 0.5f);

								}
								if (this->AnimWalkCoount == 6)
								{
									XA_Play(SOUND_LABEL_SE003, 0.5f);

								}
								if (this->AnimWalkCoount == 8)
								{
									this->AnimWalkCoount = 0;

								}
								this->AnimWalkChangeValues = 0;
							}
							this->mSprite->SetTexture(RgpTextureDoolWalk[this->AnimWalkCoount]);
						}						
					}
				}
				break;
			default:
				break;
			}
		}
	}	
	else
	{
		//�X���b�g�̒��̒l�ōs����ς���
		for (int i = 0; i < 3; i++)
		{
			switch (mGearSlot[i].nGear_no)
			{				
			case 0:				
				break;
			case WALK:
				//�ړ�
				this->mSprite->mCenter.y -= 0.00001f;
				break;
			case JUMP:
				//�W�����v
				Jump();
				break;
			case CHACH:
				//�ړ�
				this->mSprite->mCenter.y += 0.00001f;
				break;
			case SHAGMU:
				//�ړ�
				this->mSprite->mCenter.x -= 0.0001f;
				break;
			default:
				break;
			}
		}
	}
}

void Dool::GeraIdCheck()
{
	//�X���b�g�̒��̒l�ōs����ς���
	for (int i = 0; i < 3; i++)
	{
		switch (mGearSlot[i].nGear_tag)
		{
		case NORMAL:
			//�ʏ�
			Act();
			break;
		case SABI:
			//�K
			Act();
			break;
		case HISPEC:
			//�����\
			Act();
			break;
		default:
			break;
		}
	}
}

void Dool::GearDist(GEAR_ID gearNo)
{
	//�O�̒l��ۑ�
	int save = mGcount;	

	//�X���b�g�ɃM�A�̃i���o�[������
	//�z��̒��ɓ����������Ȃ���Γ����
	if (gearNo.nGear_no != mGearSlot[0].nGear_no && gearNo.nGear_no != mGearSlot[1].nGear_no && gearNo.nGear_no != mGearSlot[2].nGear_no)
	{
		//�X���b�g�̒l����i�߂�
	//�l��3�ɂȂ�����0�ɖ߂��ă_�b�V���֐����Ă�
		if (mGcount == 3)
		{
			mGcount = 0;
			save = mGcount;
			mGcount++;
			this->Dashu();
		}
		else
		{
			save = mGcount;
			mGcount++;
		}

		mGearSlot[save] = gearNo;		
	}			
	else
	{		
	}
}

void Dool::Dashu()
{	
	if (mReverse)
	{
		dashAcc += 0.5f;
	}
	else
	{
		this->mSprite->mCenter.x -= 1.0f;
	}
}

void Dool::Jump()
{	
	XA_Stop(SOUND_LABEL_SE003);

	// �W�����v�ł��邩�H
	if (canJump == true)
	{
		if (canJumpAnim == true)
		{			

			if (vel > 0.6f)
			{
				canJump = false;
				onceVelCheck = false;

				this->AnimJumpChangeValues = 0;
				this->AnimJumpCount = 0;
			}

			//�n�ʂ��痣���̂ɍŏ��������������l������K�v������
			if (onceVelCheck)
			{
				vel += 0.0025f;
				canAcc = true;
				
				
			}	
			else
			{			
				vel += onceVelAdd;
				onceVelCheck = true;
			}	

			if (mapHitChange)
			{
				//�W�����v�Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
				this->AnimJumpChangeValues += this->AnimJumpSpeed * gDeltaTime;

				if (this->AnimJumpChangeValues > ANIMATIONVALUES - 30)
				{					
					if (AnimJumpCount == 0)
					{
						XA_Play(SOUND_LABEL_SE000,0.15f);
					}	

					this->AnimJumpCount++;
					//�摜�ԍ��������Ȃ��悤�ɂ���
					if (this->AnimJumpCount > 7)
					{
						this->AnimJumpCount = 7;
					}

					this->AnimJumpChangeValues = 0;
				}
				this->mSprite->SetTexture(gpTextureDoolJump[this->AnimJumpCount]);
			}
			else
			{
				//�W�����v�Ԃ͖��t���[���A�j���[�V�������x�𑫂��Ă����Ċ���̒l�ɍs���Ǝ��̔ԍ��ɍs��
				this->AnimJumpChangeValues += this->AnimJumpSpeed * gDeltaTime;

				if (this->AnimJumpChangeValues > ANIMATIONVALUES - 30)
				{
					if (AnimJumpCount == 0)
					{
						XA_Play(SOUND_LABEL_SE000, 0.15f);
					}
					this->AnimJumpCount++;
					//�摜�ԍ��������Ȃ��悤�ɂ���
					if (this->AnimJumpCount > 7)
					{
						this->AnimJumpCount = 7;
					}

					this->AnimJumpChangeValues = 0;
				}
				this->mSprite->SetTexture(RgpTextureDoolJump[this->AnimJumpCount]);
			}
		}
	}

	
}

void Dool::SetmReverse(bool _reverse)
{
	mReverse = _reverse;
}

void Dool::SetOldPosX(float _x)
{
	this->old_posX = _x;
}

void Dool::SetOldPosY(float _y)
{
	this->old_posY = _y;
}

void Dool::SetCanJump(bool _jump)
{
	canJump = _jump;
}

void Dool::SetAcc(float _acc)
{
	acc = _acc;
}

void Dool::SetVel(float _vel)
{
	vel = _vel;
}

void Dool::SetWalk(float _walk)
{
	walk = _walk;
}

void Dool::SetAnimJumpCoount(int _count)
{
	AnimJumpCount = _count;
}

void Dool::SetAnimJumpChangeValues(float _values)
{
	AnimJumpChangeValues = _values;
}

void Dool::SetAccCheck(bool _acc)
{
	canAcc = _acc;
}

void Dool::SetCanWalkAnim(bool _WalkAnim)
{
	canWalkAnim = _WalkAnim;
}

float Dool::GetOldPosX()
{
	return old_posX;
}

float Dool::GetOldPosY()
{
	return old_posY;
}

bool Dool::GetCanJump()
{
	return canJump;
}

float Dool::GetSpeed()
{
	return speed;
}

float Dool::GetWalk()
{
	return walk;
}

void Dool::GearDelete()
{
	mGcount = 0;
	for (int i = 0; i < 3; i++)
		mGearSlot[i] = {0,0};
}

int Dool::GetGearNo(int _no)
{
	for (int i = 0; i < 3; i++)
	{
		if (mGearSlot[i].nGear_no == _no)
		{
			return _no;
		}
	}
	return 0;
}

bool Dool::GearCrash()
{
	//�X���b�g�̒��̒l�ōs����ς���
	for (int i = 0; i < 3; i++)
	{
		// JumpHighAccCheck�̒l��ς���ƃ_�E������}�X�ڂ��ς��
		if (mGearSlot[i].nGear_no == 2 && HighAcc >= JumpHighAccCheck)
		{

			return true;
		}
		else
		{
			// �W�����v�M�A�������Ă��Ȃ��Ƃ��A4�i�Ń_�E��
			if (mGearSlot[0].nGear_no != 2 && mGearSlot[1].nGear_no != 2 && mGearSlot[2].nGear_no != 2)
			{
				// HighAccCheck�̒l��ς���ƃ_�E������}�X�ڂ��ς��
				if (HighAcc >= HighAccCheck)
					return true;
			}
		}
	}

	return false;
}
