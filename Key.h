#pragma once
#include "HitObject.h"
//�h�[�����Q�b�g������E�[
class Key :	public HitObject
{
public:

	//�Q�b�^�[
	bool GetKey();

	//�Z�b�^�[
	void SetKey(bool _key);

private:
	bool keyget = false;
};

