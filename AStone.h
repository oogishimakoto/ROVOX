#pragma once
#include "Accident.h"
class AStone :
	public Accident
{
public:
	int flg;	//�΂Ɉ�񓖂���Ɣ��������

	AStone();
	~AStone();

	void Update() override;
};

