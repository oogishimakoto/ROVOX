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

// CSVフォーマットのデータ１行分(文字列)を整数集合に変換する。
// 数字に変換できないものは強制的に(0)に変換している。
//
vector<int>* conv_csv_line(string& _str)
{
	string	str;	//getline()で切り出した文字列を受け取る場所
	istringstream	iss(_str);	//文字列ストリーム入力
	vector<int>*	p_result = new vector<int>;	//切り出した数字を変換した整数(int)を入れる可変長配列。
	//getlineで文字列の最後まで','で区切られた文字列を変換する。
	//getline()は切り出せるものが無くなると'false'になる。
	while (getline(iss, str, ',')) {
		//文字列から数値への変換(stoi関数)では、数値以外の文字列を変換しようとした場合、
		//「例外」が発生するので「try～catch」で処理している。
		//try {
		//	int num = stoi(str);	//数字文字列を数字に変換
		//	p_result->push_back(num);	//末尾に追加
		//}
		//catch (const invalid_argument& e) {
		//	//例外：変換出来ない値だった
		//	p_result->push_back(0);	//強制的に(0)に変換する。
		//	//std::cerr << e.what();	//デバッグ用エラー出力
		//}
		//catch (const out_of_range& e) {
		//	//例外：範囲外の値だった
		//	p_result->push_back(0);	//強制的に(0)に変換する。
		//	//std::cerr << e.what();	//デバッグ用エラー出力
		//}

		int num = stoi(str);	//数字文字列を数字に変換
		p_result->push_back(num);	//末尾に追加
	}
	return p_result;	//変換後の整数集合へのポインタを返す。
}	//conv_csv_line


int MaptipFileRead::readFile()
{
	if (this->file_path == nullptr){
		return -1;	//ファイル名が無い
	}
	if (this->fileData != nullptr) {
		return (int)this->size();	//既に読み込み済みなので要素数を返す
	}
	//指定されたファイルから入力ファイルストリームを開く
	ifstream ifs(this->file_path);
	if (!ifs) {
		return -1;	//失敗
	}

	//整数の可変用配列(vector<int>)を作成
	this->fileData = new vector<int>;	//作成
	string	line;	//１行分読み込む文字列
	//入力ファイルストリームからgetline()で最後の行まで１行づつ読み込む。
	//getline()はファイルの終わりなると'false'になる。
	while (getline(ifs, line)) {
		vector<int>* line_vec = conv_csv_line(line);	//１行を','区切りのデータとして、複数の整数値に変換
		fileData->insert(fileData->end(), line_vec->begin(), line_vec->end());	//変換した１行分を'p_vec'の末尾に追加
		delete line_vec;	//追加したので変換に使ったオブジェクトは削除
		line_vec = nullptr;	//念のため'nullptr'でクリア
	}
	ifs.close();	//入力ファイルストリームを閉じる
	return (int)this->size();	//変換した要素数を返す
}

void MaptipFileRead::mapObjSet()
{
	float map_width = 0.1f;
	float map_hight = 0.1f;

	int data_size = this->readFile();	//CSVファイルを読み込む。戻り値は読み込んだデータ数（intデータの数）
	if (data_size < 0) {
		//read()の戻り値が０やマイナス値ならエラー。
		std::cout << "CSV読み込み失敗。" << std::endl;
	}
	//読み取ったデータのポインタ（配列の先頭アドレス）を取得。
	const int* p_csv_data = this->data();	//読み取ったデータは"p_csv_data[0]～[data_size-1]"に入っている。
	//CSVデータは整数の１次元配列に変換されるので、扱う側でタテヨコ方向の数を決める。
	//この場合は、横方向の数は'MAP_WIDTH'、縦方向の数は'MAP_HEIGHT'として扱っている。	
	//２次元として表示し確認する。
	for (int y = 0; y < MAP_HEIGHT; y++) {
//	for (int y = MAP_HEIGHT - 1; y>=0; y--) {
			//縦方向の数だけ繰り返す
		for (int x = 0; x < MAP_WIDTH; x++) {
			//横方向の数だけ繰り返す。
			int idx = (y * MAP_WIDTH + x);
			//１次元配列のサイズを超えない様にする。超えた場合は何もしない。
			if (idx < data_size) {
				
				float colorRand = rand() % 5 + 5;

				MapObject* map = new MapObject();								
				map->mSprite->mSize.x = map_width;
				map->mSprite->mSize.y = map_hight;
				map->mSprite->mCenter.x = map_width * x - 1.3f;
				map->mSprite->mCenter.y = map_hight * -y + 0.7f;
				map->mHitSquare->mSizeY = 0.1f;
				map->mapNo = p_csv_data[idx];
				//画像読み込み
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
		std::cout << std::endl;	//改行
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

	//回数が超えたら移動をやめる
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

	//数える
	countX++;
}

void MaptipFileRead::MovefloorX2()
{
	float pos = MovePosX2 / (MoveCount * 100);

	//回数が超えたら移動をやめる
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

	//数える
	countX2++;
}

void MaptipFileRead::MovefloorX3()
{
	float pos = MovePosX3 / (MoveCount * 100);

	//回数が超えたら移動をやめる
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

	//数える
	countX3++;
}

void MaptipFileRead::MovefloorX4()
{
	float pos = MovePosX4 / (MoveCount * 100);

	//回数が超えたら移動をやめる
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

	//数える
	countX4++;
}

void MaptipFileRead::MovefloorX5()
{
	float pos = MovePosX5 / (MoveCount * 100);

	//回数が超えたら移動をやめる
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

	//数える
	countX5++;
}

void MaptipFileRead::MovefloorY()
{
	float pos = MovePosY / (MoveCount * 100);

	//回数が超えたら移動をやめる
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

	//数える
	countY++;
}
