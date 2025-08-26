using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.XR;
using static Unity.VisualScripting.Member;

public class Boss1 : MonoBehaviour
{
    //変数宣言
    //================================================================================================

    //ステート
    public enum EnemyState
    {
        Idle,   //待機状態
        Rotate, //回転
        Attack,   //攻撃
        Pull,   //引っ張り合い
        Dmage,  //ダメージ
        Dead,  //ダメージ

    }
    public enum AttackState
    {
        OccurrenceTime,    //攻撃発生前時間
        ChageTime,    //攻撃前硬直時間
          stiffness,         //硬直
    }
    [Header("敵キャラクターのステート")]
    //ステート変数
    [SerializeField, ReadOnly] EnemyState state_Normal = EnemyState.Idle;
    [SerializeField, ReadOnly] AttackState state_Attack;
    [Space(20)]


    [Header("回転速度")]
    [SerializeField] float rotationSpeed;


    AttackDataList dataList;
    [Header("弾オブジェクト")]
    [SerializeField] GameObject bullet;
    [Header("ミサイルオブジェクト")]
    [SerializeField] GameObject[] missileObj;
    [Header("タケノコオブジェクト")]
    [SerializeField] GameObject TakenokoObj;
    [Header("ロケットオブジェクト")]
    [SerializeField] RoketReaction roketObj;
    [Header("コーンオブジェクト")]
    [SerializeField] GameObject[] cornObj;
    [Header("コーンの弾オブジェクト")]
    [SerializeField] GameObject cornBulletObj;
    [Header("コーンのターゲット")]
    [SerializeField] GameObject cornTargetObj;
    [Header("ビームを発射するポジション")]
    [SerializeField] Transform BeamPosition;
    [Header("コーンを一度に発射する数")]
    [SerializeField] int cornShotNum = 8;

    [Space(20)]

    //上半身の回転関数
    //BodyRotation bodyRotation;
    //攻撃パターン決定取得用コンポーネント
    IActionSelect actionPaternComp;
    //プレイヤーオブジェクト保存変数
    private GameObject PlayerObj = null;
    private GameObject PlayerHeadObj = null;
    [Space(20)]


    //攻撃予告用オブジェクト
    [SerializeField] GameObject AttackAnnounceObj;
    [SerializeField] GameObject AttackAnnounceCol;


    //カウンター
    float attackCount;          //攻撃時間カウント用
    float pullCount;            //ひっぱり時間カウント用
    //プレイヤーポジション保存用
    Vector3 playerPosBuf;
    //アニメーター
    Animator anim;


    [Header("ミサイル発射SE")]
    [SerializeField] AudioClip SE;
    AudioSource source;


    [Header("ダメージ状態の時間")]
    [SerializeField] float DamageStateTime = 3.0f;
    float DamageStateCount = 0.0f;

    [Header("攻撃アニメーション再生速度")]
    [SerializeField] float AnimSpeed = 1.0f;



    bool isWait;
    float WaitTime =3.0f;
    float WaitCount = 6.0f;

    void Start()
    {
        actionPaternComp = GetComponent<IActionSelect>();
        //プレイヤーを取得
        PlayerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj();
        //プレイヤーの頭の位置を取得
        PlayerHeadObj = transform.root.GetComponent<Enemy_PlayerManager>().GetHeadObj();


        anim = GetComponent<Animator>();
        anim.SetFloat("AnimSpeed", AnimSpeed);
        source = transform.AddComponent<AudioSource>();
        GameObject.Find("SEManager").GetComponent<SEManager>().SetAudioSource(source);

        dataList = transform.root.transform.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }

