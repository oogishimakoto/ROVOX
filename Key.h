#pragma once
#include "HitObject.h"
//ドールがゲットしたら右端
class Key :	public HitObject
{
public:

	//ゲッター
	bool GetKey();

	//セッター
	void SetKey(bool _key);

private:
	bool keyget = false;
};

