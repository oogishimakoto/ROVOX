#include "MaptipFileRead.h"
#define MAP_HEIGHT 20
#define MAP_WIDTH 75
extern DWORD gDeltaTime;

MaptipFileRead::~MaptipFileRead()
{
	delete fileData;
	fileData = nullptr;

	for (int i = 0; i < mapObj.size(); i++)
	{
		delete mapObj[i];
	}
}

// CSV�t�H�[�}�b�g�̃f�[�^�P�s��(������)�𐮐��W���ɕϊ�����B
// �����ɕϊ��ł��Ȃ����̂͋����I��(0)�ɕϊ����Ă���B
//
vector<int>* conv_csv_line(string& _str)
{
	string	str;	//getline()�Ő؂�o������������󂯎��ꏊ
	istringstream	iss(_str);	//������X�g���[������
	vector<int>*	p_result = new vector<int>;	//�؂�o����������ϊ���������(int)������ϒ��z��B
	//getline�ŕ�����̍Ō�܂�','�ŋ�؂�ꂽ�������ϊ�����B
	//getline()�͐؂�o������̂������Ȃ��'false'�ɂȂ�B
	while (getline(iss, str, ',')) {
		//�����񂩂琔�l�ւ̕ϊ�(stoi�֐�)�ł́A���l�ȊO�̕������ϊ����悤�Ƃ����ꍇ�A
		//�u��O�v����������̂Łutry�`catch�v�ŏ������Ă���B
		//try {
		//	int num = stoi(str);	//����������𐔎��ɕϊ�
		//	p_result->push_back(num);	//�����ɒǉ�
		//}
		//catch (const invalid_argument& e) {
		//	//��O�F�ϊ��o���Ȃ��l������
		//	p_result->push_back(0);	//�����I��(0)�ɕϊ�����B
		//	//std::cerr << e.what();	//�f�o�b�O�p�G���[�o��
		//}
		//catch (const out_of_range& e) {
		//	//��O�F�͈͊O�̒l������
		//	p_result->push_back(0);	//�����I��(0)�ɕϊ�����B
		//	//std::cerr << e.what();	//�f�o�b�O�p�G���[�o��
		//}

		int num = stoi(str);	//����������𐔎��ɕϊ�
		p_result->push_back(num);	//�����ɒǉ�
	}
	return p_result;	//�ϊ���̐����W���ւ̃|�C���^��Ԃ��B
}	//conv_csv_line


int MaptipFileRead::readFile()
{
	if (this->file_path == nullptr){
		return -1;	//�t�@�C����������
	}
	if (this->fileData != nullptr) {
		return (int)this->size();	//���ɓǂݍ��ݍς݂Ȃ̂ŗv�f����Ԃ�
	}
	//�w�肳�ꂽ�t�@�C��������̓t�@�C���X�g���[�����J��
	ifstream ifs(this->file_path);
	if (!ifs) {
		return -1;	//���s
	}

	//�����̉ϗp�z��(vector<int>)���쐬
	this->fileData = new vector<int>;	//�쐬
	string	line;	//�P�s���ǂݍ��ޕ�����
	//���̓t�@�C���X�g���[������getline()�ōŌ�̍s�܂łP�s�Âǂݍ��ށB
	//getline()�̓t�@�C���̏I���Ȃ��'false'�ɂȂ�B
	while (getline(ifs, line)) {
		vector<int>* line_vec = conv_csv_line(line);	//�P�s��','��؂�̃f�[�^�Ƃ��āA�����̐����l�ɕϊ�
		fileData->insert(fileData->end(), line_vec->begin(), line_vec->end());	//�ϊ������P�s����'p_vec'�̖����ɒǉ�
		delete line_vec;	//�ǉ������̂ŕϊ��Ɏg�����I�u�W�F�N�g�͍폜
		line_vec = nullptr;	//�O�̂���'nullptr'�ŃN���A
	}
	ifs.close();	//���̓t�@�C���X�g���[�������
	return (int)this->size();	//�ϊ������v�f����Ԃ�
}

