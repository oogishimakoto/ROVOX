using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullUI : MonoBehaviour
{
    public Sprite sprite0;        // 2番目の画像
    public Sprite sprite1;        // 最初の画像
    public Sprite sprite2;        // 2番目の画像
    public float switchInterval = 2.0f;  // 切り替え間隔（秒）

    private Image imageComponent;
    private Sprite nowsprite;

    private float timer;
    private bool isShowingFirstSprite;

    Bullet mori;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("mori") != null)
        {
            mori = GameObject.Find("mori").GetComponent<Bullet>();
        }
        // 子オブジェクトからImageコンポーネントを取得
        imageComponent = this.GetComponentInChildren<Image>();
        nowsprite = sprite1;

        // 初期化
        timer = 0.0f;
        isShowingFirstSprite = true;
        if (imageComponent != null)
        {
        }
        else
        {
            Debug.LogError("Image component not found in children.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mori != null)
        {
            if (mori.GetBulletState() == Bullet.State.PULL || mori.GetBulletState() == Bullet.State.HEALPULL)
            {

                if (imageComponent != null)
                {
                    imageComponent.sprite = nowsprite;
                    // タイマーを更新
                    timer += Time.deltaTime;

                    // 一定時間が経過したら画像を切り替える
                    if (timer >= switchInterval)
                    {

                        // 画像を切り替え
                        if (isShowingFirstSprite)
                        {
                            nowsprite = sprite2;
                        }
                        else
                        {
                            nowsprite = sprite1;
                        }

                        // フラグを反転
                        isShowingFirstSprite = !isShowingFirstSprite;

                        // タイマーをリセット
                        timer = 0.0f;
                    }
                }

            }
            else
            {
                imageComponent.sprite = sprite0;

            }

        }

    }
}
