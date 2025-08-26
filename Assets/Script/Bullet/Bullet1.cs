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
    [Header("�u���b�N�̃h���b�v")]
    public bool b_DropPlayerVector = true;

    [Header("�v���C���[�̎�̍��W")]
    [SerializeField] private GameObject PlayerHandObj;
    [Header("��~����")]
    [SerializeField] private float HitStopTime = 2.0f;
    private float f_Count;

    [SerializeField] private float f_ShotTime = 15.0f;
    private float f_MoveSpeed;
    private float f_ReturnSpeed = 10.0f; //�߂��Ă���Ƃ��̑���


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

                //�v���C���[�܂ł̋��������ȉ��ɂȂ������~����
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

                        //���x������
                        f_MoveSpeed -= f_MoveSpeed * 6.0f * Time.deltaTime;

                        //���������I�u�W�F�N�g�̐e�I�u�W�F�N�g�����擾
                        HitObjectName = other.transform.root.gameObject.name;
                        //���x�����ȉ��ɂȂ�����~�߂�
                        if (f_MoveSpeed <= 0.6f)
                        {
                            f_MoveSpeed = 0.0f;
                            state = State.STOP;
                            //���������I�u�W�F�N�g�ɒǏ]����悤�ɂ���
                            this.transform.parent = other.transform;
                            Debug.Log("�q�I�u�W�F�N�g " + other.transform.parent.name);

                            f_Count = 0.0f;

                        }
                    }
                    else if (other.tag == "Wall")
                    {
                     
                        f_MoveSpeed = 0.0f;
                        state = State.STOP;
                        //���������I�u�W�F�N�g�ɒǏ]����悤�ɂ���
                        this.transform.parent = other.transform;
                        Debug.Log("�q�I�u�W�F�N�g " + other.transform.parent.name);

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
                        this.transform.parent = null;       //�Ǐ]�I��
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



    //�u���b�N�𕪉�����֐�
    void BlockDisassembly(Collider hitObj)
    {
        //���������I�u�W�F�̂����蔻��S�Ď擾
        List<BoxCollider> bodyCol =  this.transform.parent.parent.GetComponent<BodyCollider>().GetBodyCollider();

        //�擾���������蔻��̂��Ă��镔�ʂ̃u���b�N�ɑ΂��ĂP�P�������邩�̃`�F�b�N
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
                //��������I�u�W�F�N�g���󂩂�v���C���[���ɂ��邩
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

                    //MeshRenderer�ɒu������
                    childObj.AddComponent<MeshRenderer>();
                    var mesh = childObj.GetComponent<MeshRenderer>();
                    mesh.material = skin.material;
                    childObj.AddComponent<MeshFilter>();
                    var meshfil = childObj.GetComponent<MeshFilter>();
                    meshfil.mesh = skin.sharedMesh;
                    Destroy(skin);
                }

                //�R���C�_�[���Ȃ��Ȃ�t����
                if (!childObj.GetComponent<BoxCollider>())
                {
                    childObj.AddComponent<BoxCollider>();

                    MeshFilter filter = childObj.GetComponent<MeshFilter>();
                    if (filter != null)
                    {
                        // ����GameObject�̃T�C�Y���擾
                        Bounds bounds = filter.sharedMesh.bounds;

                        // BoxCollider�̃T�C�Y��ݒ�
                        childObj.GetComponent<BoxCollider>().size = bounds.size;

                        // BoxCollider�̒��S������GameObject�̒��S�ɐݒ�
                        childObj.GetComponent<BoxCollider>().center = bounds.center;
                    }
                }

                //�u���b�N��Ɨ�������
                childObj.transform.parent = null;
                num--;

                //�d�͂��󂯂�悤�ɂ���
                childObj.AddComponent<Rigidbody>().useGravity = true;
                childObj.GetComponent<Rigidbody>().isKinematic = false;



                //Drop�̃X�N���v�g��t����
                childObj.AddComponent<Drop>();


                //���������h���b�v�̌����J�E���g
                GameObject enemy = this.transform.parent.parent.gameObject;

                if (enemy != null)
                {
                    //Debug.Log(enemy.name + "�̃u���b�N������܂���" + enemy.GetComponent<UriboMove>().GetEnemyHP());
                    enemy.GetComponent<UriboMove>().SetEnemyHP(enemy.GetComponent<UriboMove>().GetEnemyHP() - 1);
                }


                //�͂�������
                if (b_DropPlayerVector)
                {
                    //�v���C���[�̕����ɃA�C�e�����h���b�v������
                    GameObject player = GameObject.Find("Player");
                    if (player)
                    {
                        childObj.GetComponent<Rigidbody>().AddForce((player.transform.position).normalized * 5.0f, ForceMode.Impulse);

                    }
                    else
                    {
                        Debug.Log("�v���C���[��������܂���");
                    }
                }
                else
                {
                    if (skin)
                    {
                        //��̐�[���玩���̃|�W�V�����̃x�N�g���̕��ɔ�΂�
                        childObj.GetComponent<Rigidbody>().AddForce((skin.bounds.center - transform.GetChild(0).transform.position).normalized * 5.0f, ForceMode.Impulse);
                    }
                    else
                    {
                        //��̐�[���玩���̃|�W�V�����̃x�N�g���̕��ɔ�΂�
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



