using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UI.Image;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    private PlayerSetInfo playersetinfo;


    Bullet BulletObj;
    //[SerializeField] private float f_ShotPowerMaxTime = 5.0f;

    GameObject camera;

    private bool ShotFlg = true;    //武器を飛ばせる状態か

    float horizontalInput;          //水平入力
    float verticalInput;            //垂直入力

    Vector3 moveDirection;          //移動方向

    //private bool changescene = false;
    /// <summary>
    /// プレイヤーのアニメーション状態
    /// </summary>
    public enum State
    {
        IDLE,
        RUN,
        CHARGE,
        THROW,
        COLLECT,
        DAMAGE,
        DEATH
    }

    [Header("プレイヤーの状態")]
    [SerializeField] private State playerState = State.IDLE;
    [SerializeField] 　bool haveWeapon = true;
    [SerializeField] bool isCharge = false;

    private State NoWeaponState = State.IDLE;

    public enum Charge
    {
        charge0,
        charge1,
        charge2,
        charge3,
    }

    [Header("チャージ")]
    [SerializeField] private Charge charge = Charge.charge0;

    [Header("経過時間")]
    [SerializeField] private float f_ShotPowerCount;
    private float f_time;

    [field: SerializeField, Header("現在いる階層")] public int StageHierarchical { get; set; } = 1;

    // 入力を受け取るPlayerInput
    // ボタンの押下状態
    private bool isPressed;

    //プレイヤー情報を取得
    private PlayerInfo playerinfo;

    //ショットスピードを保存
    private float shotspeed =0;

    private bool chargecancel = false;

    SceneChanger sceneChanger;

    //ステージに乗っているか
    bool StageRide = true;

    float maxVelocity = 8;

    TutorialTextManager tutorial;

    public void SetRide(bool set) {  StageRide = set; }
    public bool GetRide() { return StageRide; }


    public State GetPlayerState() { return playerState; }
    public void SetPlayerState(State newstate)
    { 
        playerState = newstate;
        
    }

    public bool GetHaveWeapon() { return playersetinfo.haveWeapon; }
    public void SetHaveWeapon(bool flg) { playersetinfo.haveWeapon = flg; }
    public State GetNoWeaponState() { return NoWeaponState; }

    public Charge GetChargeState() { return charge; }

    Vector3 combatShotDir;

    bool stopVelocity = false;

    // Start is called before the first frame update
    void Start()
    {
        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }


        sceneChanger = GetComponent<SceneChanger>();

        playersetinfo = GetComponent<PlayerSetInfo>();

        BulletObj = playersetinfo.BulletObj;
        camera = playersetinfo.camera;

        playerinfo = GetComponent<PlayerInfo>();

        //開始時にボスの方を向くようにする
        Vector3 vec = new Vector3(0, transform.position.y, 0);
        transform.LookAt(vec);

        camera.transform.position = (transform.position - vec) * 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //コアを引き抜くフェイズになっているか
        if(!BulletObj.GetBulletPullState())
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            //銛回収とダメージアニメーションの時は入力受付を無くす
            if (!playersetinfo.animator.GetAnimator().GetBool("Collect") && !playersetinfo.animator.GetAnimator().GetBool("Damage"))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");   //エンジン側の設定
                verticalInput = Input.GetAxisRaw("Vertical");       //エンジン側の設定

                //歩きの値をアニメーターに送る(チャージ中用)
                playersetinfo.animator.GetAnimator().SetFloat("Walk", horizontalInput);
                if(horizontalInput == 0)
                {
                    playersetinfo.animator.GetAnimator().SetFloat("Walk", -verticalInput);
                }
            }
            else
            {
                //移動量を無くす
                horizontalInput = 0;
                verticalInput = 0;
            }
            //キー入力がされている場合状態を更新
            if (horizontalInput != 0 || verticalInput != 0)
            {
                //チュートリアル用
                if(SceneManager.GetActiveScene().name == "Tutorial")
                {
                    //現在のミッション確認
                    if(tutorial.GetNowCount() == 0 && tutorial.GetTextCount() == 1) 
                    {
                        tutorial.TextCount(2);
                    }
                }

                //走るアニメーションより優先順位が高い特定のアニメーションの場合更新しない
                if (playerState != State.THROW && playerState != State.CHARGE && playerState != State.COLLECT && playerState != State.DAMAGE)
                {
                    playerState = State.RUN;
                }
                else
                {
                    f_time += Time.deltaTime;
                }

            }
            else
            {
                //待機アニメーションより優先順位が高い特定のアニメーションの場合更新しない
                if (playerState != State.THROW && playerState != State.CHARGE && playerState != State.COLLECT && playerState != State.DAMAGE)
                {
                    playerState = State.IDLE;
                }
                else
                {
                    f_time += Time.deltaTime;
                }

            }

            //ショットボタンが押されている場合
            if (isPressed && f_ShotPowerCount <= playerinfo.chargetime3)
            {
                f_ShotPowerCount += Time.deltaTime;
            }

            //弾発射
            Action();

            if (playerinfo.HP <= 0)
            {
                playerState = State.DEATH;
                playerinfo.HP = 0;

            }

            //チャージ中の被ダメで溜めを解除
            if (playerState == State.DAMAGE)
            {
                f_ShotPowerCount = 0;
                if(BulletObj.GetBulletState() != Bullet.State.NORMAL)
                     BulletObj.BulletReturn();
                playersetinfo.haveWeapon= true;
                isCharge = false;
            }

            //特定のアニメーションの時は銛を消す
            if (playerState == State.IDLE || playerState == State.RUN || playerState == State.DEATH || playerState == State.DAMAGE)
            {
                if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
                {
                    BulletObj.gameObject.SetActive(false);
                }
            }
            else
            {
                BulletObj.gameObject.SetActive(true);

            }

        }
        else
        {
            //フリーズする
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }


    }

    void FixedUpdate()
    {
        //特定のアニメーションの時は移動を制限
        if(playerState != State.COLLECT && playerState != State.DAMAGE && playerState != State.THROW && playerState != State.DEATH &&
            BulletObj.GetBulletState() != Bullet.State.WARP)
        {
            //player移動
            MovePlayer();
        }
        else
        {
            // 固定する
            transform.position = transform.position;
        }
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    private void MovePlayer()
    {
        //歩く方向を計算
        moveDirection = camera.transform.forward * verticalInput + camera.transform.right * horizontalInput;
        moveDirection.y = 0;
        Rigidbody rb = GetComponent<Rigidbody>();
        float limit = 1.5f;

        // 移動速度上限チェック
        float horizontalSpeed = (float)Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
        if (horizontalSpeed > maxVelocity)
        {
            rb.velocity = new Vector3(
                rb.velocity.x / (horizontalSpeed / maxVelocity),
                rb.velocity.y,
                rb.velocity.z / (horizontalSpeed / maxVelocity)
            );
        }

        //プレイヤーが溜めをしているときは歩くスピードをゆっくりにする
        if (playerState != State.CHARGE)
        {
            Vector3 vel = new Vector3(moveDirection.normalized.x * playerinfo.moveSpeed, rb.velocity.y, moveDirection.normalized.z * playerinfo.moveSpeed);
            rb.velocity = vel;

            //rb.AddForce(moveDirection.normalized * playerinfo.moveSpeed * 10.0f, ForceMode.Force);
        }
        else
        {
            Vector3 vel = new Vector3(moveDirection.normalized.x * playerinfo.chargeSpeed, rb.velocity.y, moveDirection.normalized.z * playerinfo.chargeSpeed);
            rb.velocity = vel;
            //rb.velocity = new Vector3(rb.velocity.x / limit, rb.velocity.y, rb.velocity.z / limit);
            //rb.AddForce(moveDirection.normalized * playerinfo.moveSpeed * 10.0f, ForceMode.Force);
        }
    }

    private void ChargeTime()
    {

        //ため時間によって段階を変える
        if (f_ShotPowerCount <= playerinfo.chargetime1)
        {
            
            //ため時間が1未満
            ShotFlg = false;    
            charge = Charge.charge0;
            shotspeed = 0;


        }
        else
        {
            //チャージ中はプレイヤーをカメラの向きと合わせる
            Vector3 newforward = camera.transform.forward;
            newforward.y = 0;
            transform.forward = newforward;

            if (f_ShotPowerCount >= playerinfo.chargetime1 && f_ShotPowerCount < playerinfo.chargetime2)
            {
                //ため時間が1
                ShotFlg = true;
                charge = Charge.charge1;
                shotspeed = playerinfo.shotspeed1;

            }
            else if (f_ShotPowerCount >= playerinfo.chargetime2 && f_ShotPowerCount < playerinfo.chargetime3)
            {
                //ため時間が2
                ShotFlg = true;
                charge = Charge.charge2;
                shotspeed = playerinfo.shotspeed2;
            }
            else if (f_ShotPowerCount >= playerinfo.chargetime3)
            {
                //ため時間が3
                ShotFlg = true;
                charge = Charge.charge3;
                shotspeed = playerinfo.shotspeed3;
                //ため時間の最大を固定
                f_ShotPowerCount = playerinfo.chargetime3;
            }

        }


        //溜めをキャンセル
        if (Input.GetMouseButtonDown(1))
        {
            ShotFlg = false;
            charge = Charge.charge0;
            shotspeed = 0;
            playerState = State.IDLE;
        }
    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        if (!BulletObj.GetBulletPullState())
        {
            //武器をすでに投げていないかの確認
            if (BulletObj.GetBulletState() == Bullet.State.STOP)
            {
                //銛を返す
                BulletObj.BulletReturn();
            }
            else if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
            {
                switch (context.phase)
                {
                    case InputActionPhase.Started:
                        {
                            // ボタンが押された時の処理
                            playerState = State.CHARGE;
                            f_time = 0;
                            f_ShotPowerCount = 0.0f;
                            isPressed = true;
                            isCharge = true;
                        }
                        break;

                    case InputActionPhase.Canceled:
                        // ボタンが離された時の処理
                        isPressed = false;
                        break;
                }
            }
        }
    }

    public void ButtonRelease(InputAction.CallbackContext context)
    {
        if (!BulletObj.GetBulletPullState())
        {
            //プレイヤーが武器を持っているかどうか
            if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
            {
                if (context.phase == InputActionPhase.Canceled)
                {
                    //ため時間が足りているかどうか
                    if (charge == Charge.charge0)
                    {
                        playerState = State.IDLE;

                    }
                    else
                    {
                        //溜め動作を行っているか
                        if (isCharge)
                        {
                            playerState = State.THROW;
                            isCharge = false;
                            //カメラが向いている方向に飛ばす
                            combatShotDir = camera.gameObject.transform.forward;
                        }
                    }
                }
            }
        }
    }

    public void ButtonCancel(InputAction.CallbackContext context)
    {
        //溜めをキャンセル
        ShotFlg = false;
        charge = Charge.charge0;
        shotspeed = 0;
        playerState = State.IDLE;
    }

    public void Action()
    {
        //引っこ抜き状態になっているか
        if (!BulletObj.GetBulletPullState())
        {
            //
            if(chargecancel)
            {
                //溜めのアニメーションを終了するタイミングを調整
                playersetinfo.animator.ChangeCharge();
                if(playerState!= State.CHARGE)
                    chargecancel = false;
            }

            //武器をすでに投げていないかの確認
            if (BulletObj.GetBulletState() == Bullet.State.STOP)
            {
                //すでに投げている場合手元に戻ってくる
                if (Input.GetMouseButtonDown(0))
                {
                    BulletObj.BulletReturn();
                }
            }
            else if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)    //武器が手元にある場合
            {

                //押したときカウント初期化
                if (Input.GetMouseButtonDown(0))
                {
                    playerState = State.CHARGE;
                    f_time = 0;
                    f_ShotPowerCount = 0.0f;
                    isCharge = true;
                }
            }

            //押している間カウント
            if (Input.GetMouseButton(0))
            {
                f_ShotPowerCount += Time.deltaTime;
            }
            if (playerState == State.CHARGE)
            {
                //武器の溜め
                ChargeTime();
            }

            //武器を持っているかどうか
            if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
            {
                //離したとき弾発射
                if (Input.GetMouseButtonUp(0))
                {

                    //ため時間が足りているかどうか
                    if (charge == Charge.charge0)
                    {
                       
                        if (horizontalInput != 0 || verticalInput != 0)
                        {
                            SetPlayerState(PlayerAction.State.RUN);
                        }
                        else
                        {
                            SetPlayerState(PlayerAction.State.IDLE);
                        }
                            
                    }
                    else
                    {
                        //溜め動作を行っているか
                        if (isCharge)
                        {
                            playerState = State.THROW;

                            //カメラが向いている方向に飛ばす
                            combatShotDir = camera.gameObject.transform.forward;

                            isCharge = false;
                        }
                    }
                }
            }
        }
    }

    public IEnumerator Shot()
    {
        if (ShotFlg)
        {
            //チュートリアル用
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                //現在のミッション確認
                if (tutorial.GetNowCount() == 1 && tutorial.GetTextCount() == 1)
                {
                    tutorial.TextCount(2);
                }
            }

            f_time += Time.deltaTime;
            //Debug.Log("ボタンから離れました");
            //ソナー or 弾をカメラの向きに飛ばす
            //武器をため段階に対応し体力で飛ばす
            if (BulletObj.gameObject.activeSelf)
            {
                //指定された時間遅らせる
                yield return new WaitForSeconds(playerinfo.shottime);
              
                BulletObj.BulletShot(combatShotDir, shotspeed);
            }
            //武器を持っているかどうか
            playersetinfo.haveWeapon = false;
            ShotFlg = false;
            //経過時間初期化
            f_ShotPowerCount = 0.0f;
        }

        charge = Charge.charge0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            stopVelocity = true;
        }
    }
}
