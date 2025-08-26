#pragma once
#include "AnimHitObject.h"
//マップサイズ
#define MAX_HIGHT 40
#define MAX_WIDHT 20

class MapObject : public AnimHitObject
{
public:

	//コンストラクタ
	MapObject();

	~MapObject();

	//マップ識別番号
	int mapNo;

private:	
};

