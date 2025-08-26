using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class StoneBreak : MonoBehaviour
{

    //敵キャラクターの所持ブロック数
    private int n_MaxHP;
    private int n_NowHP;

    public GameObject player;

    public int NeedKillEnemy = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //ブロックの初期の所持数を計算
            n_MaxHP += transform.GetChild(i).childCount;
        }

        //初期の所持数を現在の所持数に合わせる
        n_NowHP = n_MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(n_NowHP < n_MaxHP * 0.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if(player.gameObject.GetComponent<PlayerMove>().GetKillenemyCount() >= NeedKillEnemy && other.name == "mori")
        {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform childObj = transform.GetChild(i);
                //コライダーがないなら付ける
                if (!childObj.GetComponent<BoxCollider>())
                {
                    childObj.AddComponent<BoxCollider>();
                    

                    // 元のGameObjectのサイズを取得
                    Bounds bounds = childObj.GetComponent<MeshFilter>().sharedMesh.bounds;

                    // BoxColliderのサイズを設定
                    childObj.GetComponent<BoxCollider>().size = bounds.size;

                    // BoxColliderの中心を元のGameObjectの中心に設定
                    childObj.GetComponent<BoxCollider>().center = bounds.center;
                }
                //ブロックを独立させる
                childObj.transform.parent = null;
              

                //重力を受けるようにする
                childObj.AddComponent<Rigidbody>().useGravity = true;
                childObj.GetComponent<Rigidbody>().isKinematic = false;

                //Dropのスクリプトを付ける
                childObj.AddComponent<Drop>();

                rb.AddForce((transform.position - transform.GetChild(0).transform.position).normalized * 5.0f, ForceMode.Impulse);

                n_NowHP--;
            }
           
        }
    }
}
