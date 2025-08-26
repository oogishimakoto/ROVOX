#include "Operation.h"

extern POINT cursor;

void Operation::Update()
{
	// オブジェクトの中心点が画面のど真ん中なので、
	// 左上端からマウス座標を取得してるのでそれに合わせる
	double saveX = (cursor.x - SCREEN_WIDTH / 2) / (SCREEN_WIDTH / 2);
	double saveY = (-cursor.y + SCREEN_HEIGHT / 2) / (SCREEN_HEIGHT / 2);

	// 画面サイズと頂点シェーダーで設定している画像の大きさの差分を合わせる
	saveX *= 640.0f / 480.0f;
	saveY *= 480.0f / 640.0f;

	// 差分を引いた変数をオブジェクトのX座標とY座標に置き換える
	this->mSprite->mCenter.x = saveX;
	this->mSprite->mCenter.y = saveY;

	AnimHitObject::Update();
}