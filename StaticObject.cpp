#include "StaticObject.h"

StaticObject::StaticObject()
{
	// �A�j���[�V�����Ȃ��\���@�\���쐬
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
