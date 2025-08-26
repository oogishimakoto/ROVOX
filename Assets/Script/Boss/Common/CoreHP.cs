using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoreHP : MonoBehaviour
{
    [SerializeField] int MAXHP = 60;
    [SerializeField] int MinHP = 10;
    [SerializeField] float endPos;

    [SerializeField] int HP = 60;

    [Header("ダメージSE")]
    [SerializeField] AudioClip SE;
    AudioSource source;

    private float damageVec;  //1HP当たりの移動ベクトル

    private void Start()
    {
         damageVec = endPos / MAXHP;
        HP = MAXHP;

        source = transform.AddComponent<AudioSource>();
        GameObject.Find("SEManager").GetComponent<SEManager>().SetAudioSource(source);

    }

    //HPを減らす関数
    public void HPDecrease(int _value)
    {
        HP -= _value;
        this.transform.position -= transform.forward* damageVec * _value;

        source.clip = SE;
        source.loop = false;
        source.PlayOneShot(source.clip);
    }

    //HPが残っているかチェック関数
    public bool CheckHP()
    {
        //コアのHPが0以下ならtrueを返す
        if(HP <= 0)
        {
            return true;
        }

        return false;
    }

    //コアの最低体力まで回復させる関数
    private void CoreRefresh()
    {
        if (HP < MinHP)
        {
            //最低体力と現在のHPの差分コアの位置を戻して最低体力まで回復させる
            this.transform.position += transform.forward * damageVec *( MinHP - HP);
            HP = MinHP;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        //銛が引き抜かれたときにコアの最低体力まで回復させる
        if (other.transform.tag == "Player")
        {

            CoreRefresh();
        }
    }

}
