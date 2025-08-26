using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;


//必要なコンポーネントを定義
[RequireComponent(typeof(CoreHP))]

public class EnemySmallCoreDamage : MonoBehaviour, IEnemyDamage
{
    [Header("分解する部位")]
    [SerializeField] GameObject BreakParts;
    [Header("分解する部位に関係するコアのシールドの当たり判定オブジェクト")]
    [SerializeField] GameObject LinkSheld;
    [Header("コアのシールドを破壊するか")]
    [SerializeField] bool isBreakSheld;
    [Header("コアのHPコンポーネント")]
    [SerializeField] CoreHP coreHp;
  

    [Header("ダメージテスト用")]
    public bool BreakTest;
    public int  damagetest = 1;
    private int damageCount;

    [Header("ボスアニメーション")]
    [SerializeField] Animator anim;

    [Header("コア破壊SE")]
    [SerializeField] AudioClip SE;
    AudioSource source;

    [SerializeField] float rotationSpeed = 90.0f; // 回転速度 (度/秒)
    [SerializeField] float totalRotation = 60.0f; // 総回転量 (度)

    public float currentRotation = 0.0f; // 現在の回転量 (度)

    bool IsRotation;

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
        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }

        //自身を登録する
        transform.root.GetComponent<CoreManager>().AddCoreNum();

        source = transform.AddComponent<AudioSource>();
        GameObject.Find("SEManager").GetComponent<SEManager>().SetAudioSource(source);

        bullet = GameObject.Find("mori").GetComponent<Bullet>();
    }

    private void Update()
    {
        createBlockCount += Time.deltaTime;

        //デバッグ用ダメージテスト
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

        //ねじの回転
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
    }

    void IEnemyDamage.Damage(int _DamageNum)
    {
        //コアのHPを減らす
        coreHp.HPDecrease(_DamageNum);
        IsRotation = true;
        currentRotation = 0.0f;


        //ダメージを受けたときにねじからブロックを発生
        if (createBlockCount >= createBlockTime)
        {

               for (int i = 0; i < createBlockNum; ++i)
            {
                Vector3 force;
                force.x = Random.Range(-5, 5);
                force.z = Random.Range(-5, 5);
                force.y = Random.Range(0, 20);
                GameObject obj = Instantiate(blockPrehub, transform);
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
            if (tutorial.GetNowCount() == 2 && tutorial.GetTextCount() == 2)
            {
                if (tutorial.GetTextCount() == 2 && tutorial.GetNowCount() == 2)
                    tutorial.Count(1);

                tutorial.TextCount(1);
            }

            //現在のミッション確認
            if (tutorial.GetNowCount() == 4 && tutorial.GetTextCount() == 2)
            {
                if (tutorial.GetTextCount() == 2 && tutorial.GetNowCount() == 4)
                    tutorial.Count(1);

                tutorial.TextCount(1);
            }
        }

        bullet.BulletReturn();

        //これより下はHPがなくなったときの処理
        if (transform.parent != null)
        {
            //コア数を１つ減らす
            transform.root.GetComponent<CoreManager>().SubCoreNum();

            Rigidbody myRb = transform.AddComponent<Rigidbody>();
            myRb.useGravity = true;
            myRb.isKinematic = false;
            transform.parent = null;
            transform.tag = "Untagged";
            //GetComponent<Collider>().enabled = false;
            if (!BreakParts)
            {
                return;
            }
            //取得したあたり判定のついている部位のブロックに対して１つ１つ分解するかのチェック
            for (int i = 0; i < BreakParts.transform.childCount; i++)
            {

                Transform childObj = BreakParts.transform.GetChild(i);

                var skin = childObj.GetComponent<SkinnedMeshRenderer>();

                //銛は判定せずに抜ける
                if (childObj.name == "mori")
                {
                    childObj.GetComponent<Bullet>().BulletReturn();
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

            //ダメージアニメーションをセット
            anim.SetBool("IsDamage", true);

            source.clip = SE;
            source.loop = false;
            source.PlayOneShot(source.clip);

            //リンクしている当たり判定があるなら削除する
            if (isBreakSheld)
            {
                Destroy(LinkSheld);
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
}
