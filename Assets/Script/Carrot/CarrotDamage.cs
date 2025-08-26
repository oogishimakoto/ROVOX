using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotDamage : MonoBehaviour,IEnemyDamage
{
    [Header("分解する部位")]
    [SerializeField] GameObject BreakParts;

    [Header("コアのHPコンポーネント")]
    [SerializeField] CarrotHeal coreHp;


    [Header("ダメージテスト用")]
    public bool BreakTest;
    public int damagetest = 1;
    private int damageCount;

    [SerializeField] float rotationSpeed = 90.0f; // 回転速度 (度/秒)
    [SerializeField] float totalRotation = 60.0f; // 総回転量 (度)

    public float currentRotation = 0.0f; // 現在の回転量 (度)

    bool IsRotation;

    Bullet bullet;

    private void Reset()
    {
        coreHp = GetComponent<CarrotHeal>();
    }

    private void Start()
    {
        //自身を登録する
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
        coreHp = GetComponent<CarrotHeal>();

    }

    private void Update()
    {
        if (BreakTest)
        {
            GetComponent<IEnemyDamage>().Damage(1);

            damageCount += 1;
            if (damagetest <= damageCount)
            {
                BreakTest = false;
                damageCount = 0;
            }
        }

        if (IsRotation)
        {

            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
            currentRotation += rotationAmount;
            if (currentRotation >= totalRotation)
            {
                IsRotation = false;
            }

        }


        if (coreHp.Check())
        {

            bullet.BulletReturn();

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
                rb.AddForce((childObj.GetComponent<MeshRenderer>().bounds.center - BreakParts.transform.position).normalized * Random.Range(5.0f, 10.0f) , ForceMode.Impulse);

               
            }
            enabled = false;

        }

    }

    void IEnemyDamage.Damage(int _DamageNum)
    {
        //コアのHPを減らす
        coreHp.Decrease(_DamageNum);
        IsRotation = true;
        currentRotation = 0.0f;

        //HPが残っているかどうか
        if (!coreHp.Check())
        {
            return;
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
}
