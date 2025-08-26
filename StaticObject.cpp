#include "StaticObject.h"

StaticObject::StaticObject()
{
	// アニメーションなし表示機能を作成
	mSprite = new CSprite();
}

StaticObject::~StaticObject()
{
	delete mSprite;
}

void StaticObject::Update()
{
	mSprite->Update();
}

void StaticObject::Draw()
{
	if (active == true)
	{
		mSprite->Draw();
	}
}

void StaticObject::Activate(bool state)
{
	active = state;
}
