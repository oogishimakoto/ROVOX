#pragma once
#include"AnimHitObject.h"
#include"Gear.h"
#include"GameScene.h"

#define ANIMATIONVALUES 60

class StaticObject;

class Dool : public AnimHitObject
{
private:		
	//ギアはめる場所何番目か
	int mGcount = 0;
	//逆回転かどうか
	bool mReverse = true;

	//加速度
	float acc = -0.00001f;

	//ジャンプできるか
	bool canAcc = true;

	//y方向の速度
	float vel = -0.15f;

	//初速が大きくないと地面から離れないので一度だけ値をこっち入れる
	float onceVelAdd = 0.017f;
	//一回入れたかチェックするよう
	bool onceVelCheck = false;

	//ジャンプできるか
	bool canJump = false;
	bool JumpState = false;

	float old_posX = 0;
	float old_posY = 0;	

	//ジャンプ用アニメーション変数
	int AnimJumpCount = 0;
	float AnimJumpSpeed = 0.7f;
	float AnimJumpChangeValues = 0.0f;	

	//歩く速度
	float walk = 8.0f;

	//歩くアニメーションの判定
	bool canWalkAnim = false;

	//変更しない速度=Setしない
	//これの値をマップスクロールやまた移動させるときに代入する
	float speed = 800.0f;

	//走る時の加速度
	float dashAcc = 1.0f;

	StaticObject* talkObj;

public:

	//更新処理
	void Update() override;

	//描画処理
	void Draw() override;

	//初期化
	Dool();

	//解放
	~Dool();

	//行動
	void Act();

	//ギア判別
	void GeraIdCheck();

	//ギア格納
	void GearDist(GEAR_ID _id);

	//ダッシュ
	void Dashu();

	//ジャンプ
	void Jump();

	//セッター
	void SetmReverse(bool _reverse);

	void SetOldPosX(float);
	void SetOldPosY(float);

	void SetCanJump(bool _jump);

	void SetAcc(float _acc);
	void SetVel(float _vel);

	void SetWalk(float _walk);

	void SetAnimJumpCoount(int _count);
	void SetAnimJumpChangeValues(float _values);

	void SetAccCheck(bool _acc);

	void SetCanWalkAnim(bool _WalkAnim);	//ドールの歩くアニメーションをできるかの判定をセット

	//ゲッター
	float GetOldPosX();
	float GetOldPosY();

	bool GetCanJump();

	float GetSpeed();

	float GetWalk();

	//ギアをすべて外す(aoi作)
	void GearDelete();

	//指定したギアがあったらその値を返す
	//なかったら0を返す
	//引数：欲しいギアの番号
	int GetGearNo(int _no);

	//移動用アニメーション変数
	//移動時のアニメーションの切り替え番号
	int AnimWalkCoount = 0;

	//アニメーション切り替え速度
	float AnimWalkSpeed = 1.4f;

	//アニメーション切り替える値
	float AnimWalkChangeValues = 0.0f;

	//ギアをはめるところ
	GEAR_ID mGearSlot[3];

	//梯子を登るかどうかで移動するか決める
	bool UpHashigo = false;

	//登る用アニメーション変数
	int AnimNoboruCount = 0;
	float AnimNoboruSpeed = 0.05f;
	float AnimNoboruChangeValues = 0.0f;

	//ドールの行動の優先順
	//「登る→ジャンプ→押す→歩く」の順番に優先して画像をアニメーションさせる
	bool priority = true;

	//ジャンプアニメーションの判定
	bool canJumpAnim = true;

	//ある一定の高さから落ちたらコケる画像に変えてギアを外す
	float HighAcc;

	float HighAccCheck = 0.4f;	// ダウン4段
	float JumpHighAccCheck = 2.0f;	//ジャンプ4段

	// ダウン判定関数
	bool GearCrash();
	
	//seカウント用
	bool jump_seCount = true;
	bool walk_seCount = true;

	//会話用の変数
	int talk = 0;
};