void MaptipFileRead::mapObjSet()
{
	float map_width = 0.1f;
	float map_hight = 0.1f;

	int data_size = this->readFile();	//CSV�t�@�C����ǂݍ��ށB�߂�l�͓ǂݍ��񂾃f�[�^���iint�f�[�^�̐��j
	if (data_size < 0) {
		//read()�̖߂�l���O��}�C�i�X�l�Ȃ�G���[�B
		std::cout << "CSV�ǂݍ��ݎ��s�B" << std::endl;
	}
	//�ǂݎ�����f�[�^�̃|�C���^�i�z��̐擪�A�h���X�j���擾�B
	const int* p_csv_data = this->data();	//�ǂݎ�����f�[�^��"p_csv_data[0]�`[data_size-1]"�ɓ����Ă���B
	//CSV�f�[�^�͐����̂P�����z��ɕϊ������̂ŁA�������Ń^�e���R�����̐������߂�B
	//���̏ꍇ�́A�������̐���'MAP_WIDTH'�A�c�����̐���'MAP_HEIGHT'�Ƃ��Ĉ����Ă���B	
	//�Q�����Ƃ��ĕ\�����m�F����B
	for (int y = 0; y < MAP_HEIGHT; y++) {
//	for (int y = MAP_HEIGHT - 1; y>=0; y--) {
			//�c�����̐������J��Ԃ�
		for (int x = 0; x < MAP_WIDTH; x++) {
			//�������̐������J��Ԃ��B
			int idx = (y * MAP_WIDTH + x);
			//�P�����z��̃T�C�Y�𒴂��Ȃ��l�ɂ���B�������ꍇ�͉������Ȃ��B
			if (idx < data_size) {
				
				float colorRand = rand() % 5 + 5;

				MapObject* map = new MapObject();								
				map->mSprite->mSize.x = map_width;
				map->mSprite->mSize.y = map_hight;
				map->mSprite->mCenter.x = map_width * x - 1.3f;
				map->mSprite->mCenter.y = map_hight * -y + 0.7f;
				map->mHitSquare->mSizeY = 0.1f;
				map->mapNo = p_csv_data[idx];
				//�摜�ǂݍ���
				switch (map->mapNo)
				{
				case NONE:
					//map->mSprite->SetTexture(gpTextureMapTipWall);
					//map->mSprite->SetTexture(gpTextureFe01);
					break;
				case STAGE:					
					//map->mSprite->SetTexture(gpTextureMapTipWall);
					map->mSprite->SetTexture(gpTextureBox2);
					map->mSprite->SetColor({ colorRand, colorRand, colorRand, 1.0f });
					break;
				case HASHIGO:
					map->mSprite->SetTexture(gpTextureMapTipHasigo);					
					break;
				case BOX:
					map->mSprite->SetTexture(gpTextureBox);
					map->mSprite->SetColor({ 8.0f, 8.0f, 8.0f, 1.0f });
					break;
				case GOAL:
					map->mSprite->SetTexture(gpTextureGoal);
					map->mSprite->mSize.x = 0.1f;
					map->mSprite->mSize.y = 0.2f;
					break;
				case KEY:
					map->mSprite->SetTexture(gpTextureKey);
					break;
				case MOVEX:
					map->mSprite->SetTexture(gpTextureBox);
					map->mSprite->SetColor({ 6.0f, 6.0f, 6.0f, 1.0f });
					break;
				case MOVEY:
					map->mSprite->SetTexture(gpTextureMapTipHasigo);
					break;
				case MOVEX2:
					map->mSprite->SetTexture(gpTextureBox);
					map->mSprite->SetColor({ 6.0f, 6.0f, 6.0f, 1.0f });
					break;
				case MOVEX3:
					map->mSprite->SetTexture(gpTextureBox);
					map->mSprite->SetColor({ 6.0f, 6.0f, 6.0f, 1.0f });
					break;
				case MOVEX4:
					map->mSprite->SetTexture(gpTextureBox);
					map->mSprite->SetColor({ 6.0f, 6.0f, 6.0f, 1.0f });
					break;
				case MOVEX5:
					map->mSprite->SetTexture(gpTextureBox);
					map->mSprite->SetColor({ 6.0f, 6.0f, 6.0f, 1.0f });
					break;
				case NOTHASHIGO:
					map->mSprite->SetTexture(gpTextureMapTipHasigo);
					break;
				case 13:
					map->mSprite->SetTexture(gpTextureMapTipHasigo);
					break;
				case 14:
					map->mSprite->SetTexture(gpTextureMapTipHouse03);
					map->mSprite->mSize.y = 0.3f;
					map->mSprite->mSize.x = 0.3f;
					map->mSprite->mCenter.y += 0.1f;
					break;
				default:
					break;
				}
				this->mapObj.emplace_back(map);
			}
		}
		std::cout << std::endl;	//���s
	}
}