    // Update is called once per frame
    void Update()
    {



        switch (state_Normal)
        {
            //______________________________________________________________________
            //行動決定初期ステート
            case EnemyState.Idle:

                if ((actionPaternComp.AttackPatternNum == 0 || actionPaternComp.AttackPatternNum == 3) && WaitCount < WaitTime)
                {
                    WaitCount += Time.deltaTime;
                }
                else
                {
                    //前回とプレイヤーのいる階層が違うならキャラクターをジャンプさせる

                    if (anim.GetInteger("PlayerHierarchy") != PlayerObj.GetComponent<PlayerAction>().StageHierarchical)
                    {
                        ChangeState(EnemyState.Rotate);
                        //プレイヤーの階層情報を更新
                        anim.SetInteger("PlayerHierarchy", PlayerObj.GetComponent<PlayerAction>().StageHierarchical);

                    }
                    //同じなら攻撃パターン決定
                    else
                    {

                        //行動決定してステート変更
                        actionPaternComp.ActionSelect();
                        ChangeState(EnemyState.Attack);
                        //攻撃パターン情報をセット
                        anim.SetInteger("AttackPatern", actionPaternComp.AttackPatternNum);
                        WaitCount = 0.0f;
                    }
                }
                break;

            //_________________________________________________________________________
            //攻撃ステート
            case EnemyState.Attack:
                {
                    if (!anim.GetBool("IsAttacking"))
                    {
                        attackCount += Time.deltaTime;
                    }
                    switch (state_Attack)
                    {
                        //_________________________________________________________________________
                        //予備動作
                        case AttackState.OccurrenceTime:
                            //追従するかどうか
                            if (dataList.data[actionPaternComp.AttackPatternNum].B_isFollow)
                            {

                                //bodyRotation.IsRotation(true);
                                //回転
                                //キャラクターを回転
                                // ターゲットへの方向を計算
                                Vector3 direction = PlayerObj.transform.position - transform.position;

                                // directionのy成分を0にすることで、XZ平面での方向のみを考慮
                                direction.y = 0;

                                // 目標の回転を計算
                                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

                                // 現在の回転と目標の回転を線形補間でスムーズに回転させる
                                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                            }
                            //_________________________________________________________________________
                            //予備動作時間経過後に攻撃をする
                            if (attackCount > dataList.data[actionPaternComp.AttackPatternNum].f_BeforeTime)
                            {
                                ChangeState_Attack(AttackState.ChageTime);
                            }
                            break;
                        //_________________________________________________________________________
                        //予備動作
                        case AttackState.ChageTime:
                            //攻撃前硬直時間経過後に攻撃をする
                            if (attackCount > dataList.data[actionPaternComp.AttackPatternNum].f_ChargeTime)
                            {
                                ChangeState_Attack(AttackState.stiffness);
                            }
                            break;
                        //_________________________________________________________________________
                     
                        //_________________________________________________________________________
                        //硬直
                        case AttackState.stiffness:
                            //硬直時間経過後にIdle状態にする
                            if (attackCount > dataList.data[actionPaternComp.AttackPatternNum].f_AfterTime)
                            {
                                ChangeState(EnemyState.Idle);
                                anim.SetBool("IsAttackAfter", false);

                            }
                            break;

                    }

                }

                break;
            //_________________________________________________________________________
            //引っ張り合い
            case EnemyState.Pull:
                pullCount += Time.deltaTime;
                if (pullCount >= 5.0f)
                {
                    //銛を外す


                    //ステートをもとに戻す
                    ChangeState(EnemyState.Idle);
                }
                break;

            case EnemyState.Rotate:
                {
                    //_________________________________________________________________________
                    //回転
                    //キャラクターを回転
                    // ターゲットへの方向を計算
                    Vector3 direction = PlayerObj.transform.position - transform.position;

                    // directionのy成分を0にすることで、XZ平面での方向のみを考慮
                    direction.y = 0;

                    // 目標の回転を計算
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

                    // 現在の回転と目標の回転を線形補間でスムーズに回転させる
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    anim.SetBool("IsJump", false);
                    ChangeState(EnemyState.Idle);



                 
                }
                break;
            case EnemyState.Dmage:
                {
                    DamageStateCount += Time.deltaTime;
                    if (DamageStateCount > DamageStateTime)
                    {
                        state_Normal = EnemyState.Idle;
                        anim.ResetTrigger("IsAttack");
                        anim.SetBool("IsCharge", false);
                        anim.SetBool("IsAttackAfter", false);

                    }
                    break;
                }
        }
    }







    //================================================================================================
    //関数
    //************************************************************************************************



    //ステート変更関数
    void ChangeState(EnemyState nextState)
    {
        state_Normal = nextState;
        switch (nextState)
        {
            case EnemyState.Idle:
                //bodyRotation.IsRotation(false);

                break;

            case EnemyState.Attack:
                ChangeState_Attack(AttackState.OccurrenceTime);

                break;
            case EnemyState.Pull:
                pullCount = 0.0f;
                break;

            case EnemyState.Rotate:
                anim.SetBool("IsJump", true);
                break;
        }
    }

