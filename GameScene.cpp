#include "GameScene.h"

extern bool clickCheck;
extern bool clickCheckAfter;

//�����邽�тɌ�����ς���
bool mapHitChange = true;

//�莆�̐��𐔂���
int latterCount = 0;

//�h�[���������Ă�摜8��
ID3D11ShaderResourceView* gpTextureDoolWalk[MAX_GP];
//�W�����v�̉摜8��
ID3D11ShaderResourceView* gpTextureDoolJump[MAX_GP];
//�h�[���������Ă�摜8��
ID3D11ShaderResourceView* RgpTextureDoolWalk[MAX_GP];
//�W�����v�̉摜8��
ID3D11ShaderResourceView* RgpTextureDoolJump[MAX_GP];
//�����Ă�摜
ID3D11ShaderResourceView* gpTextureDoolPushu[MAX_GP];
//��q�o��摜
ID3D11ShaderResourceView* gpTextureDoolNoboru[MAX_GP];

//�����摜
ID3D11ShaderResourceView* gpTextureGearUI01;

//�͂����摜
ID3D11ShaderResourceView* gpTextureGearUI02;

//�W�����v�摜
ID3D11ShaderResourceView* gpTextureGearUI03;

//���Z�b�g
ID3D11ShaderResourceView* gpTextureGearUI04;

//�莆�摜
ID3D11ShaderResourceView* gpTextureGearUI05;

//��b�摜
ID3D11ShaderResourceView* gpTextureDoolTalk[MAX_GP];

ID3D11ShaderResourceView* gpTextureMapTipWall;		//�}�b�v�`�b�v�p�摜��
ID3D11ShaderResourceView* gpTextureMapTipHasigo;	//�}�b�v�`�b�v�p�摜��q

ID3D11ShaderResourceView* gpTextureMapTipHouse03;	//�}�b�v�`�b�v�p�摜��q

ID3D11ShaderResourceView* gpTextureFe01;			//�t�F�[�h�A�E�g�摜01�̃e�N�X�`��

ID3D11ShaderResourceView* gpTextureBox;				//���̉摜�̃e�N�X�`��
ID3D11ShaderResourceView* gpTextureBox2;				//���̉摜�̃e�N�X�`��
ID3D11ShaderResourceView* gpTextureBox3;				//���̉摜�̃e�N�X�`��

ID3D11ShaderResourceView* gpTextureKey;				//���̉摜�̃e�N�X�`��

ID3D11ShaderResourceView* gpTextureGoal;				//�S�[���摜�̃e�N�X�`��

ID3D11ShaderResourceView* gpTextureDoolFallRight;	//�E�����ɂ�����h�[���摜01�̃e�N�X�`��

ID3D11ShaderResourceView* gpTextureGear01;			//�M�A�摜01�̃e�N�X�`��

ID3D11ShaderResourceView* gpTextureGoldGear01;		//�M�A�摜01�̃e�N�X�`��(���F
ID3D11ShaderResourceView* gpTextureGoldGear02;		//�M�A�摜02�̃e�N�X�`��(���F)
ID3D11ShaderResourceView* gpTextureGoldGear03;		//�M�A�摜03�̃e�N�X�`��(���F)
ID3D11ShaderResourceView* gpTextureGoldGear04;		//�M�A�摜02�̃e�N�X�`��(���F)
ID3D11ShaderResourceView* gpTextureGoldGear05;		//�M�A�摜03�̃e�N�X�`��(���F)
extern DWORD gDeltaTime;

