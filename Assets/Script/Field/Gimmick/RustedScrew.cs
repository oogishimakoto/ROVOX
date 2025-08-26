using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

public class RustedScrew : MonoBehaviour, IEnemyDamage
{
    [SerializeField] int MAXHP = 20;
    [SerializeField] int HP = 20;

    private float damageVec;  //1HP当たりの移動ベクトル

    Bullet bullet;


    [SerializeField] GameObject blockPrehub;
    [SerializeField] float createBlockTime = 1;
    [SerializeField] int createBlockNum = 30;
    float createBlockCount;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
     
        transform.tag = "Core";
    }

    private void Update()
    {
        createBlockCount += Time.deltaTime;

        if (Check())
        {

            bullet.BulletReturn();

            transform.tag = "Warp";
            this.enabled = false;

         
            Transform BreakParts = transform.GetChild(0);

            //取得したあたり判定のついている部位のブロックに対して１つ１つ分解するかのチェック
            for (int i = 0; i < BreakParts.transform.childCount; i++)
            {

                Transform childObj = BreakParts.transform.GetChild(i);

                var skin = childObj.GetComponent<SkinnedMeshRenderer>();

                //銛は判定せずに抜ける
                if (childObj.name == "mori")
                {
                    continue;
                }



                if (skin)
                {
                    //MeshRendererに置き換え
                    var mesh = childObj.AddComponent<MeshRenderer>();
                    mesh.material = skin.material;
                    childObj.AddComponent<MeshFilter>();
                    var meshfil = childObj.GetComponent<MeshFilter>();
                    meshfil.mesh = skin.sharedMesh;
                    Destroy(skin);
                }

                //ブロックを独立させる
                childObj.transform.parent = null;
                //ブロックを削除するコルーチン呼び出し
                StartCoroutine(BlockDestroy(childObj.gameObject));
                i--;

                //重力を受けるようにする
                Rigidbody rb = childObj.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;



                //銛の先端から自分のポジションのベクトルの方に飛ばす
                rb.AddForce((childObj.GetComponent<MeshRenderer>().bounds.center - BreakParts.transform.position).normalized * 40.0f, ForceMode.Impulse);
            }

        }
    
  
    }

    //一定時間後にブロックオブジェクトを削除する関数
    private IEnumerator BlockDestroy(GameObject _block)
    {
        //ｎ秒待つ
        yield return new WaitForSeconds(4f);
        //削除
        Destroy(_block);

    }

    void IEnemyDamage.Damage(int _DamageNum)
    {
        HP -= _DamageNum;
        //damageVec.y = 0;
        this.transform.position -= transform.forward * damageVec * _DamageNum;


        if (createBlockCount >= createBlockTime )
        {


            for (int i = 0; i < createBlockNum; ++i)
            {
                Vector3 force;
                force.x = Random.Range(-5, 5);
                force.z = Random.Range(-5, 5);
                force.y = Random.Range(0, 20);
                GameObject obj = Instantiate(blockPrehub, transform);
                obj.transform.localScale *= 1.5f;
                obj.transform.parent = null;
                obj.transform.AddComponent<Rigidbody>().useGravity = true;
                obj.transform.GetComponent<Rigidbody>().AddForce(force * 2.0f, ForceMode.Impulse);


                //ブロックを削除するコルーチン呼び出し
                StartCoroutine(BlockDestroy(obj.gameObject));

            }
            createBlockCount = 0;
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