    //攻撃用ステート変更
    public void ChangeState_Attack(AttackState nextState)
    {
        state_Attack = nextState;
        attackCount = 0.0f;
        switch (nextState)
        {
            //攻撃前
            case AttackState.OccurrenceTime:
                //攻撃予告を表示
                if (dataList.data[actionPaternComp.AttackPatternNum].B_isAnnounce)
                {
                    //高さをプレイヤーに合わせる
                    Vector3 posBuf = AttackAnnounceObj.transform.position;
                    posBuf.y = PlayerObj.transform.position.y;
                    AttackAnnounceObj.transform.position = posBuf;
                    AttackAnnounceCol.transform.position = posBuf;

                    //攻撃予告を表示する　
                    AttackAnnounceObj.gameObject.SetActive(true);
                    AttackAnnounceObj.GetComponent<AttackNotice>().Init();
                }
                
                //アニメーションをさせる
                anim.SetTrigger("IsAttack");
                anim.SetBool("IsCharge", true);
                break;
            //攻撃前硬直
            case AttackState.ChageTime:
                anim.SetBool("IsAttackAfter", true);

                if (actionPaternComp.AttackPatternNum == 4 || actionPaternComp.AttackPatternNum == 5)
                {
                    playerPosBuf = PlayerHeadObj.transform.position;
                }
                break;

            //攻撃後
            case AttackState.stiffness:
                anim.SetBool("IsCharge", false);



                //攻撃予告を消す
                AttackAnnounceObj.gameObject.SetActive(false);


                if (actionPaternComp.AttackPatternNum == 5)
                {
                    // Vector3 direction = transform.forward;
                    // プレイヤーの方向に向けて弾を生成
                    Vector3 direction = (playerPosBuf - roketObj.gameObject.transform.parent.transform.position);
                    direction.y = 0;
                    direction.Normalize();
                    direction *= 21;
                    if (PlayerObj.GetComponent<PlayerAction>().StageHierarchical == 2)
                    {
                        direction.y = -9.0f;
                    }
                    else if (PlayerObj.GetComponent<PlayerAction>().StageHierarchical == 1)
                    {
                        direction.y = -18.0f;
                    }
                    else
                    {
                        direction.y = -29.0f;
                    }
                    roketObj.ShotBullet(direction.normalized);
                }
                //コーンの発射
                if (actionPaternComp.AttackPatternNum == 6)
                {
                    int cornShotNum = 4;
                    for (int j = 0; j < cornShotNum; j++)
                    {
                        int sidenum = UnityEngine.Random.Range(0, 2);
                        int cornNum = UnityEngine.Random.Range(0, cornObj[sidenum].transform.childCount);

              
                        var targetcomp = cornTargetObj.GetComponent<SatelliteTargetPos>();
                        for (int i = 0; i < cornTargetObj.transform.childCount; ++i)
                        {
                            if (targetcomp.GetUseFlg(i) == false)
                            {
                                Satellite cornComp = cornObj[sidenum].transform.GetChild(cornNum).AddComponent<Satellite>();

                                targetcomp.SetUseFlg(i);
                                cornComp.SetFollowTarget(cornTargetObj.transform.GetChild(i).transform);
                                cornComp.SetBulletObj(cornBulletObj);
                                cornComp.SetTargetNum(i);
                                cornComp.AddComponent<BillBoard>();
                                if (UnityEngine.Random.Range(0, 2) == 0)
                                {
                                    cornComp.SetBulletType(satelliteType.NORMAL);
                                }
                                else
                                {
                                    cornComp.SetBulletType(satelliteType.PREDICTION);

                                }
                                break;
                            }
                        }
                    }
                }
                else if (actionPaternComp.AttackPatternNum == 3)
                {
                    //ミサイルを発射する
                    for (int i = 0; i < missileObj.Length; ++i)
                    {
                        float randomPower = UnityEngine.Random.Range(0, 50);
                        randomPower *= 0.1f;
                        float randomRotation = UnityEngine.Random.Range(0, 100);
                        randomRotation *= 0.001f;

                        missileObj[i].GetComponent<Missile>().ShotBullet(randomPower, randomRotation);


                    }
                    source.clip = SE;
                    source.loop = false;
                    source.PlayOneShot(source.clip);

                }
                else if (actionPaternComp.AttackPatternNum == 2)
                {
                    TakenokoObj.GetComponent<Attack_Takenoko>().OnTakenokoAttack();
                }
                else if (actionPaternComp.AttackPatternNum == 1)
                {
                    GetComponent<Attack_Slap>().AttackOn();
                }

                  
              
                break;


        }
    }

    public void CreateBeam()
    {
        // プレイヤーの方向に向けて弾を生成
        Vector3 direction = (playerPosBuf - BeamPosition.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Instantiate(bullet, BeamPosition.position, lookRotation);

    }


    //攻撃判定オン
    public void AttackColliderOn()
    {
        //攻撃用のコライダーを取得し、コライダーをオンにする
        List<Collider> attackCol = GetComponent<AttackCollider>().GetAttackCollider(dataList.data[actionPaternComp.AttackPatternNum].C_AttackName);
        for (int i = 0; i < attackCol.Count; ++i)
        {
            attackCol[i].enabled = true;
        }
    }

    //攻撃判定オフ
    public void AttackColliderOff()
    {
        //攻撃用のコライダーを取得し、コライダーをオフにする
        List<Collider> attackCol = GetComponent<AttackCollider>().GetAttackCollider(dataList.data[actionPaternComp.AttackPatternNum].C_AttackName);
        for (int i = 0; i < attackCol.Count; ++i)
        {
            attackCol[i].enabled = false;
        }
    }


    //ダメージ状態変更用関数
    public void ChangeState_Damage(bool isDamage)
    {
        if ((isDamage))
        {

            state_Normal = EnemyState.Dmage;
            DamageStateCount = 0.0f;
        }
       
        
        
    }
    public void ChangeState_Dead()
    {
        //GetComponent<BossMaterialChange>().Replace();
        state_Normal = EnemyState.Dead;

    }
    public void ChangeState_Idle()
    {
        state_Normal = EnemyState.Idle;
    }
    public void ResetAttackNotice()
    {
        AttackAnnounceObj.SetActive(false);
    }

    public void AttackNoticeDisplay(bool isPlay)
    {
        AttackAnnounceObj.SetActive(isPlay);
    }
}


