using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float f_EnemySpeed = 2.0f;

    public float f_WalkRangeMin = 0.0f;
    public float f_WalkRangeMax = 10.0f;

    [SerializeField]  NavMeshAgent agent;

    private float f_RandX;
    private float f_RandY;
    private float f_RandZ;
    private Vector3 vec;

    enum EnemyState
    {
        Idle,   //待機状態
        Walk,   //徘徊状態
        Chase,  //プレイヤーを追いかける状態
        Attack, //攻撃
    }

    [SerializeField] private EnemyState state = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        { 
        case EnemyState.Idle:
                float f_RandWalk = Random.Range(f_WalkRangeMin, 100);

                if (f_RandWalk < 95)
                {
                    f_RandX = Random.Range(f_WalkRangeMin, f_WalkRangeMax);
                    f_RandY = Random.Range(f_WalkRangeMin, f_WalkRangeMax);
                    f_RandZ = Random.Range(f_WalkRangeMin, f_WalkRangeMax);

                    vec.x = f_RandX;
                    vec.y = f_RandY;
                    vec.z = f_RandZ;


                    state = EnemyState.Walk;
                }

                break; 
        case EnemyState.Walk:

              
                agent.destination = vec;
                agent.speed = f_EnemySpeed;

                if(agent.remainingDistance < 0.1f)
                {
                    //Debug.Log("到着しました");
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
                    Vector3 vector= player.transform.position - transform.position;

                    //transform.position += EnemySpeed * vector.normalized * Time.deltaTime;

                    agent.destination = player.transform.position;
                    agent.speed = f_EnemySpeed;
                }
                else
                {
                    Debug.Log("プレイヤーが取得できませんでした");
                }

                break;

        case EnemyState.Attack:

                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other != null && other.name == "Player" )
        {
            //Debug.Log(other.tag);   
            state= EnemyState.Chase;
        }

    }
}
