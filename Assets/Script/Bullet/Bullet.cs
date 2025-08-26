using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;


public class Bullet : MonoBehaviour
{
    [Header("�u���b�N�̃h���b�v")]
    public bool b_DropPlayerVector = true;

    [Header("�v���C���[�̎�̍��W")]
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private GameObject PlayerHandObj;

    [Header("�v���C���[�̎�̍��W")]
    [SerializeField] private GameObject PlayerShotObj;

    [Header("��~����")]
    [SerializeField] private float HitStopTime = 2.0f;
    private float f_Count;

    private float f_MoveSpeed;
    private float f_ReturnSpeed = 10.0f; //�߂��Ă���Ƃ��̑���

    [SerializeField] private float f_moveSpeedMAX = 30.0f;
    [SerializeField] private float f_moveSpeedMIN = 10.0f;

    private ParentConstraint constraint;

    private Vector3 moveVec;

    private int DropCount = 0;
    private String HitObjectName;

    private PlayerSEManager playerse;

    Vector3 ReturnLock;

    CorePull corepull;

    [SerializeField] GameObject blockPrehub;
    [SerializeField] int createBlockNum = 30;
    float createBlockCount;

    public enum State
    {
        NORMAL,
        SHOT,
        STOP,
        RETURN,
        WARP,
        PULL,
        HEALPULL
    }
    [SerializeField] private State state = State.NORMAL;

    [SerializeField] private int  PullTime = 10;

    TutorialTextManager tutorial;

    private bool Shotflg = true;

    public void ResetCount()
    {
        f_Count= 0;
    }

    public State GetBulletState() { return state; }
    public void SetBulletState(State _state) { state = _state; }

    public bool GetBulletPullState()  
    { 
        if (state == State.PULL) 
        {
            return true;
        }

        if (state == State.HEALPULL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool GetBulletShotFlg() { return Shotflg; }
    //public void SetBulletShotFlg(bool flg ) { Shotflg = flg; }

    // Start is called before the first frame update
    void Start()
    {
        //�`���[�g���A���p
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }


        constraint = GetComponent<ParentConstraint>();
        corepull = GetComponent<CorePull>();
        //�v���C���[��SE���擾
        if (PlayerObj != null)
        {
            for (int i = 0; i < PlayerObj.transform.childCount; i++)
            {
                if (PlayerObj.transform.GetChild(i).name == "SE")
                {
                    playerse = PlayerObj.transform.GetChild(i).GetComponent<PlayerSEManager>();
                }
            }
        }
        transform.position = PlayerHandObj.transform.position;
        //�߂��Ă���Ƃ��̃X�s�[�h���擾
        f_ReturnSpeed = PlayerObj.GetComponent<PlayerInfo>().returnspeed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.NORMAL:
                Shotflg = true;
                if(PlayerObj.GetComponent<PlayerAction>().GetPlayerState() == PlayerAction.State.DAMAGE)
                {
                    state = State.RETURN;
                    PlayerObj.GetComponent<PlayerAction>().SetHaveWeapon(true);
                }

                break;

            case State.SHOT:
                GetComponent<BoxCollider>().enabled = true;
                Shotflg = false;
                 transform.position +=(moveVec * f_MoveSpeed * Time.deltaTime);
                //this.GetComponent<Rigidbody>().AddForce(moveVec * f_MoveSpeed, ForceMode.Impulse);
                f_Count += Time.deltaTime;

                break;


            case State.STOP:
                f_Count += Time.deltaTime;
                Shotflg = false;
                ReturnLock = transform.position;

                break;


            case State.RETURN:

                GetComponent<BoxCollider>().enabled = false;
                // Debug.Log(PlayerHandObj.transform.position);
                Shotflg = false;
                transform.position += (PlayerHandObj.transform.position - transform.position).normalized * f_MoveSpeed * Time.deltaTime;
                //�v���C���[�ɂ��K�������Ė߂��Ă���悤�ɂ���
                transform.LookAt(ReturnLock);
                PlayerObj.GetComponent<PlayerAction>().SetHaveWeapon(true);
                //�v���C���[�܂ł̋��������ȉ��ɂȂ������~����
                if ((PlayerHandObj.transform.position - transform.position).magnitude < 0.5f)
                {
                    transform.position = PlayerHandObj.transform.position;
                    constraint.constraintActive = true;
                    state = State.NORMAL;

                    //�A�j���[�V������D��
                    if (PlayerObj.GetComponent<PlayerAction>().GetPlayerState() != PlayerAction.State.DAMAGE)   //�_���[�W��Ԃ�D��
                         PlayerObj.GetComponent<PlayerAction>().SetPlayerState(PlayerAction.State.COLLECT);
                    corepull.HitCoreflg = false;
                    corepull.HitCoreObject = null;
                    GetComponent<HealPull>().HitCarrotflg = false;
                    //SE�����邩
                    if (playerse.collect != null)
                    {
                        playerse.source.clip = playerse.collect;
                        playerse.source.PlayOneShot(playerse.source.clip);
                    }
                    
                }
                break;

            case State.WARP:
                Shotflg = false;


                f_Count += Time.deltaTime;

                WarpPlayer();

                break;

            case State.PULL:
                f_Count += Time.deltaTime;
                
                if(f_Count >= PullTime)
                {
                    BulletReturn();
                }
                break;

            case State.HEALPULL:
                f_Count += Time.deltaTime;

                if (f_Count >= PullTime)
                {
                    BulletReturn();
                }
                break;
            default:
                break;
        }
    }

