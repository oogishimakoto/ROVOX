using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CoreSonar : MonoBehaviour
{
    [Header("停止時間")]
    [SerializeField] private float HitStopTime = 2.0f;
    private float f_Count;

    [SerializeField] private float f_ShotTime = 15.0f;
    private float f_MoveSpeed;
    private float f_ReturnSpeed = 10.0f; //戻ってくるときの早さ
    [SerializeField] private float f_moveSpeedMAX = 30.0f;
    [SerializeField] private float f_moveSpeedMIN = 10.0f;

    [SerializeField] private GameObject PlayerHandObj;

    private ParentConstraint constraint;

    private Vector3 moveVec;

    public enum State
    {
        NORMAL,
        SHOT,
        STOP,
        RETURN,
    }
    [SerializeField] private State state = State.NORMAL;

    public State GetSonarState() { return state; }

    private bool Shotflg = true;

    public bool GetSonarShotFlg() { return Shotflg; }

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
                Shotflg = true;
                break;

            case State.SHOT:
                transform.position += moveVec * f_MoveSpeed * Time.deltaTime;
                Shotflg = false;
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
                Shotflg = false;


                break;


            case State.RETURN:
                //transform.position = PlayerHandObj.transform.position;
                //Debug.Log(PlayerHandObj.transform.position);
                Shotflg = false;
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

                        //速度が一定以下になったら止める
                        if (f_MoveSpeed <= 0.6f)
                        {
                            f_MoveSpeed = 0.0f;
                            state = State.STOP;
                            //当たったオブジェクトに追従するようにする
                            this.transform.parent = other.transform;
                            // Debug.Log("子オブジェクト " + other.transform.parent.name);

                            f_Count = 0.0f;

                        }
                    }
                    else if (other.tag == "Wall")
                    {

                        f_MoveSpeed = 0.0f;
                        state = State.STOP;

                        f_Count = 0.0f;

                    }

                    break;
                case State.STOP:
                    if (HitStopTime <= f_Count)
                    {

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
}
