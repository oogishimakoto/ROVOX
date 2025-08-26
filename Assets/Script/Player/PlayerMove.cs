using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] GameObject CameraObj;
    [SerializeField] Bullet BulletObj;
    //[SerializeField] private float f_MoveSpeed = 3.0f; // ��]���x

    private bool ShotFlg = true;

    [SerializeField] private float f_ShotPowerMaxTime = 5.0f;
    [SerializeField] private float f_ShotPowerCount;

    public int i_ItemCount = 0;

    //�O���Position
    private Vector3 latestPos; 

    //private Rigidbody _rb = default;

    //Vector3 movingDirecion;
    //public float speedMagnification; //�����K�v�@��10
    public Rigidbody rb;
    //public Vector3 movingVelocity;


    private float x = 0f;
    private float z = 0f;


    //�|�����G�l�~�[�̐���ۑ����Ă���
    private int KillEnemyCount = 0;

    public void SetKillenemyCount() { KillEnemyCount++; }

    public int GetKillenemyCount() { return KillEnemyCount; }

    /// <�ǉ��R�[�h�`�`�`�ړ�>
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;        //���C��

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;     //�󒆖��C��
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]        //�n�ʔ���i�W�����v�p�j
    public float playerHeight;      //�v���C���[����
    public LayerMask whatIsGround;  //�n�ʃ��C���[
    bool grounded;

    public Transform orientation;   //�v���C���[��]�iCinemachine camera�p�j
    public Transform combatLookAt;  //���ߍU������ꏊ�iCinemachine camera�p�j


    float horizontalInput;          //��������
    float verticalInput;            //��������

    Vector3 moveDirection;          //�ړ�����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        /// <�ǉ��R�[�h�`�`�`�ړ�>
        rb.freezeRotation = true;

        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //�������Ƃ��J�E���g������
        if (Input.GetKeyDown(KeyCode.Return))
        {
            f_ShotPowerCount = 0.0f;
        }
        //�����Ă���ԃJ�E���g
        if (Input.GetKey(KeyCode.Return))
        {
            f_ShotPowerCount += Time.deltaTime / f_ShotPowerMaxTime;
        }
        if(f_ShotPowerCount > 1.0f) 
        { f_ShotPowerCount = 1.0f; }
        //�������Ƃ��e����
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (ShotFlg)
            {
                Vector3 combatShotDir = combatLookAt.position - new Vector3(orientation.position.x, combatLookAt.position.y, orientation.position.z);
                BulletObj.BulletShot(combatShotDir, f_ShotPowerCount);
                //2024/4/23 ���F�e���˕����ƃJ�������킹��悤�ɂ�����ƒ��߂��� 
            }
            else
            {
                BulletObj.BulletReturn();
            }
        }


        // 2024/4/18 ���F<�ǉ��R�[�h�`�`�`�ړ�>
        //�n�ʔ���
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);//�n�ʔ���

        MyInput();

        SpeedControl();

        //���C�͓K�p
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
        // 2024/4/18 ���F<�ǉ��R�[�h�`�`�`�ړ�>
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");   //�G���W�����̐ݒ�
        verticalInput = Input.GetAxisRaw("Vertical");       //�G���W�����̐ݒ�

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            //�W�����v�N�[���_�E��
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    public void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //�n�ʂ���
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
        }

        //�󒆂���
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        //���x����
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void Jump()
    {
        //y���̉e��������
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        //�W�����v
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
