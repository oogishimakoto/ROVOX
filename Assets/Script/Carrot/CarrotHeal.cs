using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotHeal : MonoBehaviour
{
    //最大体力
    [SerializeField] int MAXHP = 20;
    [SerializeField] float endPos; //目的場所

    [SerializeField] int HP = 20;//残り体力

    [Header("stem_ALLを入れる")]
    [SerializeField] GameObject targetpos;  //進む方向

    private float damageVec;  //1HP当たりの移動ベクトル

    PlayerInfo playerInfo;　 //プレイヤーのhp情報を読み取る用
    int playermaxhp = 100;  //プレイヤーの体力

    Bullet bullet;　 //バレットを返す時に使う


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        damageVec = endPos / HP;
        HP = MAXHP;
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
        playermaxhp = playerInfo.HP;
        if (targetpos == null)
        {
            for(int i = 0;i < transform.childCount; i++) 
            {
                if(transform.GetChild(i).name == "stem_ALL")
                    targetpos = transform.GetChild(1).gameObject;
            }

            

        }
    }

    private void Update()
    {
        //人参の体力チェック
        if(Check())
        {
            //プレイヤーの体力を回復できる量を判定
            if(playerInfo.HP + playerInfo.healPoint <= playermaxhp)
            {
                //合計値が最大体力より少ないので回復量分回復
                playerInfo.HP += playerInfo.healPoint;

            }
            else
            {
                //合計値が最大体力より多いので最大体力にする
                playerInfo.HP = playermaxhp;
            }
            
            bullet.BulletReturn();  //銛を返す

           
        }

        if (transform.GetChild(0).childCount <= 0)
        {
            gameObject.SetActive(false);
            enabled = false;
        }
           
    }

    //HPを減らす関数
    public void Decrease(int _value)
    {
        if(enabled)
        {
            HP -= _value;
            //人参の位置をダメージを受けるごとに移動させる　方向　×　1HP当たりの移動ベクトル　×　受けたダメージ
            this.transform.position -= (targetpos.transform.position - transform.position).normalized * damageVec * _value;
        }
    }

    public bool Check()
    {
        //コアのHPが0以下ならtrueを返す
        if (HP <= 0)
        {
            
            return true;
           
        }

        return false;
    }


}
