#include "Key.h"

bool Key::GetKey()
{
	return keyget;
}

void Key::SetKey(bool _key)
{
	keyget = _key;
}