void MaptipFileRead::mapScroll(float _speed)
{
	for (int i = 0; i < this->mapObj.size(); i++)
	{		
		this->mapObj[i]->mSprite->mCenter.x -= _speed;
	}
}

void MaptipFileRead::MovefloorX()
{
	float pos = MovePosX / (MoveCount * 100);

	//�񐔂���������ړ�����߂�
	if (countX > MoveCount)
	{
		MovePosX = 0;
	}	

	for (int i = 0; i < this->mapObj.size(); i++)
	{
		if (this->mapObj[i]->mapNo == 6)
		{
			this->mapObj[i]->mSprite->mCenter.x += pos;
		}
	}

	//������
	countX++;
}

void MaptipFileRead::MovefloorX2()
{
	float pos = MovePosX2 / (MoveCount * 100);

	//�񐔂���������ړ�����߂�
	if (countX2 > MoveCount)
	{
		MovePosX2 = 0;
	}

	for (int i = 0; i < this->mapObj.size(); i++)
	{
		if (this->mapObj[i]->mapNo == 8)
		{
			this->mapObj[i]->mSprite->mCenter.x += pos;
		}
	}

	//������
	countX2++;
}

void MaptipFileRead::MovefloorX3()
{
	float pos = MovePosX3 / (MoveCount * 100);

	//�񐔂���������ړ�����߂�
	if (countX3 > MoveCount)
	{
		MovePosX3 = 0;
	}

	for (int i = 0; i < this->mapObj.size(); i++)
	{
		if (this->mapObj[i]->mapNo == 9)
		{
			this->mapObj[i]->mSprite->mCenter.x += pos;
		}
	}

	//������
	countX3++;
}

void MaptipFileRead::MovefloorX4()
{
	float pos = MovePosX4 / (MoveCount * 100);

	//�񐔂���������ړ�����߂�
	if (countX4 > MoveCount)
	{
		MovePosX4 = 0;
	}

	for (int i = 0; i < this->mapObj.size(); i++)
	{
		if (this->mapObj[i]->mapNo == 10)
		{
			this->mapObj[i]->mSprite->mCenter.x += pos;
		}
	}

	//������
	countX4++;
}

void MaptipFileRead::MovefloorX5()
{
	float pos = MovePosX5 / (MoveCount * 100);

	//�񐔂���������ړ�����߂�
	if (countX5 > MoveCount)
	{
		MovePosX5 = 0;
	}

	for (int i = 0; i < this->mapObj.size(); i++)
	{
		if (this->mapObj[i]->mapNo == 11)
		{
			this->mapObj[i]->mSprite->mCenter.x += pos;
		}
	}

	//������
	countX5++;
}

void MaptipFileRead::MovefloorY()
{
	float pos = MovePosY / (MoveCount * 100);

	//�񐔂���������ړ�����߂�
	if (countY > MoveCount)
	{
		MovePosY = 0;
	}

	for (int i = 0; i < this->mapObj.size(); i++)
	{
		if (this->mapObj[i]->mapNo == 7)
		{
			this->mapObj[i]->mSprite->mCenter.y -= pos;
		}
	}

	//������
	countY++;
}
