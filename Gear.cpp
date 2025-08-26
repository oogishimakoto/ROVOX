#include "Gear.h"
#define PI 3.14

Gear::Gear()
{
}

Gear::Gear(GEAR_ID Dogear)
{
	//‰Šú‰»
	mDogear = Dogear;
}

Gear::~Gear()
{
}

GEAR_ID Gear::GetGear()
{
	return mDogear;
}

void Gear::MoveGear(float _xpos, float _ypos)
{
	this->mSprite->mCenter = {_xpos,_ypos};
}

int Gear::GetGearNo()
{
	return mDogear.nGear_no;
}

void Gear::Update()
{	
	RotateObject::Update();
}
