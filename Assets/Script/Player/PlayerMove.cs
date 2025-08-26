using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] GameObject CameraObj;
    [SerializeField] Bullet BulletObj;
    //[SerializeField] private float f_MoveSpeed = 3.0f; // 回転速度

    private bool ShotFlg = true;

    [SerializeField] private float f_ShotPowerMaxTime = 5.0f;
    [SerializeField] private float f_ShotPowerCount;

    public int i_ItemCount = 0;

    //前回のPosition
    private Vector3 latestPos; 

    //private Rigidbody _rb = default;

    //Vector3 movingDirecion;
    //public float speedMagnification; //調整必要　例10
    public Rigidbody rb;
    //public Vector3 movingVelocity;


    private float x = 0f;
    private float z = 0f;


    //倒したエネミーの数を保存しておく
    private int KillEnemyCount = 0;

    public void SetKillenemyCount() { KillEnemyCount++; }

    public int GetKillenemyCount() { return KillEnemyCount; }

    /// <追加コード～～～移動>
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;        //摩擦力

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;     //空中摩擦力
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]        //地面判定（ジャンプ用）
    public float playerHeight;      //プレイヤー高さ
    public LayerMask whatIsGround;  //地面レイヤー
    bool grounded;

    public Transform orientation;   //プレイヤー回転（Cinemachine camera用）
    public Transform combatLookAt;  //溜め攻撃見る場所（Cinemachine camera用）


    float horizontalInput;          //水平入力
    float verticalInput;            //垂直入力

    Vector3 moveDirection;          //移動方向

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        /// <追加コード～～～移動>
        rb.freezeRotation = true;

        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //押したときカウント初期化
        if (Input.GetKeyDown(KeyCode.Return))
        {
            f_ShotPowerCount = 0.0f;
        }
        //押している間カウント
        if (Input.GetKey(KeyCode.Return))
        {
            f_ShotPowerCount += Time.deltaTime / f_ShotPowerMaxTime;
        }
        if(f_ShotPowerCount > 1.0f) 
        { f_ShotPowerCount = 1.0f; }
        //離したとき弾発射
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (ShotFlg)
            {
                Vector3 combatShotDir = combatLookAt.position - new Vector3(orientation.position.x, combatLookAt.position.y, orientation.position.z);
                BulletObj.BulletShot(combatShotDir, f_ShotPowerCount);
                //2024/4/23 ラ：弾発射方向とカメラ合わせるようにちょっと調節した 
            }
            else
            {
                BulletObj.BulletReturn();
            }
        }


        // 2024/4/18 ラ：<追加コード～～～移動>
        //地面判定
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);//地面判定

        MyInput();

        SpeedControl();

        //摩擦力適用
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0.0f;
        }
    }



    void FixedUpdate()
    {
        // 2024/4/18 ラ：<追加コード～～～移動>
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");   //エンジン側の設定
        verticalInput = Input.GetAxisRaw("Vertical");       //エンジン側の設定

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            //ジャンプクールダウン
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    public void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //地面いる
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
        }

        //空中いる
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        //速度制限
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void Jump()
    {
        //y軸の影響を消す
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        //ジャンプ
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