GameScene::GameScene()
{
	//�h�[���摜�ǂݍ���
	LoadTexture(L"assets/dool_idle.png", &gpTextureDool01);

	//�h�[���̕����摜�ǂݍ���
	LoadTexture(L"assets/walk_1.png", &gpTextureDoolWalk[0]);
	LoadTexture(L"assets/walk_2.png", &gpTextureDoolWalk[1]);
	LoadTexture(L"assets/walk_3.png", &gpTextureDoolWalk[2]);
	LoadTexture(L"assets/walk_4.png", &gpTextureDoolWalk[3]);
	LoadTexture(L"assets/walk_5.png", &gpTextureDoolWalk[4]);
	LoadTexture(L"assets/walk_06.png", &gpTextureDoolWalk[5]);
	LoadTexture(L"assets/walk_7.png", &gpTextureDoolWalk[6]);
	LoadTexture(L"assets/walk_8.png", &gpTextureDoolWalk[7]);

	//�h�[���̃W�����v�摜�ǂݍ���
	LoadTexture(L"assets/j1.png", &gpTextureDoolJump[0]);
	LoadTexture(L"assets/j2.png", &gpTextureDoolJump[1]);
	LoadTexture(L"assets/j3.png", &gpTextureDoolJump[2]);
	LoadTexture(L"assets/j4.png", &gpTextureDoolJump[3]);
	LoadTexture(L"assets/j5.png", &gpTextureDoolJump[4]);
	LoadTexture(L"assets/j6.png", &gpTextureDoolJump[5]);
	LoadTexture(L"assets/j7.png", &gpTextureDoolJump[6]);
	LoadTexture(L"assets/j8.png", &gpTextureDoolJump[7]);

	//�h�[���̕����摜�ǂݍ���
	LoadTexture(L"assets/Rwalk_1.png", &RgpTextureDoolWalk[0]);
	LoadTexture(L"assets/Rwalk_2.png", &RgpTextureDoolWalk[1]);
	LoadTexture(L"assets/Rwalk_3.png", &RgpTextureDoolWalk[2]);
	LoadTexture(L"assets/Rwalk_4.png", &RgpTextureDoolWalk[3]);
	LoadTexture(L"assets/Rwalk_5.png", &RgpTextureDoolWalk[4]);
	LoadTexture(L"assets/Rwalk_06.png", &RgpTextureDoolWalk[5]);
	LoadTexture(L"assets/Rwalk_7.png", &RgpTextureDoolWalk[6]);
	LoadTexture(L"assets/Rwalk_8.png", &RgpTextureDoolWalk[7]);

	//�h�[���̃W�����v�摜�ǂݍ���
	LoadTexture(L"assets/Rj1.png", &RgpTextureDoolJump[0]);
	LoadTexture(L"assets/Rj2.png", &RgpTextureDoolJump[1]);
	LoadTexture(L"assets/Rj3.png", &RgpTextureDoolJump[2]);
	LoadTexture(L"assets/Rj4.png", &RgpTextureDoolJump[3]);
	LoadTexture(L"assets/Rj5.png", &RgpTextureDoolJump[4]);
	LoadTexture(L"assets/Rj6.png", &RgpTextureDoolJump[5]);
	LoadTexture(L"assets/Rj7.png", &RgpTextureDoolJump[6]);
	LoadTexture(L"assets/Rj8.png", &RgpTextureDoolJump[7]);

	//�h�[���̉����Ă�摜�ǂݍ���
	LoadTexture(L"assets/osu_1.png", &gpTextureDoolPushu[0]);
	LoadTexture(L"assets/osu_2.png", &gpTextureDoolPushu[1]);
	LoadTexture(L"assets/osu_3.png", &gpTextureDoolPushu[2]);
	LoadTexture(L"assets/osu_4.png", &gpTextureDoolPushu[3]);
	LoadTexture(L"assets/osu_5.png", &gpTextureDoolPushu[4]);
	LoadTexture(L"assets/osu_6.png", &gpTextureDoolPushu[5]);
	LoadTexture(L"assets/osu_7.png", &gpTextureDoolPushu[6]);
	LoadTexture(L"assets/osu_8.png", &gpTextureDoolPushu[7]);

	//�h�[���̒�q�o��摜�ǂݍ���
	LoadTexture(L"assets/ladder_1.png", &gpTextureDoolNoboru[0]);
	LoadTexture(L"assets/ladder_2.png", &gpTextureDoolNoboru[1]);
	LoadTexture(L"assets/ladder_3.png", &gpTextureDoolNoboru[2]);
	LoadTexture(L"assets/ladder_4.png", &gpTextureDoolNoboru[3]);
	LoadTexture(L"assets/ladder_5.png", &gpTextureDoolNoboru[4]);
	LoadTexture(L"assets/ladder_6.png", &gpTextureDoolNoboru[5]);
	LoadTexture(L"assets/ladder_7.png", &gpTextureDoolNoboru[6]);
	LoadTexture(L"assets/ladder_8.png", &gpTextureDoolNoboru[7]);

	//�h�[���̒�q�o��摜�ǂݍ���
	LoadTexture(L"assets/Sentence1.png", &gpTextureDoolTalk[0]);
	LoadTexture(L"assets/Sentence2.png", &gpTextureDoolTalk[1]);
	LoadTexture(L"assets/Sentence3.png", &gpTextureDoolTalk[2]);
	LoadTexture(L"assets/Sentence4.png", &gpTextureDoolTalk[3]);
	LoadTexture(L"assets/Sentence5.png", &gpTextureDoolTalk[4]);
	LoadTexture(L"assets/Sentence6.png", &gpTextureDoolTalk[5]);
	LoadTexture(L"assets/Sentence7.png", &gpTextureDoolTalk[6]);
	LoadTexture(L"assets/Sentence8.png", &gpTextureDoolTalk[7]);

	//gearUI
	LoadTexture(L"assets/UI_Name_aruku.png", &gpTextureGearUI01);
	LoadTexture(L"assets/UI_Name_hasigo.png", &gpTextureGearUI02);
	LoadTexture(L"assets/UI_Name_janpu.png", &gpTextureGearUI03);
	LoadTexture(L"assets/UI_Name_risetto.png", &gpTextureGearUI04);
	LoadTexture(L"assets/UI_Name_tegami.png", &gpTextureGearUI05);

	//��
	LoadTexture(L"assets/house3.png", &gpTextureMapTipHouse03);

	//�Ǘp�摜�ǂݍ���
	LoadTexture(L"assets/murasaki.png", &gpTextureMapTipWall);

	//��q�摜�ǂݍ���
	LoadTexture(L"assets/hasigo.png", &gpTextureMapTipHasigo);

	//�E�����ɂ�����h�[���摜�ǂݍ���
	LoadTexture(L"assets/dool_kokeru_R.png", &gpTextureDoolFallRight);

	//�h�������ɂ�����[���摜�ǂݍ���
	LoadTexture(L"assets/dool_kokeru_L.png", &gpTextureDoolFallLeft);

	//�Ή摜�ǂݍ���
	LoadTexture(L"assets/stone.png", &gpTextureStone);

	//�w�i�摜�ǂݍ���
	LoadTexture(L"assets/BackGround.jpg", &gpTextureBackGround);

	//�M�A�摜�ǂݍ���(���F)		
	LoadTexture(L"assets/walk_gear.png", &gpTextureGoldGear01);

	LoadTexture(L"assets/jump_gear.png", &gpTextureGoldGear02);

	LoadTexture(L"assets/ladder_gear.png", &gpTextureGoldGear03);	
	LoadTexture(L"assets/memo_gear.png", &gpTextureGoldGear04);

	LoadTexture(L"assets/osu_gear.png", &gpTextureGoldGear05);
	//�M�A�摜�ǂݍ���
	LoadTexture(L"assets/gear_walk.png", &gpTextureGear01);

	LoadTexture(L"assets/gear_jump.png", &gpTextureGear02);

	LoadTexture(L"assets/gear_up.png", &gpTextureGear03);

	//�M�A�摜�ǂݍ���
	LoadTexture(L"assets/UI1.png", &gpTextureUI01);
	LoadTexture(L"assets/UI2.png", &gpTextureUI02);
	LoadTexture(L"assets/UI3.png", &gpTextureUI03);
	LoadTexture(L"assets/UI4.png", &gpTextureUI04);

	//�t�F�[�h
	LoadTexture(L"assets/white.png", &gpTextureFe01);	

	//goal
	LoadTexture(L"assets/mailbox.png", &gpTextureGoal);

	//��
	LoadTexture(L"assets/Box.png", &gpTextureBox);

	LoadTexture(L"assets/IMG_0624.png", &gpTextureBox2);

	LoadTexture(L"assets/IMG_0616.png", &gpTextureBox3);

	//��	
	LoadTexture(L"assets/Key.png", &gpTextureKey);


	//�h�[�����̉�
	dool = new Dool();
	dool->mSprite->SetTexture(gpTextureDool01);
	dool->mSprite->mCenter.x = -0.8f;
	/*dool->mSprite->mCenter.x = 0.5f;*/
	dool->mSprite->mCenter.y = 1.0f;
	dool->SetSize(0.2f, 0.2f);

	//�M�A�����N���X����
	gj = new GearJanereter({ 4,0 });
	gj->mSprite->SetTexture(gpTextureGoldGear01);
	gj->mSprite->mCenter.x = 1.0f;
	gj->mSprite->mCenter.y = -0.415f;
	gj->SetSize(0.2f, 0.2f);
	// �W�����v�M�A
	jumpGear = new GearJanereter({ 2,0 });
	jumpGear->mSprite->SetTexture(gpTextureGoldGear02);
	jumpGear->mSprite->mCenter.x = 0.85f;
	jumpGear->mSprite->mCenter.y = -0.58f;
	jumpGear->SetSize(0.2f, 0.2f);
	// ���ރM�A
	catchiGear = new GearJanereter({ 3,0 });
	catchiGear->mSprite->SetTexture(gpTextureGoldGear05);
	catchiGear->mSprite->mCenter.x = 0.85f;
	catchiGear->mSprite->mCenter.y = -0.25f;
	catchiGear->SetSize(0.2f, 0.2f);
	// �o��M�A
	upGear = new GearJanereter({ 1,0 });
	upGear->mSprite->SetTexture(gpTextureGoldGear03);
	upGear->mSprite->mCenter.x = 1.15f;
	upGear->mSprite->mCenter.y = -0.25f;
	upGear->SetSize(0.2f, 0.2f);

	//��̃M�A
	emptiGear = new GearJanereter({ 1,0 });
	emptiGear->mSprite->SetTexture(gpTextureGoldGear04);
	emptiGear->mSprite->mCenter.x = 1.15f;
	emptiGear->mSprite->mCenter.y = -0.58f;
	emptiGear->SetSize(0.2f, 0.2f);

	//�X�e�[�W�M�A�E�ɓ�����
	StageGearX = new RotateObject();
	StageGearX->mSprite->SetTexture(gpTextureGear01);
	StageGearX->SetPosition(1.15f, 0.1f);
	StageGearX->SetSize(0.2f, 0.2f);

	//�X�e�[�W�M�A��ɓ�����
	StageGearY = new RotateObject();
	StageGearY->mSprite->SetTexture(gpTextureGear01);
	StageGearY->SetPosition(1.0f, 0.1f);
	StageGearY->SetSize(0.2f, 0.2f);

	//���������M�A�̉摜��ݒ肷��
	for (int i = 0; i < 5; i++)
	{		
		gj->mGear[i]->mSprite->SetTexture(gpTextureGoldGear01);
		jumpGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear02);
		catchiGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear05);
		upGear->mGear[i]->mSprite->SetTexture(gpTextureGoldGear03);
	}

	//�h�[���̊Ǘ��N���X����
	doolMg = new DoolManager();
	doolMg->mSprite->SetTexture(gpTextureGear01);
	doolMg->mSprite->mCenter.x = 0.95f;
	doolMg->mSprite->mCenter.y = 0.5f;
	doolMg->SetSize(0.6f, 0.4f);

	////�S�[���N���X����
	//goal = new Goal();
	//goal->mSprite->SetTexture(gpTextureGoal);
	//goal->mSprite->mCenter.x = 0.3f;
	//goal->mSprite->mCenter.y = -0.2f;

	//�΃N���X
	stone = new AStone();
	stone->mSprite->SetTexture(gpTextureStone);
	stone->mSprite->mCenter.x = -0.5f;
	stone->mSprite->mCenter.y = 0.0f;
	stone->active = false;

	//�w�i
	for (int i = 0; i < 2; i++)
	{
		background[i] = new StaticObject();
		background[i]->mSprite->SetTexture(gpTextureBackGround);
		background[i]->mSprite->mSize.x = 2.675f;
		background[i]->mSprite->mSize.y = 1.36f;
		background[i]->mSprite->mCenter.y = 0.074f;
		background[i]->mSprite->mCenter.x = i * 2;
	}	

	//UI
	UI01 = new HitObject();
	UI01->mSprite->SetTexture(gpTextureUI01);
	UI01->mSprite->mCenter.x = -0.34f;
	UI01->mSprite->mCenter.y = -0.575f;
	UI01->mSprite->mSize.x = 1.99f;
	UI01->mSprite->mSize.y = 0.35f;

	UI02 = new StaticObject();
	UI02->mSprite->SetTexture(gpTextureUI02);
	UI02->mSprite->mCenter.x = 1.0f;
	UI02->mSprite->mCenter.y = -0.4f;
	UI02->SetSize(0.7f,0.7f);

	UI03 = new StaticObject();
	UI03->mSprite->SetTexture(gpTextureUI03);
	UI03->mSprite->mCenter.x = 1.0f;
	UI03->mSprite->mCenter.y = 0.1f;
	UI03->mSprite->mSize.x = 0.7f;
	UI03->mSprite->mSize.y = 0.4f;


	UI04 = new StaticObject();
	UI04->mSprite->SetTexture(gpTextureUI04);
	UI04->mSprite->mCenter.x = 1.0f;
	UI04->mSprite->mCenter.y = 0.49f;
	UI04->mSprite->mSize.x = 0.7f;
	UI04->mSprite->mSize.y = 0.54f;

	//�}�b�v�`�b�v�t�@�C���ǂݍ���
	maptip = new MaptipFileRead("1-1.csv");
	maptip->mapObjSet();

	//�}�E�X�摜�ǂݍ���
	LoadTexture(L"assets/MouseN.png", &gpTextureMouse01);
	LoadTexture(L"assets/MouseY.png", &gpTextureMouse02);

	//�}�E�X���̉�
	gpMouse = new Operation();
	gpMouse->mSprite->SetTexture(gpTextureMouse01);
	gpMouse->SetSize(0.35f, 0.35f);

	//�����̉�
	//key = new Key();
	//key->mSprite->SetTexture(gpTextureKey);
	//key->mSprite->mCenter.x = 0.0f;
	//key->mSprite->mCenter.y = -0.2f;
	//key->SetSize(0.25f, 0.25f);

	// �t�F�[�h
	gpFade = new Fade();

	//�n���R�����摜
	stanp = new StaticObject();
	stanp->mSprite->SetTexture(gpTextureGear01);
	stanp->SetSize(0.25f, 0.25f);

	//gear�͂܂������p�摜
	for (int i = 0; i < 3; i++)
	{
		Gpgear[i] = new RotateObject();
		Gpgear[i]->mSprite->SetTexture(gpTextureGear01);
		Gpgear[i]->mSprite->mCenter.x = 0.86f + (i * 0.145f);
		Gpgear[i]->mSprite->mCenter.y = 0.48f;
		Gpgear[i]->SetSize(0.13f, 0.13f);
		Gpgear[i]->Activate(false);
	}

	// �J�[�\���̈ʒu���Z�b�g����
	SetCursorPos(0.0f, 0.0f);
}

