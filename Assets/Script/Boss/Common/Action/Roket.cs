using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Roket : MonoBehaviour
{
  
    public float bounceHeight = 2.0f;
    public float gravity = -4.9f; // 落下速度を遅くするための重力
    public Vector3 rotationPoint; // 回転軸の位置
    public float rotationSpeed = 20.0f; // 回転速度

    private Rigidbody rb;
    private bool isGrounded;
    float rotationDirection;
    private PlayerAction playerObj;

    [SerializeField] private int boundTime = 5;
    int boundcount;

    [SerializeField] private float deleteTime = 25;
    [SerializeField] private float deleteCount = 0;


    void Start()
    {
        playerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<PlayerAction>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Unity の重力を無効にする

    }

    void Update()
    {
        if (isGrounded)
        {

            float jumpVelocity;

            jumpVelocity = Mathf.Sqrt(2 * bounceHeight * Mathf.Abs(gravity));

            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
        else
        {
            // カスタム重力を適用
            rb.AddForce(new Vector3(0, gravity * rb.mass, 0));
        }

        deleteCount += Time.deltaTime;
        if(deleteCount > deleteTime)
        {
            ParentConstraint constraint = transform.GetComponent<ParentConstraint>();
            if (constraint != null)
            {
                constraint.constraintActive = true;
            }
            Debug.Log("aaaaa");
            deleteCount = 0;
            boundcount = 0;
            this.enabled = false;
            this.transform.localScale = Vector3.one;
            rb.velocity = Vector3.zero;
        }

        RotateAroundPoint();
    }


    void RotateAroundPoint()
    {
        // オブジェクトの位置を回転軸からの相対位置に変換
        Vector3 relativePos = transform.position - rotationPoint;

        // y軸回りの回転のためのクォータニオンを作成
        Quaternion rotation = Quaternion.Euler(0, rotationSpeed * rotationDirection * Time.deltaTime, 0) ;

        // 相対位置を回転
        relativePos = rotation * relativePos ;

        // 回転後の位置を計算
        transform.position = rotationPoint + relativePos;

        // オブジェクトの向きを回転軸からの相対位置に基づいて更新
        Vector3 lookDirection = rotationPoint - transform.position;
        lookDirection.y = 0; // y軸方向の回転のみを考慮するために、y成分をゼロにする
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void SetGroundTouchFlg(bool _isGround, int _stageHierarchy)
    {
        isGrounded = _isGround;
        if (_isGround && _stageHierarchy == 0)
        {
            boundcount += 1;
            if (boundcount >= boundTime)
            {
                  ParentConstraint constraint = transform.GetComponent<ParentConstraint>();
                if (constraint != null)
                {
                    constraint.constraintActive = true;
                }

                deleteCount = 0;
                boundcount = 0;
                this.enabled = false;
                this.transform.localScale = Vector3.one;
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void SetMoveDirection()
    {
        // プレイヤーの位置を取得
        Vector3 playerPosition = playerObj.gameObject.transform.position;

        // オブジェクトの位置を回転軸からの相対位置に変換
        Vector3 relativePos = transform.position - rotationPoint;

        // プレイヤーの位置を回転軸からの相対位置に変換
        Vector3 playerRelativePos = playerPosition - rotationPoint;

        // プレイヤーが左側にいるか右側にいるかを判定
        float direction = Vector3.Cross(relativePos, playerRelativePos).y;

        // 回転方向を決定
        rotationDirection = direction > 0 ? 1 : -1;

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "Player")
        {
            PlayerDamage damage = other.gameObject.GetComponent<PlayerDamage>();
            if (damage != null)
            {
                Vector3 force = other.transform.position - transform.position;
                force.Normalize();
                force *= 10.0f;
                force.y = 7.0f;

                damage.Damage(1, force);
            }
        }

       

    }
    public void SetPlayerObj(GameObject _playerObj)
    {
        playerObj = _playerObj.GetComponent<PlayerAction>();

    }
}