    public void BulletShot(Vector3 _moveVec, float _shotPower)
    {
        transform.LookAt(transform.position + _moveVec);
        ReturnLock = transform.position + _moveVec;
        state = State.SHOT;
        moveVec = _moveVec.normalized;
        constraint.constraintActive = false;
        transform.position = PlayerShotObj.transform.position;
        float movespeed = f_moveSpeedMIN * (1 - _shotPower) + f_moveSpeedMAX * _shotPower;
        f_MoveSpeed = movespeed;

        f_Count = 0.0f;
    }

    public void BulletReturn()
    {
        this.transform.parent = null;       //�Ǐ]�I��
        state = State.RETURN;
        f_MoveSpeed = f_ReturnSpeed;

        //�`���[�g���A���p
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            //���݂̃~�b�V�����m�F
            if (tutorial.GetNowCount() == 1 && tutorial.GetTextCount() == 2)
            {
                if (tutorial.GetTextCount() == 2 && tutorial.GetNowCount() == 1)
                    tutorial.Count(1);

                tutorial.TextCount(1);
            }
        }
    }

    public void WarpPlayer()
    {
        Transform pos = transform.parent;
        if (transform.parent.name == "BOSSFieldRange")
        {
            Debug.Log(pos.name);

            pos = null;
        }
        else if (transform.parent.tag != "Warp")
        {
            pos = transform.parent;
        }

        if (transform.parent.name == "GameObject")
        {
            pos = transform.parent.parent;
        }

        if(pos!= null)
        {
            if (PlayerObj.GetComponent<ScrewWarp>().targetObject == null)
                PlayerObj.GetComponent<ScrewWarp>().targetObject = pos;
            // �o�ߎ��Ԃ��ݒ肵���ړ����Ԃ𒴂�����ړ����I��
            if (PlayerObj.GetComponent<ScrewWarp>().warpend)
            {

                //�`���[�g���A���p
                if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    //���݂̃~�b�V�����m�F
                    if (tutorial.GetNowCount() == 3 && tutorial.GetTextCount() == 1)
                    {
                        if (tutorial.GetTextCount() == 1 && tutorial.GetNowCount() == 3)
                            tutorial.Count(1);

                    }
                }

                BulletReturn();     //�Ǐ]�I��
                this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                PlayerObj.GetComponent<ScrewWarp>().warpend = false;
                f_MoveSpeed = f_ReturnSpeed;
                PlayerObj.GetComponent<ScrewWarp>().targetObject = null;
                PlayerObj.transform.position = pos.position;
            }

        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "PlayerHitObj")
        {
            switch (state)
            {
                case State.NORMAL:
                    break;


                case State.SHOT:
                    if(other.tag == "Wall")
                    {

                        return;
                    }
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    f_MoveSpeed = 0.0f;
                    state = State.STOP;
                    //���������I�u�W�F�N�g�ɒǏ]����悤�ɂ���
                    this.transform.parent = other.transform;
                    //Debug.Log("�q�I�u�W�F�N�g " + other.transform.parent.name);

                    f_Count = 0.0f;

                    if (other.tag == "Core")
                    {
                        //transform.position = other.transform.position;
                        f_Count = 0.0f;


                        for (int i = 0; i < createBlockNum; ++i)
                        {
                            Vector3 force;
                            force.x = UnityEngine.Random.Range(-5, 5);
                            force.z = UnityEngine.Random.Range(-5, 5);
                            force.y = UnityEngine.Random.Range(0, 20);
                            GameObject obj = Instantiate(blockPrehub, transform);
                            obj.transform.localScale *= 1.5f;
                            obj.transform.parent = null;
                            obj.transform.AddComponent<Rigidbody>().useGravity = true;
                            obj.transform.GetComponent<Rigidbody>().AddForce(force * 2.0f, ForceMode.Impulse);


                            //�u���b�N���폜����R���[�`���Ăяo��
                            StartCoroutine(BlockDestroy(obj.gameObject));

                        }
                    }

                    if (other.tag == "Warp")
                    {
                        state = State.WARP;
                        f_Count = 0.0f;
                    }

                    if (other.tag == "Heal")
                    {
                       // state = State.HEALPULL;
                        f_Count = 0.0f;
                    }
                    break;
                case State.STOP:
                    if (HitStopTime <= f_Count )
                    {
                        if (this.transform.parent != null)
                        {
                            if (this.transform.parent.tag == "Core" && other)
                            {
                                IEnemyDamage damageComp = other.GetComponent<IEnemyDamage>();


                            }

                            if (this.transform.parent.tag == "Heal" && other)
                            {
                                IEnemyDamage damageComp = other.GetComponent<IEnemyDamage>();

                            }
                        }
   

                        state = State.STOP;
                       
                        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

                        f_MoveSpeed = f_ReturnSpeed;
                        return;

                    }
                    break;
                case State.RETURN:


                    break;

                case State.WARP:
                    //���������I�u�W�F�N�g�ɒǏ]����悤�ɂ���
                    this.transform.parent = other.transform;

                    break;
            }

        }

    }


    //��莞�Ԍ�Ƀu���b�N�I�u�W�F�N�g���폜����֐�
    private IEnumerator BlockDestroy(GameObject _block)
    {
        //���b�҂�
        yield return new WaitForSeconds(4f);
        //�폜
        Destroy(_block);

    }
}