GameScene::~GameScene()
{
	// �@�B�l�`
	delete dool;
	// �M�A����
	delete gj;
	delete jumpGear;
	delete catchiGear;
	delete upGear;
	delete emptiGear;
	// �h�[��
	delete doolMg;
	// �S�[��
	//delete goal;
	// ��
	delete stone;
	// �w�i
	for (int i = 0; i < 2; i++)
	{
		delete background[i];
	}	
	// �}�b�v�`�b�v
	delete UI01;
	delete UI02;
	delete UI03;
	delete UI04;
	// �}�b�v�`�b�v�p
	delete maptip;
	// ��
	//delete key;
	// �M�A�̂͂܂��Ă���摜
	for (int i = 0; i < 3; i++)
	{
		delete Gpgear[i];
	}
	// �t�F�[�h
	delete gpFade;
	// �}�E�X
	delete gpMouse;
	//�X�e�[�W�M�A
	delete StageGearX;
	delete StageGearY;

	// �e�N�X�`���̉��
	COM_SAFE_RELEASE(gpTextureMapTipWall);
	COM_SAFE_RELEASE(gpTextureMapTipHasigo);
	COM_SAFE_RELEASE(gpTextureGoldGear01);
	COM_SAFE_RELEASE(gpTextureGoldGear02);
	COM_SAFE_RELEASE(gpTextureGoldGear03);
	COM_SAFE_RELEASE(gpTextureGoldGear04);
	COM_SAFE_RELEASE(gpTextureGoldGear05);
	COM_SAFE_RELEASE(gpTextureGear01);
	COM_SAFE_RELEASE(gpTextureGear02);
	COM_SAFE_RELEASE(gpTextureGear03);
	COM_SAFE_RELEASE(gpTextureBackGround);
	COM_SAFE_RELEASE(gpTextureFe01);
	COM_SAFE_RELEASE(gpTextureUI01);
	COM_SAFE_RELEASE(gpTextureUI02);
	COM_SAFE_RELEASE(gpTextureUI03);
	COM_SAFE_RELEASE(gpTextureUI04);
	COM_SAFE_RELEASE(gpTextureDoolFallRight);
	COM_SAFE_RELEASE(gpTextureMouse01);
	COM_SAFE_RELEASE(gpTextureBox);
	COM_SAFE_RELEASE(gpTextureDool01);
	COM_SAFE_RELEASE(gpTextureDoolFallLeft);
	COM_SAFE_RELEASE(gpTextureStone);

	for (int i = 0; i < MAX_GP; i++)
	{
		COM_SAFE_RELEASE(gpTextureDoolWalk[i]);

		COM_SAFE_RELEASE(gpTextureDoolJump[i]);

		COM_SAFE_RELEASE(RgpTextureDoolWalk[i]);

		COM_SAFE_RELEASE(RgpTextureDoolJump[i]);

		COM_SAFE_RELEASE(gpTextureDoolPushu[i]);

		COM_SAFE_RELEASE(gpTextureDoolNoboru[i]);
	}

	COM_SAFE_RELEASE(gpTextureMouse02);
}

