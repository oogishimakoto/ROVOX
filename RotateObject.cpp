#include "RotateObject.h"

RotateObject::RotateObject()
{
	mSprite = new RotateSprite();

	//ƒTƒCƒYŒÅ’è
	mSprite->mSize = { 0.3f,0.3f };
	//mSprite->mSize = { 1.0f,1.0f };

	mHitCircle = new HitCircle(mSprite->mCenter.x, mSprite->mCenter.y, 0.1f);
	mHitSquare = new HitSquare(mSprite->mCenter.x, mSprite->mCenter.y, mSprite->mSize.x, mSprite->mSize.y);
}

RotateObject::~RotateObject()
{
	delete mSprite;
	delete mHitCircle;
	mHitCircle = nullptr;
	delete mHitSquare;
	mHitCircle = nullptr;
}

void RotateObject::Update()
{
	mSprite->Update();
}

void RotateObject::Draw()
{
	if (active == true)
	{
		mSprite->Draw();
	}	
}

void RotateObject::Activate(bool state)
{
	mHitCircle->active = state;
	mHitSquare->active = state;
	active = state;
}

void RotateObject::SetPosition(float _x, float _y)
{
	mSprite->mCenter = { _x,_y };
}

void RotateObject::SetSize(float _w, float _h)
{
	mSprite->mSize = { _w,_h };
}

void RotateObject::SetDoRotate(bool _r)
{
	mSprite->DoRotate = _r;
}

void RotateObject::SetDoingRotate(bool _r)
{
	mSprite->DoingRotate = _r;
}
