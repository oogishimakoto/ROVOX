#include "MapObject.h"

//MapObject::MapObject()
//{
//
//	float map_width = 0.1f;
//	float map_hight = 0.1f;
//
//	int data_size = this->readFile();	//CSV�t�@�C����ǂݍ��ށB�߂�l�͓ǂݍ��񂾃f�[�^���iint�f�[�^�̐��j
//	if (data_size < 0) {
//		//read()�̖߂�l���O��}�C�i�X�l�Ȃ�G���[�B
//		std::cout << "CSV�ǂݍ��ݎ��s�B" << std::endl;		
//	}
//
//	//�ǂݎ�����f�[�^�̃|�C���^�i�z��̐擪�A�h���X�j���擾�B
//	const int* p_csv_data = this->data();	//�ǂݎ�����f�[�^��"p_csv_data[0]�`[data_size-1]"�ɓ����Ă���B
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