void GameScene::Update()
{
	// �f���^�^�C�����z��O�̒l�ɂȂ����ꍇ�̏���
	if (gDeltaTime >= 100)
	{
		gDeltaTime = 0;
	}

	//�}�E�X�N���b�N�����特��炷
	//if (Input_GetKeyTrigger(VK_LBUTTON))
	//{
	//	XA_Play(SOUND_LABEL_SE000);
	//}

	if (Input_GetKeyDown(VK_LBUTTON))
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse02);
	}
	else
	{
		gpMouse->mSprite->SetTexture(gpTextureMouse01);
	}

	//CSprite�X�V����	
	gpFade->Update();

	//�����ɏ���������	
	dool->Update();
	gj->Update();
	jumpGear->Update();
	catchiGear->Update();
	upGear->Update();
	emptiGear->Update();
	doolMg->Update();	
	//goal->Update();
	stone->Update();	
	//key->Update();
	gpMouse->Update();
	stanp->Update();
	StageGearX->Update();
	StageGearY->Update();
	for (int i = 0; i < 2; i++)
	{
		background[i]->Update();		
	}	
	UI01->Update();
	UI02->Update();
	UI03->Update();
	UI04->Update();
	for (int i = 0; i < 3; i++)
	{
		Gpgear[i]->Update();
	}	

	//�X�e�[�W�M�A��͂�
	if (gpMouse->mHitCircle->IsHit(StageGearX->mHitCircle) == true)
	{
		if (clickCheck == true)
		{
			StageGearX->SetPosition(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
		}
	}	
	if (gpMouse->mHitCircle->IsHit(StageGearY->mHitCircle) == true)
	{
		if (clickCheck == true)
		{
			StageGearY->SetPosition(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
		}
	}

	//���������M�A�̍X�V����
	for (int i = 0; i < MAX_GEAR; i++)
	{
		gj->mGear[i]->Update();
		jumpGear->mGear[i]->Update();
		catchiGear->mGear[i]->Update();
		upGear->mGear[i]->Update();
	}

	//�M�A��͂ޏ���
	for (int i = 0; i < MAX_GEAR; i++)
	{
		//�M�A����		

		if (gpMouse->mHitCircle->IsHit(gj->mHitCircle) == true)
		{
			if (clickCheck == true && clickCheckAfter == false)
			{
				clickCheckAfter = true;
				gj->Janereter();
			}
		}
		if (gpMouse->mHitCircle->IsHit(jumpGear->mHitCircle) == true)
		{
			if (clickCheck == true && clickCheckAfter == false)
			{
				clickCheckAfter = true;
				jumpGear->Janereter();
			}
		}

		if (gpMouse->mHitCircle->IsHit(catchiGear->mHitCircle) == true)
		{
			if (clickCheck == true && clickCheckAfter == false)
			{
				clickCheckAfter = true;
				catchiGear->Janereter();
			}
		}

		if (gpMouse->mHitCircle->IsHit(upGear->mHitCircle) == true)
		{
			if (clickCheck == true && clickCheckAfter == false)
			{
				clickCheckAfter = true;
				upGear->Janereter();
			}
		}

		for (int i = 0; i < maptip->mapObj.size(); i++)
		{
			maptip->mapObj[i]->Update();

			//�X�e�[�W�M�A���X�e�[�W�ɓ���������
			if (maptip->mapObj[i]->mapNo == 6 && maptip->mapObj[i]->mHitCircle->IsHit(StageGearX->mHitCircle) == true)
			{
				//�X�e�[�W�𓮂���
				if (clickCheck == false)
				{
					ModeMoveFloorX = true;
					StageGearX->Activate(false);
				}
			}
			//�X�e�[�W�M�A���X�e�[�W�ɓ���������
			if (maptip->mapObj[i]->mapNo == 7 && maptip->mapObj[i]->mHitCircle->IsHit(StageGearY->mHitCircle) == true)
			{
				//�X�e�[�W�𓮂���
				if (clickCheck == false)
				{
					ModeMoveFloorY = true;
					StageGearY->Activate(false);
				}
			}
		}

		//�����M�A��͂�
		if (gpMouse->mHitCircle->IsHit(gj->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == true)
			{
				gj->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
			}
		}
		if (gpMouse->mHitCircle->IsHit(jumpGear->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == true)
			{
				jumpGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
			}
		}
		if (gpMouse->mHitCircle->IsHit(catchiGear->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == true)
			{
				catchiGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
			}
		}
		if (gpMouse->mHitCircle->IsHit(upGear->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == true)
			{
				upGear->mGear[i]->MoveGear(gpMouse->mSprite->mCenter.x, gpMouse->mSprite->mCenter.y);
			}
		}

		//�M�A���h�[���ɓ���
		if (doolMg->mHitCircle->IsHit(gj->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == false)
			{
				clickCheckAfter = false;
				gj->mGear[i]->Activate(false);
				doolMg->SetGear(dool, gj->mGear[i]);
				Gpgear[0]->mSprite->SetTexture(gpTextureGear02);
				Gpgear[0]->Activate(true);
			}
		}
		if (doolMg->mHitCircle->IsHit(jumpGear->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == false)
			{
				clickCheckAfter = false;
				jumpGear->mGear[i]->Activate(false);
				doolMg->SetGear(dool, jumpGear->mGear[i]);
			}
		}
		if (doolMg->mHitCircle->IsHit(catchiGear->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == false)
			{
				clickCheckAfter = false;
				catchiGear->mGear[i]->Activate(false);
				doolMg->SetGear(dool, catchiGear->mGear[i]);
			}
		}
		if (doolMg->mHitCircle->IsHit(upGear->mGear[i]->mHitCircle) == true)
		{
			if (clickCheck == false)
			{
				clickCheckAfter = false;
				upGear->mGear[i]->Activate(false);
				doolMg->SetGear(dool, upGear->mGear[i]);
			}
		}
	}		

	////��������n���R���������
	//if (clickCheck == true && clickCheckAfter == false)
	//{
	//	//clickCheckAfter = true;
	//	stanp->mSprite->mColor.a = 1.0f;
	//	stanp->SetPosition(gpMouse->mSprite->mCenter.x - 0.1f, gpMouse->mSprite->mCenter.y - 0.1f);
	//}

	//���t���[�������ɂ��Ă���
	stanp->mSprite->mColor.a -= 0.03f;

	for (int i = 0; i < 3; i++)
	{
		//�����ĂȂ��Ȃ�摜������
		//����ȊO�Ȃ�Ή������摜�ɕύX
		if (dool->mGearSlot[i].nGear_no == 0)
		{
			Gpgear[i]->Activate(false);
		}
		if (dool->mGearSlot[i].nGear_no == 1)
		{
			Gpgear[i]->Activate(true);
			Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear03);
		}
		if (dool->mGearSlot[i].nGear_no == 2)
		{
			Gpgear[i]->Activate(true);
			Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear02);
		}
		if (dool->mGearSlot[i].nGear_no == 3)
		{
			Gpgear[i]->Activate(true);
			Gpgear[i]->mSprite->SetTexture(gpTextureGear03);
		}
		if (dool->mGearSlot[i].nGear_no == 4)
		{
			Gpgear[i]->Activate(true);
			Gpgear[i]->mSprite->SetTexture(gpTextureGoldGear01);
		}
	}

	//�h�[���̒��������鏈���u����
	//�ǂɓ������������Ăяo��
	doolMg->WallHit(dool, maptip);
	//�S�[���ɒ������Ƃ�
	//doolMg->GoalHit(dool, goal, key);
	//�����ɓ��������Ƃ�
	//doolMg->KeyHit(dool, key);
	//�΂��T������
	doolMg->StoneGear(stone, dool);
	//�h�[�����^�񒆂܂ł��ă}�b�v�X�N���[������Ƃ�
	doolMg->DoolPosMapScroll(dool, maptip);

	//�����E�ɓ�����
	if (ModeMoveFloorX)
	{
		maptip->MovefloorX();
	}	
	//������ɓ�����
	if (ModeMoveFloorY)
	{
		maptip->MovefloorY();
	}

	if (dool->mSprite->mCenter.x > 0.0f && dool->GetGearNo(4) == 4)
	{
		for (int i = 0; i < 2; i++)
		{
			background[i]->mSprite->mCenter.x -= dool->GetSpeed();
		}
	}	

	//�h�[������	
	//dool->GeraIdCheck();	
}

