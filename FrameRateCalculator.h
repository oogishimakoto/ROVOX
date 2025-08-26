#pragma once

#include <thread>
#include <chrono>
#include <string>

//�t���[�����[�g�v�Z�N���X
class FrameRateCalculator
{
	long long cnt = 0;
	const int limit = 60;
	std::string fpsStr = "0fps";
	long long time = currentTime();

	//���ݎ������擾����֐�
	long long currentTime()
	{
		std::chrono::system_clock::duration d = std::chrono::system_clock::now().time_since_epoch();
		return std::chrono::duration_cast<std::chrono::milliseconds>(d).count();
	}

	//�t���[�����[�g�̌v�Z�ƌ��ʕ�������\�z����
	void updateStr()
	{
		//fps���v�Z���A������Ƃ��ĕێ�����
		long long end = currentTime();
		double fpsResult = (double)(1000) / (end - time) * cnt;
		time = end;
		fpsStr = std::to_string(fpsResult) + "fps";
		cnt = 0;
	}
public:

	//�t���[�����[�g�X�V���\�b�h
	std::string* update()
	{
		cnt++;
		//�K��t���[�����ɂȂ�����t���[�����[�g�̍X�V
		if (limit <= cnt)
		{
			updateStr();
		}
		return &fpsStr;
	}
};
