using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullUI : MonoBehaviour
{
    public Sprite sprite0;        // 2�Ԗڂ̉摜
    public Sprite sprite1;        // �ŏ��̉摜
    public Sprite sprite2;        // 2�Ԗڂ̉摜
    public float switchInterval = 2.0f;  // �؂�ւ��Ԋu�i�b�j

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
        // �q�I�u�W�F�N�g����Image�R���|�[�l���g���擾
        imageComponent = this.GetComponentInChildren<Image>();
        nowsprite = sprite1;

        // ������
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
                    // �^�C�}�[���X�V
                    timer += Time.deltaTime;

                    // ��莞�Ԃ��o�߂�����摜��؂�ւ���
                    if (timer >= switchInterval)
                    {

                        // �摜��؂�ւ�
                        if (isShowingFirstSprite)
                        {
                            nowsprite = sprite2;
                        }
                        else
                        {
                            nowsprite = sprite1;
                        }

                        // �t���O�𔽓]
                        isShowingFirstSprite = !isShowingFirstSprite;

                        // �^�C�}�[�����Z�b�g
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
