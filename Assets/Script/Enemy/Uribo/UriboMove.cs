using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UriboMove : MonoBehaviour
{

    [Header("�G�L�����N�^�[�̈ړ����x")]
    public float f_EnemySpeed = 2.0f;

    [Header("�G�L�����N�^�[�̌��݈ʒu����̍s���͈�")]
    public float f_WalkRangeMin = 0.0f;
    public float f_WalkRangeMax = 10.0f;

    [SerializeField] NavMeshAgent agent;


    [Header("�G�L�����N�^�[�̍U���̂��ߎ���")]
    public float f_Accumulate = 1;

    [Header("�G�L�����N�^�[�̍U�����̏��")]
    public float f_AttackSpeed = 4; //f_EnemySpeed�@���@���{��
    public float f_RushDistance = 20;

    //�G�L�����N�^�[�̜p�j�p�̃����_���ϐ�
    private float f_RandX;
    private float f_RandY;
    private float f_RandZ;
    private Vector3 vec;

    //�G�L�����N�^�[�̏����u���b�N��
    private int n_MaxHP;
    private int n_NowHP;

    private float f_rotaion = -0.5f;

    private float nowTime = 0;

    public void SetNowtime(float time) { nowTime = time; }

    public enum EnemyState
    {
        Idle,   //�ҋ@���
        Walk,   //�p�j���
        Chase,  //�v���C���[��ǂ���������
        AttackAccumulate,   //�U���̂��ߎ���
        Attack, //�U��
    }

    [SerializeField] private EnemyState state = EnemyState.Idle;

    //���݂̃X�e�[�^�X���Q�b�g�Z�b�g
    public EnemyState GetEnemyState() { return state; }
    public void SetEnemyState(EnemyState _state) { state = _state; }

    //���݂�HP�̃Q�b�g�Z�b�g
    public void SetEnemyHP(int _newHP) { n_NowHP = _newHP; }
    public int GetEnemyHP() { return n_NowHP; }

    private bool BodyHitFlg = true; //�̂̂����蔻��p

    // Start is called before the first frame update
    void Start()
    {
        //AI�悤��NavMeshAgent���擾
        agent = GetComponent<NavMeshAgent>();

        for(int i = 0;i < transform.childCount;i++)
        {
            //�u���b�N�̏����̏��������v�Z
            n_MaxHP += transform.GetChild(i).childCount;
        }

        //�����̏����������݂̏������ɍ��킹��
        n_NowHP = n_MaxHP;  

    }

    // Update is called once per frame
    void Update()
    {
        //�x�C�N�Ȃǂ̐ݒ肪�I����Ă��邩�̊m�F�@+ �̗͂��ő��40%�ȏ゠�邩
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

                    //�ړI��ݒ�
                    agent.destination = vec;
                    agent.speed = f_EnemySpeed;

                    if (agent.remainingDistance < 0.1f)
                    {
                       // Debug.Log("�������܂���");
                        state = EnemyState.Idle;
                    }


                    break;

                case EnemyState.Chase:
                    //�v���C���[���擾
                    GameObject player = GameObject.Find("Player");

                    if (player != null)
                    {
                        gameObject.transform.GetChild(2).gameObject.SetActive(true);

                        //�v���C���[�ւ̕������擾
                        Vector3 vector = player.transform.position - transform.position;

                        //transform.position += EnemySpeed * vector.normalized * Time.deltaTime;

                        agent.destination = player.transform.position;
                        agent.speed = f_EnemySpeed;
                    }
                    else
                    {
                        Debug.Log("�v���C���[���擾�ł��܂���ł���");
                    }

                    break;

                case EnemyState.AttackAccumulate:
                    {
                        agent.destination = transform.position;
                        GameObject playerchar = GameObject.Find("Player");
                        Vector3 playervector = playerchar.transform.position;
                        playervector.y = 0; //y�͍l�����Ȃ�
                        transform.LookAt(Vector3.Lerp(transform.forward + transform.position, playervector, 0.02f));
                        //���ߎ��Ԍo�ߌ�ɍU��������
                        if (Time.time - nowTime > f_Accumulate)
                        {
                            //�v���C���[�̕����ɓːi������
                            agent.destination = transform.position + transform.forward.normalized * f_RushDistance;
                            agent.speed = f_AttackSpeed;�@//�X�s�[�h�𑁂�����
                            state = EnemyState.Attack;
                        }
                    }

                    break;

                case EnemyState.Attack:
                    {
                        //�ːi���I�������Idle��Ԃɖ߂�
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
                //�̗͂�40%��؂����Ƃ��Ɏ��S������

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
