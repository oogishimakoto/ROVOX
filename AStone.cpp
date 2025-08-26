#include "AStone.h"

AStone::AStone()
{
	flg = 0;
}

AStone::~AStone()
{

}

void AStone::Update()
{
	this->mSprite->mSize.x = 0.05f;
	this->mSprite->mSize.y = 0.05f;
	this->mHitCircle->mHankei = 0.005f;


	Accident::Update();
}
