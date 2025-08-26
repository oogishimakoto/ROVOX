#pragma once

#include <thread>
#include <chrono>
#include <string>

//フレームレート計算クラス
class FrameRateCalculator
{
	long long cnt = 0;
	const int limit = 60;
	std::string fpsStr = "0fps";
	long long time = currentTime();

	//現在時刻を取得する関数
	long long currentTime()
	{
		std::chrono::system_clock::duration d = std::chrono::system_clock::now().time_since_epoch();
		return std::chrono::duration_cast<std::chrono::milliseconds>(d).count();
	}

	//フレームレートの計算と結果文字列を構築する
	void updateStr()
	{
		//fpsを計算し、文字列として保持する
		long long end = currentTime();
		double fpsResult = (double)(1000) / (end - time) * cnt;
		time = end;
		fpsStr = std::to_string(fpsResult) + "fps";
		cnt = 0;
	}
public:

	//フレームレート更新メソッド
	std::string* update()
	{
		cnt++;
		//規定フレーム数になったらフレームレートの更新
		if (limit <= cnt)
		{
			updateStr();
		}
		return &fpsStr;
	}
};
