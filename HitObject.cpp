#include "HitObject.h"

HitObject::HitObject()
{
	mSprite = new CSprite();
	//ƒTƒCƒYŒÅ’è
	mSprite->mSize = { 0.3f,0.3f };
	//mSprite->mSize = { 1.0f,1.0f };

	mHitCircle = new HitCircle(mSprite->mCenter.x, mSprite->mCenter.y, 0.1f);
	mHitSquare = new HitSquare(mSprite->mCenter.x, mSprite->mCenter.y, mSprite->mSize.x, mSprite->mSize.y);
}

HitObject::~HitObject()
{
	delete mHitCircle;
	mHitCircle = nullptr;
	delete mHitSquare;
	mHitCircle = nullptr;
}

void HitObject::Activate(bool state)
{
	mHitCircle->active = state;
	mHitSquare->active = state;
	GameObject::Activate(state);
}
