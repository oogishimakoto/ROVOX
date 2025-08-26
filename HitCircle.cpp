#include "HitCircle.h"

bool HitCircle::IsHit(HitCircle * pOther)
{
	if (active == false || pOther->active == false)
	{
		return false;
	}

	float x = pOther->mCenterX - mCenterX;
	float y = pOther->mCenterY - mCenterY;
	float ctc = x * x + y * y;
	float h1_h2 = pOther->mHankei + mHankei;
	return ctc < h1_h2 * h1_h2;
}

HitCircle::HitCircle(float & x, float & y, float hankei) : mCenterX(x),mCenterY(y)
{
	mHankei = hankei;
}
