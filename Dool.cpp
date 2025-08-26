#include "Dool.h"
#include"input.h"
#include "Direct3d.h"

//ドールが歩いてる画像8枚
extern ID3D11ShaderResourceView* gpTextureDoolWalk[MAX_GP];

//ジャンプ画像
extern ID3D11ShaderResourceView* gpTextureDoolJump[MAX_GP];

//ドールが歩いてる画像8枚
extern ID3D11ShaderResourceView* RgpTextureDoolWalk[MAX_GP];
//ジャンプの画像8枚
extern ID3D11ShaderResourceView* RgpTextureDoolJump[MAX_GP];

//会話画像
extern ID3D11ShaderResourceView* gpTextureDoolTalk[MAX_GP];

extern DWORD gDeltaTime;

//マップがスクロールするか歩くか
bool modeScrollChange = true;

//反転してるかどうか
bool reverseDool = false;

//デバッグモード
bool debugMode = false;

//当たるたびに向きを変える
extern bool mapHitChange;


void Dool::Update()
{	
	talkObj->Update();	

	//毎フレーム0にしておく
	walk = 0.0f;

	//歩きのアニメーションをするかどうかを決める
	if (GetGearNo(2) != 2)
		priority = true;

	//歩きのアニメーションをするかどうかを決める
	if (GetGearNo(3) != 3 && GetGearNo(4) != 4)
		canWalkAnim = true;

	//行動
	GeraIdCheck();

	// 加速度
	if (acc > -0.0001f && canAcc == true)
	{
		acc -= -0.0002f;
	}

	HighAcc += acc;

	//加速しすぎたら戻す
	if (acc > 0.01f)
	{
		acc = 0.01f;
	}

#ifdef _DEBUG
	// デバックビルドなら

	// 右移動
	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown('A'))
	{
		this->mSprite->mCenter.x -= 0.01f;
	}
	// 左移動
	if (Input_GetKeyDown(VK_SHIFT) && Input_GetKeyDown('D'))
	{
		this->mSprite->mCenter.x += 0.01f * gDeltaTime;
	}

	//反転させる
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

	// 右移動
	if (Input_GetKeyDown('A'))
	{
		this->mSprite->mCenter.x -= walk * gDeltaTime;
	}
	// 左移動
	if (Input_GetKeyDown('D'))
	{
		this->mSprite->mCenter.x += walk * gDeltaTime;
	}

	//デバッグモード
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

	// 下移動
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

	// 無理やりしています（別の処理でｙ座標の強制的な移動処理しているみたいですが、バグの元なので直してください）
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

	// ギア初期化
	if (Input_GetKeyDown('V'))
	{
		GearDelete();
	}

#endif // _DEBUG

	//前のフレームの情報を保存する
	this->SetOldPosX(this->mSprite->mCenter.x);
	this->SetOldPosY(this->mSprite->mCenter.y);

	if (modeScrollChange == true && UpHashigo == false)
	{		
		//移動に加速度かける
		walk *= dashAcc;

		dashAcc -= 0.001f;

		if (dashAcc < 1.0f)
		{
			dashAcc = 1.0f;
		}

		// 何の処理？
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

	//毎フレーム重力をオンにする
	canAcc = true;

	// 重力？
	vel -= acc;
	this->mSprite->mCenter.y = vel;

	//梯子に登ってない間は常にfalseにしておく
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
	// 何のギアが入っているのか？
	for (int i = 0; i < 3; i++)
	{	// 3スロット分、回す
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
	//逆回転か判定
	if (mReverse)
	{
		//スロットの中の値で行動を変える
		for (int i = 0; i < 3; i++)
		{
			switch (mGearSlot[i].nGear_no)
			{
			case 0:
				break;
			case WALK:
				//移動
				//this->mSprite->mCenter.x += 0.00001f;
				break;
			case JUMP:		
				//ジャンプ	
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
				//移動
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
							//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
							this->AnimWalkChangeValues += this->AnimWalkSpeed*gDeltaTime;

							if (this->AnimWalkChangeValues > ANIMATIONVALUES)
							{
								this->AnimWalkCoount++;
								
								//画像番号が超えないようにする
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
							//歩いている間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
							this->AnimWalkChangeValues += this->AnimWalkSpeed*gDeltaTime;

							if (this->AnimWalkChangeValues > ANIMATIONVALUES)
							{
								
								this->AnimWalkCoount++;

								//画像番号が超えないようにする
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
		//スロットの中の値で行動を変える
		for (int i = 0; i < 3; i++)
		{
			switch (mGearSlot[i].nGear_no)
			{				
			case 0:				
				break;
			case WALK:
				//移動
				this->mSprite->mCenter.y -= 0.00001f;
				break;
			case JUMP:
				//ジャンプ
				Jump();
				break;
			case CHACH:
				//移動
				this->mSprite->mCenter.y += 0.00001f;
				break;
			case SHAGMU:
				//移動
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
	//スロットの中の値で行動を変える
	for (int i = 0; i < 3; i++)
	{
		switch (mGearSlot[i].nGear_tag)
		{
		case NORMAL:
			//通常
			Act();
			break;
		case SABI:
			//錆
			Act();
			break;
		case HISPEC:
			//高性能
			Act();
			break;
		default:
			break;
		}
	}
}

void Dool::GearDist(GEAR_ID gearNo)
{
	//前の値を保存
	int save = mGcount;	

	//スロットにギアのナンバーを入れる
	//配列の中に同じ数字がなければ入れる
	if (gearNo.nGear_no != mGearSlot[0].nGear_no && gearNo.nGear_no != mGearSlot[1].nGear_no && gearNo.nGear_no != mGearSlot[2].nGear_no)
	{
		//スロットの値を一つ進める
	//値が3になったら0に戻してダッシュ関数を呼ぶ
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

	// ジャンプできるか？
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

			//地面から離れるのに最初だけおおきい値を入れる必要がある
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
				//ジャンプ間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
				this->AnimJumpChangeValues += this->AnimJumpSpeed * gDeltaTime;

				if (this->AnimJumpChangeValues > ANIMATIONVALUES - 30)
				{					
					if (AnimJumpCount == 0)
					{
						XA_Play(SOUND_LABEL_SE000,0.15f);
					}	

					this->AnimJumpCount++;
					//画像番号が超えないようにする
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
				//ジャンプ間は毎フレームアニメーション速度を足していって既定の値に行くと次の番号に行く
				this->AnimJumpChangeValues += this->AnimJumpSpeed * gDeltaTime;

				if (this->AnimJumpChangeValues > ANIMATIONVALUES - 30)
				{
					if (AnimJumpCount == 0)
					{
						XA_Play(SOUND_LABEL_SE000, 0.15f);
					}
					this->AnimJumpCount++;
					//画像番号が超えないようにする
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
	//スロットの中の値で行動を変える
	for (int i = 0; i < 3; i++)
	{
		// JumpHighAccCheckの値を変えるとダウンするマス目が変わる
		if (mGearSlot[i].nGear_no == 2 && HighAcc >= JumpHighAccCheck)
		{

			return true;
		}
		else
		{
			// ジャンプギアが入っていないとき、4段でダウン
			if (mGearSlot[0].nGear_no != 2 && mGearSlot[1].nGear_no != 2 && mGearSlot[2].nGear_no != 2)
			{
				// HighAccCheckの値を変えるとダウンするマス目が変わる
				if (HighAcc >= HighAccCheck)
					return true;
			}
		}
	}

	return false;
}
