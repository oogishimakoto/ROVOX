using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


//必要なコンポーネントを定義
[RequireComponent(typeof(CoreHP))]
public class EnemyBigCoreDamege : MonoBehaviour, IEnemyDamage
{
    [Header("大元のキャラクターのオブジェクト")]
    [SerializeField] GameObject parentObj;

    [SerializeField] Animator anim;

    [Header("コアのHPコンポーネント")]
    [SerializeField] CoreHP coreHp;

    [Header("ダメージテスト用")]
    public bool BreakTest;
    public int damagetest = 1;
    private int damageCount;

    [Header("ゲームClearイベント")]
    [SerializeField] GameEvent clearEvenet;


    [SerializeField] GameObject blockPrehub;
    [SerializeField] float createBlockTime = 1;
    [SerializeField] int createBlockNum = 30;
    float createBlockCount;

    Bullet bullet;

    TutorialTextManager tutorial;//チュートリアル用

    private void Reset()
    {
        coreHp = GetComponent<CoreHP>();
    }

    private void Start()
    {
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }
    }

    private void Update()
    {
        createBlockCount += Time.deltaTime;

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
    }

    void IEnemyDamage.Damage(int _DamageNum)
    {
        if (transform.parent != null)
        {
            //コアのHPを減らす
            int damage = _DamageNum;
            damage *= transform.root.GetComponent<CoreManager>().GetDamageRate();
            coreHp.HPDecrease(damage);



            
            if (createBlockCount >= createBlockTime && damage > 0.0f)
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


            //HPが残っているかどうか
            if (!coreHp.CheckHP())
            {
                return;
            }

            //チュートリアル用
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                //現在のミッション確認
                if (tutorial.GetNowCount() == 5 && tutorial.GetTextCount() == 2)
                {
                    if (tutorial.GetTextCount() == 2 && tutorial.GetNowCount() == 5)
                        tutorial.Count(1);

                    tutorial.TextCount(1);
                }
            }

            bullet.BulletReturn();

            transform.root.GetComponent<CoreManager>().SetScoreCoreNum();

            Rigidbody myRb = transform.AddComponent<Rigidbody>();
            myRb.useGravity = true;
            myRb.isKinematic = false;
            transform.parent = null;
            transform.tag = "Untagged";

            //死亡アニメーションをセット
            anim.SetTrigger("IsDead");
            anim.gameObject.GetComponent<Boss1>().ChangeState_Dead();   
            StartCoroutine(ChangeResult());
        }
    }


    //一定時間後にブロックオブジェクトを削除する関数
    private IEnumerator ChangeResult ()
    {
        

        //ｎ秒待つ
        yield return new WaitForSeconds(5f);

        clearEvenet.Raise();
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
