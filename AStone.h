#pragma once
#include "Accident.h"
class AStone :
	public Accident
{
public:
	int flg;	//Î‚Éˆê‰ñ“–‚½‚é‚Æ”»’è‚ğÁ‚·

	AStone();
	~AStone();

	void Update() override;
};