void GameScene::Draw()
{
	//DIRECT3D�\���̂ɃA�N�Z�X����
	DIRECT3D* d3d = Direct3D_Get();

	// �w�i
	for (int i = 0; i < 2; i++)
	{
		background[i]->Draw();
	}
	// �}�b�v�`�b�v
	for (int i = 0; i < maptip->mapObj.size(); i++)
	{
		maptip->mapObj[i]->Draw();
	}	
	dool->Draw();
	//��
	if (stone->active == true)
		stone->Draw();
	// ���낢��
	doolMg->Draw();	
	//key->Draw();
	//goal->Draw();	
	// UI
	UI01->Draw();
	UI04->Draw();
	UI03->Draw();
	UI02->Draw();
	// �M�A
	gj->Draw();
	jumpGear->Draw();
	catchiGear->Draw();
	upGear->Draw();
	emptiGear->Draw();
	stanp->Draw();
	StageGearX->Draw();
	StageGearY->Draw();
	for (int i = 0; i < MAX_GEAR; i++)
	{
		gj->mGear[i]->Draw();
		jumpGear->mGear[i]->Draw();
		catchiGear->mGear[i]->Draw();
		upGear->mGear[i]->Draw();
	}
	for (int i = 0; i < 3; i++)
	{
		Gpgear[i]->Draw();
	}
	// �}�E�X
	gpMouse->Draw();
}
