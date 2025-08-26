#include "MapObject.h"

//MapObject::MapObject()
//{
//
//	float map_width = 0.1f;
//	float map_hight = 0.1f;
//
//	int data_size = this->readFile();	//CSVファイルを読み込む。戻り値は読み込んだデータ数（intデータの数）
//	if (data_size < 0) {
//		//read()の戻り値が０やマイナス値ならエラー。
//		std::cout << "CSV読み込み失敗。" << std::endl;		
//	}
//
//	//読み取ったデータのポインタ（配列の先頭アドレス）を取得。
//	const int* p_csv_data = this->data();	//読み取ったデータは"p_csv_data[0]～[data_size-1]"に入っている。
//
//	for (int t = 0; t < MAX_HIGHT; t++)
//	{
//		for (int j = 0; j < MAX_WIDHT; j++)
//		{
//			int idx = (t * MAX_WIDHT + j);
//
//			AnimHitObject* map = new AnimHitObject();
//			map->mSprite->SetTexture(gpTextureFe01);
//			map->mSprite->mSize.x = map_width + 2.6;
//			map->mSprite->mSize.y = map_hight;
//			map->mSprite->mCenter.x = map_width * t - 0.98f;
//			map->mSprite->mCenter.y = map_hight * j - 0.92f;
//
//			this->MapTip.emplace_back(map);
//		}
//	}
//}

//MapObject::~MapObject()
//{
//	for (int i = 0; i < MapTip.size(); i++)
//	{
//		delete MapTip[i];
//	}
//}

MapObject::MapObject()
{
	
}

MapObject::~MapObject()
{
	
}