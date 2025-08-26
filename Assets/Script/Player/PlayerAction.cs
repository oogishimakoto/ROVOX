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

    private bool ShotFlg = true;    //������΂����Ԃ�

    float horizontalInput;          //��������
    float verticalInput;            //��������

    Vector3 moveDirection;          //�ړ�����

    //private bool changescene = false;
    /// <summary>
    /// �v���C���[�̃A�j���[�V�������
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

    [Header("�v���C���[�̏��")]
    [SerializeField] private State playerState = State.IDLE;
    [SerializeField] �@bool haveWeapon = true;
    [SerializeField] bool isCharge = false;

    private State NoWeaponState = State.IDLE;

    public enum Charge
    {
        charge0,
        charge1,
        charge2,
        charge3,
    }

    [Header("�`���[�W")]
    [SerializeField] private Charge charge = Charge.charge0;

    [Header("�o�ߎ���")]
    [SerializeField] private float f_ShotPowerCount;
    private float f_time;

    [field: SerializeField, Header("���݂���K�w")] public int StageHierarchical { get; set; } = 1;

    // ���͂��󂯎��PlayerInput
    // �{�^���̉������
    private bool isPressed;

    //�v���C���[�����擾
    private PlayerInfo playerinfo;

    //�V���b�g�X�s�[�h��ۑ�
    private float shotspeed =0;

    private bool chargecancel = false;

    SceneChanger sceneChanger;

    //�X�e�[�W�ɏ���Ă��邩
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
        //�`���[�g���A���p
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }


        sceneChanger = GetComponent<SceneChanger>();

        playersetinfo = GetComponent<PlayerSetInfo>();

        BulletObj = playersetinfo.BulletObj;
        camera = playersetinfo.camera;

        playerinfo = GetComponent<PlayerInfo>();

        //�J�n���Ƀ{�X�̕��������悤�ɂ���
        Vector3 vec = new Vector3(0, transform.position.y, 0);
        transform.LookAt(vec);

        camera.transform.position = (transform.position - vec) * 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //�R�A�����������t�F�C�Y�ɂȂ��Ă��邩
        if(!BulletObj.GetBulletPullState())
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            //�����ƃ_���[�W�A�j���[�V�����̎��͓��͎�t�𖳂���
            if (!playersetinfo.animator.GetAnimator().GetBool("Collect") && !playersetinfo.animator.GetAnimator().GetBool("Damage"))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");   //�G���W�����̐ݒ�
                verticalInput = Input.GetAxisRaw("Vertical");       //�G���W�����̐ݒ�

                //�����̒l���A�j���[�^�[�ɑ���(�`���[�W���p)
                playersetinfo.animator.GetAnimator().SetFloat("Walk", horizontalInput);
                if(horizontalInput == 0)
                {
                    playersetinfo.animator.GetAnimator().SetFloat("Walk", -verticalInput);
                }
            }
            else
            {
                //�ړ��ʂ𖳂���
                horizontalInput = 0;
                verticalInput = 0;
            }
            //�L�[���͂�����Ă���ꍇ��Ԃ��X�V
            if (horizontalInput != 0 || verticalInput != 0)
            {
                //�`���[�g���A���p
                if(SceneManager.GetActiveScene().name == "Tutorial")
                {
                    //���݂̃~�b�V�����m�F
                    if(tutorial.GetNowCount() == 0 && tutorial.GetTextCount() == 1) 
                    {
                        tutorial.TextCount(2);
                    }
                }

                //����A�j���[�V�������D�揇�ʂ���������̃A�j���[�V�����̏ꍇ�X�V���Ȃ�
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
                //�ҋ@�A�j���[�V�������D�揇�ʂ���������̃A�j���[�V�����̏ꍇ�X�V���Ȃ�
                if (playerState != State.THROW && playerState != State.CHARGE && playerState != State.COLLECT && playerState != State.DAMAGE)
                {
                    playerState = State.IDLE;
                }
                else
                {
                    f_time += Time.deltaTime;
                }

            }

            //�V���b�g�{�^����������Ă���ꍇ
            if (isPressed && f_ShotPowerCount <= playerinfo.chargetime3)
            {
                f_ShotPowerCount += Time.deltaTime;
            }

            //�e����
            Action();

            if (playerinfo.HP <= 0)
            {
                playerState = State.DEATH;
                playerinfo.HP = 0;

            }

            //�`���[�W���̔�_���ŗ��߂�����
            if (playerState == State.DAMAGE)
            {
                f_ShotPowerCount = 0;
                if(BulletObj.GetBulletState() != Bullet.State.NORMAL)
                     BulletObj.BulletReturn();
                playersetinfo.haveWeapon= true;
                isCharge = false;
            }

            //����̃A�j���[�V�����̎����������
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
            //�t���[�Y����
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }


    }

    void FixedUpdate()
    {
        //����̃A�j���[�V�����̎��͈ړ��𐧌�
        if(playerState != State.COLLECT && playerState != State.DAMAGE && playerState != State.THROW && playerState != State.DEATH &&
            BulletObj.GetBulletState() != Bullet.State.WARP)
        {
            //player�ړ�
            MovePlayer();
        }
        else
        {
            // �Œ肷��
            transform.position = transform.position;
        }
    }

    /// <summary>
    /// �v���C���[�̈ړ�
    /// </summary>
    private void MovePlayer()
    {
        //�����������v�Z
        moveDirection = camera.transform.forward * verticalInput + camera.transform.right * horizontalInput;
        moveDirection.y = 0;
        Rigidbody rb = GetComponent<Rigidbody>();
        float limit = 1.5f;

        // �ړ����x����`�F�b�N
        float horizontalSpeed = (float)Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
        if (horizontalSpeed > maxVelocity)
        {
            rb.velocity = new Vector3(
                rb.velocity.x / (horizontalSpeed / maxVelocity),
                rb.velocity.y,
                rb.velocity.z / (horizontalSpeed / maxVelocity)
            );
        }

        //�v���C���[�����߂����Ă���Ƃ��͕����X�s�[�h���������ɂ���
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

        //���ߎ��Ԃɂ���Ēi�K��ς���
        if (f_ShotPowerCount <= playerinfo.chargetime1)
        {
            
            //���ߎ��Ԃ�1����
            ShotFlg = false;    
            charge = Charge.charge0;
            shotspeed = 0;


        }
        else
        {
            //�`���[�W���̓v���C���[���J�����̌����ƍ��킹��
            Vector3 newforward = camera.transform.forward;
            newforward.y = 0;
            transform.forward = newforward;

            if (f_ShotPowerCount >= playerinfo.chargetime1 && f_ShotPowerCount < playerinfo.chargetime2)
            {
                //���ߎ��Ԃ�1
                ShotFlg = true;
                charge = Charge.charge1;
                shotspeed = playerinfo.shotspeed1;

            }
            else if (f_ShotPowerCount >= playerinfo.chargetime2 && f_ShotPowerCount < playerinfo.chargetime3)
            {
                //���ߎ��Ԃ�2
                ShotFlg = true;
                charge = Charge.charge2;
                shotspeed = playerinfo.shotspeed2;
            }
            else if (f_ShotPowerCount >= playerinfo.chargetime3)
            {
                //���ߎ��Ԃ�3
                ShotFlg = true;
                charge = Charge.charge3;
                shotspeed = playerinfo.shotspeed3;
                //���ߎ��Ԃ̍ő���Œ�
                f_ShotPowerCount = playerinfo.chargetime3;
            }

        }


        //���߂��L�����Z��
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
            //��������łɓ����Ă��Ȃ����̊m�F
            if (BulletObj.GetBulletState() == Bullet.State.STOP)
            {
                //���Ԃ�
                BulletObj.BulletReturn();
            }
            else if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
            {
                switch (context.phase)
                {
                    case InputActionPhase.Started:
                        {
                            // �{�^���������ꂽ���̏���
                            playerState = State.CHARGE;
                            f_time = 0;
                            f_ShotPowerCount = 0.0f;
                            isPressed = true;
                            isCharge = true;
                        }
                        break;

                    case InputActionPhase.Canceled:
                        // �{�^���������ꂽ���̏���
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
            //�v���C���[������������Ă��邩�ǂ���
            if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
            {
                if (context.phase == InputActionPhase.Canceled)
                {
                    //���ߎ��Ԃ�����Ă��邩�ǂ���
                    if (charge == Charge.charge0)
                    {
                        playerState = State.IDLE;

                    }
                    else
                    {
                        //���ߓ�����s���Ă��邩
                        if (isCharge)
                        {
                            playerState = State.THROW;
                            isCharge = false;
                            //�J�����������Ă�������ɔ�΂�
                            combatShotDir = camera.gameObject.transform.forward;
                        }
                    }
                }
            }
        }
    }

    public void ButtonCancel(InputAction.CallbackContext context)
    {
        //���߂��L�����Z��
        ShotFlg = false;
        charge = Charge.charge0;
        shotspeed = 0;
        playerState = State.IDLE;
    }

    public void Action()
    {
        //������������ԂɂȂ��Ă��邩
        if (!BulletObj.GetBulletPullState())
        {
            //
            if(chargecancel)
            {
                //���߂̃A�j���[�V�������I������^�C�~���O�𒲐�
                playersetinfo.animator.ChangeCharge();
                if(playerState!= State.CHARGE)
                    chargecancel = false;
            }

            //��������łɓ����Ă��Ȃ����̊m�F
            if (BulletObj.GetBulletState() == Bullet.State.STOP)
            {
                //���łɓ����Ă���ꍇ�茳�ɖ߂��Ă���
                if (Input.GetMouseButtonDown(0))
                {
                    BulletObj.BulletReturn();
                }
            }
            else if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)    //���킪�茳�ɂ���ꍇ
            {

                //�������Ƃ��J�E���g������
                if (Input.GetMouseButtonDown(0))
                {
                    playerState = State.CHARGE;
                    f_time = 0;
                    f_ShotPowerCount = 0.0f;
                    isCharge = true;
                }
            }

            //�����Ă���ԃJ�E���g
            if (Input.GetMouseButton(0))
            {
                f_ShotPowerCount += Time.deltaTime;
            }
            if (playerState == State.CHARGE)
            {
                //����̗���
                ChargeTime();
            }

            //����������Ă��邩�ǂ���
            if (playersetinfo.haveWeapon && BulletObj.GetBulletState() != Bullet.State.RETURN)
            {
                //�������Ƃ��e����
                if (Input.GetMouseButtonUp(0))
                {

                    //���ߎ��Ԃ�����Ă��邩�ǂ���
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
                        //���ߓ�����s���Ă��邩
                        if (isCharge)
                        {
                            playerState = State.THROW;

                            //�J�����������Ă�������ɔ�΂�
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
            //�`���[�g���A���p
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                //���݂̃~�b�V�����m�F
                if (tutorial.GetNowCount() == 1 && tutorial.GetTextCount() == 1)
                {
                    tutorial.TextCount(2);
                }
            }

            f_time += Time.deltaTime;
            //Debug.Log("�{�^�����痣��܂���");
            //�\�i�[ or �e���J�����̌����ɔ�΂�
            //��������ߒi�K�ɑΉ����̗͂Ŕ�΂�
            if (BulletObj.gameObject.activeSelf)
            {
                //�w�肳�ꂽ���Ԓx�点��
                yield return new WaitForSeconds(playerinfo.shottime);
              
                BulletObj.BulletShot(combatShotDir, shotspeed);
            }
            //����������Ă��邩�ǂ���
            playersetinfo.haveWeapon = false;
            ShotFlg = false;
            //�o�ߎ��ԏ�����
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
