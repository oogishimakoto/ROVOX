using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Bullet1 : MonoBehaviour
{
    [Header("ブロックのドロップ")]
    public bool b_DropPlayerVector = true;

    [Header("プレイヤーの手の座標")]
    [SerializeField] private GameObject PlayerHandObj;
    [Header("停止時間")]
    [SerializeField] private float HitStopTime = 2.0f;
    private float f_Count;

    [SerializeField] private float f_ShotTime = 15.0f;
    private float f_MoveSpeed;
    private float f_ReturnSpeed = 10.0f; //戻ってくるときの早さ


    [SerializeField] private float f_moveSpeedMAX = 30.0f;
    [SerializeField] private float f_moveSpeedMIN = 10.0f;

    private ParentConstraint constraint;

    private Vector3 moveVec;

    private int DropCount = 0;
    private String HitObjectName;

    enum State
    {
        NORMAL,
        SHOT,
        STOP,
        RETURN
    }
    [SerializeField] private State state = State.NORMAL;




    // Start is called before the first frame update
    void Start()
    {
        constraint = GetComponent<ParentConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.NORMAL:
                break;

            case State.SHOT:
                transform.position += moveVec * f_MoveSpeed * Time.deltaTime;

                f_Count += Time.deltaTime;

                if (f_ShotTime <= f_Count)
                {
                    state = State.RETURN;
                    f_MoveSpeed = f_ReturnSpeed;
                    return;
                }
                break;


            case State.STOP:
                f_Count += Time.deltaTime;



                break;


            case State.RETURN:
                transform.position += (PlayerHandObj.transform.position - transform.position).normalized * f_MoveSpeed * Time.deltaTime;

                //プレイヤーまでの距離が一定以下になったら停止する
                if ((PlayerHandObj.transform.position - transform.position).magnitude < 0.5f)
                {
                    constraint.constraintActive = true;
                    state = State.NORMAL;

                }
                break;
            default:
                break;
        }
    }

    public void BulletShot(Vector3 _moveVec, float _shotPower)
    {
        state = State.SHOT;
        moveVec = _moveVec.normalized;
        constraint.constraintActive = false;

        float movespeed = f_moveSpeedMIN * (1 - _shotPower) + f_moveSpeedMAX * _shotPower;
        f_MoveSpeed = movespeed;

        f_Count = 0.0f;
    }

    public void BulletReturn()
    {
        state = State.RETURN;
        f_MoveSpeed = f_ReturnSpeed;
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
                    if (other.tag == "Char")
                    {

                        //速度を減衰
                        f_MoveSpeed -= f_MoveSpeed * 6.0f * Time.deltaTime;

                        //当たったオブジェクトの親オブジェクト名を取得
                        HitObjectName = other.transform.root.gameObject.name;
                        //速度が一定以下になったら止める
                        if (f_MoveSpeed <= 0.6f)
                        {
                            f_MoveSpeed = 0.0f;
                            state = State.STOP;
                            //当たったオブジェクトに追従するようにする
                            this.transform.parent = other.transform;
                            Debug.Log("子オブジェクト " + other.transform.parent.name);

                            f_Count = 0.0f;

                        }
                    }
                    else if (other.tag == "Wall")
                    {
                     
                        f_MoveSpeed = 0.0f;
                        state = State.STOP;
                        //当たったオブジェクトに追従するようにする
                        this.transform.parent = other.transform;
                        Debug.Log("子オブジェクト " + other.transform.parent.name);

                        f_Count = 0.0f;

                    }
                    break;
                case State.STOP:
                    if (HitStopTime <= f_Count)
                    {
                     
                        if (this.transform.parent.tag == "Char")
                        {
                            BlockDisassembly(other);
                        }

                        state = State.RETURN;
                        this.transform.parent = null;       //追従終了
                        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

                        f_MoveSpeed = f_ReturnSpeed;
                        return;

                    }
                    break;
                case State.RETURN:


                    break;
            }

        }

    }



    //ブロックを分解する関数
    void BlockDisassembly(Collider hitObj)
    {
        //当たったオブジェのあたり判定全て取得
        List<BoxCollider> bodyCol =  this.transform.parent.parent.GetComponent<BodyCollider>().GetBodyCollider();

        //取得したあたり判定のついている部位のブロックに対して１つ１つ分解するかのチェック
        for (int i = 0; i < bodyCol.Count; i++)
        {
            for (int num = 0; num < bodyCol[i].transform.childCount; num++)
            {
                Transform childObj = bodyCol[i].transform.GetChild(num);
           

                var skin = childObj.GetComponent<SkinnedMeshRenderer>();
                if (childObj.name == "mori")
                {
                    continue;

                }
                //分解するオブジェクトが銛からプレイヤー側にあるか
                Vector3 vecPlayerBullet = PlayerHandObj.transform.position - transform.GetChild(0).transform.position;
                Vector3 vecObjBullet;
                if (skin)
                {
                    vecObjBullet = skin.bounds.center - transform.GetChild(0).transform.position;
                }
                else
                {
                    vecObjBullet = childObj.GetComponent<MeshRenderer>().bounds.center - transform.GetChild(0).transform.position;

                }
                vecPlayerBullet = vecPlayerBullet.normalized;
                vecObjBullet = vecObjBullet.normalized;

                if (Vector3.Dot(vecPlayerBullet, vecObjBullet) < 0.0f)
                {

                    continue;
                }

                if (skin)
                {

                    //MeshRendererに置き換え
                    childObj.AddComponent<MeshRenderer>();
                    var mesh = childObj.GetComponent<MeshRenderer>();
                    mesh.material = skin.material;
                    childObj.AddComponent<MeshFilter>();
                    var meshfil = childObj.GetComponent<MeshFilter>();
                    meshfil.mesh = skin.sharedMesh;
                    Destroy(skin);
                }

                //コライダーがないなら付ける
                if (!childObj.GetComponent<BoxCollider>())
                {
                    childObj.AddComponent<BoxCollider>();

                    MeshFilter filter = childObj.GetComponent<MeshFilter>();
                    if (filter != null)
                    {
                        // 元のGameObjectのサイズを取得
                        Bounds bounds = filter.sharedMesh.bounds;

                        // BoxColliderのサイズを設定
                        childObj.GetComponent<BoxCollider>().size = bounds.size;

                        // BoxColliderの中心を元のGameObjectの中心に設定
                        childObj.GetComponent<BoxCollider>().center = bounds.center;
                    }
                }

                //ブロックを独立させる
                childObj.transform.parent = null;
                num--;

                //重力を受けるようにする
                childObj.AddComponent<Rigidbody>().useGravity = true;
                childObj.GetComponent<Rigidbody>().isKinematic = false;



                //Dropのスクリプトを付ける
                childObj.AddComponent<Drop>();


                //分解したドロップの個数をカウント
                GameObject enemy = this.transform.parent.parent.gameObject;

                if (enemy != null)
                {
                    //Debug.Log(enemy.name + "のブロックが減りました" + enemy.GetComponent<UriboMove>().GetEnemyHP());
                    enemy.GetComponent<UriboMove>().SetEnemyHP(enemy.GetComponent<UriboMove>().GetEnemyHP() - 1);
                }


                //力を加える
                if (b_DropPlayerVector)
                {
                    //プレイヤーの方向にアイテムをドロップさせる
                    GameObject player = GameObject.Find("Player");
                    if (player)
                    {
                        childObj.GetComponent<Rigidbody>().AddForce((player.transform.position).normalized * 5.0f, ForceMode.Impulse);

                    }
                    else
                    {
                        Debug.Log("プレイヤーが見つかりません");
                    }
                }
                else
                {
                    if (skin)
                    {
                        //銛の先端から自分のポジションのベクトルの方に飛ばす
                        childObj.GetComponent<Rigidbody>().AddForce((skin.bounds.center - transform.GetChild(0).transform.position).normalized * 5.0f, ForceMode.Impulse);
                    }
                    else
                    {
                        //銛の先端から自分のポジションのベクトルの方に飛ばす
                        childObj.GetComponent<Rigidbody>().AddForce((childObj.GetComponent<MeshRenderer>().bounds.center - transform.GetChild(0).transform.position).normalized * 5.0f, ForceMode.Impulse);

                    }
                }

            }

        }


        var enemyHP = hitObj.GetComponent<EnemyHPManager>();
        if (enemyHP)
        {
            enemyHP.CheckHP();
        }

    }
}



