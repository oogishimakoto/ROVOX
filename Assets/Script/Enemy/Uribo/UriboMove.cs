using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UriboMove : MonoBehaviour
{

    [Header("敵キャラクターの移動速度")]
    public float f_EnemySpeed = 2.0f;

    [Header("敵キャラクターの現在位置からの行動範囲")]
    public float f_WalkRangeMin = 0.0f;
    public float f_WalkRangeMax = 10.0f;

    [SerializeField] NavMeshAgent agent;


    [Header("敵キャラクターの攻撃のため時間")]
    public float f_Accumulate = 1;

    [Header("敵キャラクターの攻撃時の情報")]
    public float f_AttackSpeed = 4; //f_EnemySpeed　＊　何倍か
    public float f_RushDistance = 20;

    //敵キャラクターの徘徊用のランダム変数
    private float f_RandX;
    private float f_RandY;
    private float f_RandZ;
    private Vector3 vec;

    //敵キャラクターの所持ブロック数
    private int n_MaxHP;
    private int n_NowHP;

    private float f_rotaion = -0.5f;

    private float nowTime = 0;

    public void SetNowtime(float time) { nowTime = time; }

    public enum EnemyState
    {
        Idle,   //待機状態
        Walk,   //徘徊状態
        Chase,  //プレイヤーを追いかける状態
        AttackAccumulate,   //攻撃のため時間
        Attack, //攻撃
    }

    [SerializeField] private EnemyState state = EnemyState.Idle;

    //現在のステータスをゲットセット
    public EnemyState GetEnemyState() { return state; }
    public void SetEnemyState(EnemyState _state) { state = _state; }

    //現在のHPのゲットセット
    public void SetEnemyHP(int _newHP) { n_NowHP = _newHP; }
    public int GetEnemyHP() { return n_NowHP; }

    private bool BodyHitFlg = true; //体のあたり判定用

    // Start is called before the first frame update
    void Start()
    {
        //AIようにNavMeshAgentを取得
        agent = GetComponent<NavMeshAgent>();

        for(int i = 0;i < transform.childCount;i++)
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
        //ベイクなどの設定が終わっているかの確認　+ 体力が最大の40%以上あるか
       if(agent.pathStatus != NavMeshPathStatus.PathInvalid && n_NowHP > n_MaxHP - 100)
       {
            switch (state)
            {
                case EnemyState.Idle:
                    float f_RandWalk = Random.Range(f_WalkRangeMin, 100);

                    if (f_RandWalk < 95)
                    {
                        f_RandX = Random.Range(transform.position.x + f_WalkRangeMin, transform.position.x + f_WalkRangeMax);
                        f_RandY = Random.Range(f_WalkRangeMin, f_WalkRangeMax);
                        f_RandZ = Random.Range(transform.position.z + f_WalkRangeMin, transform.position.z+ f_WalkRangeMax);

                        vec.x = f_RandX;
                        vec.y = f_RandY;
                        vec.z = f_RandZ;


                        state = EnemyState.Walk;
                    }

                    break;
                case EnemyState.Walk:

                    //目的を設定
                    agent.destination = vec;
                    agent.speed = f_EnemySpeed;

                    if (agent.remainingDistance < 0.1f)
                    {
                       // Debug.Log("到着しました");
                        state = EnemyState.Idle;
                    }


                    break;

                case EnemyState.Chase:
                    //プレイヤーを取得
                    GameObject player = GameObject.Find("Player");

                    if (player != null)
                    {
                        gameObject.transform.GetChild(2).gameObject.SetActive(true);

                        //プレイヤーへの方向を取得
                        Vector3 vector = player.transform.position - transform.position;

                        //transform.position += EnemySpeed * vector.normalized * Time.deltaTime;

                        agent.destination = player.transform.position;
                        agent.speed = f_EnemySpeed;
                    }
                    else
                    {
                        Debug.Log("プレイヤーが取得できませんでした");
                    }

                    break;

                case EnemyState.AttackAccumulate:
                    {
                        agent.destination = transform.position;
                        GameObject playerchar = GameObject.Find("Player");
                        Vector3 playervector = playerchar.transform.position;
                        playervector.y = 0; //yは考慮しない
                        transform.LookAt(Vector3.Lerp(transform.forward + transform.position, playervector, 0.02f));
                        //ため時間経過後に攻撃をする
                        if (Time.time - nowTime > f_Accumulate)
                        {
                            //プレイヤーの方向に突進させる
                            agent.destination = transform.position + transform.forward.normalized * f_RushDistance;
                            agent.speed = f_AttackSpeed;　//スピードを早くする
                            state = EnemyState.Attack;
                        }
                    }

                    break;

                case EnemyState.Attack:
                    {
                        //突進が終わったらIdle状態に戻す
                        if (agent.remainingDistance < 0.1f)
                        {
                            state = EnemyState.Idle;
                        }

                    }

                    break;
            }
       }
       else
       {
            //
            if(BodyHitFlg)
            {
                List<BoxCollider> col = GetComponent<BodyCollider>().GetBodyCollider();
                for(int i = 0 ; i < col.Count; i++)
                {
                    col[i].enabled = false;
                }
                BodyHitFlg = false;
            }
           
            f_rotaion -= 0.5f;
            transform.Rotate(0, 0, f_rotaion);
            if(f_rotaion <= -90)
            {
                //体力が40%を切ったときに死亡させる

                GameObject player = GameObject.Find("Player");
                player.gameObject.GetComponent<PlayerMove>().SetKillenemyCount();
                Destroy(gameObject);
            }
 

       }
      
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other != null && other.name == "Player" && state != EnemyState.Attack && state != EnemyState.AttackAccumulate)
        {

            state = EnemyState.Chase; 
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.name == "Player" && state != EnemyState.Attack && state != EnemyState.AttackAccumulate)
        {
            state = EnemyState.Idle;

        }
    }
}
