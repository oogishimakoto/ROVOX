#include "GameObject.h"

void GameObject::Update()
{
	mSprite->Update();	
}

void GameObject::Draw()
{
	if (this->active == true)
	{
		mSprite->Draw();
	}	
}

void GameObject::Activate(bool state)
{
	active = state;
}

void GameObject::SetPosition(float _x, float _y)
{
	mSprite->mCenter = { _x,_y };
}

void GameObject::SetSize(float _w, float _h)
{
	mSprite->mSize = { _w,_h };
}
