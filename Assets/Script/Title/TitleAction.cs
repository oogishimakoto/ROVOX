using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using static PlayerAction;

public class TitleAction : MonoBehaviour
{
    private PlayerSetInfo playersetinfo;

    [SerializeField] GameObject camera;

    public enum State
    {
        IDLE,
        RUN,
    }

    [Header("�v���C���[�̏��")]
    [SerializeField] private State playerState = State.IDLE;

    [Header("�v���C���[�̈ړ��̉�")]
    [SerializeField] bool moveflg = false;
    private GameObject hit;

    PlayerInfo info;

    public float RotateSpeed = 5;

    float horizontalInput;          //��������
    float verticalInput;            //��������

    Vector3 moveDirection;          //�ړ�����


    public State GetPlayerState() { return playerState; }
    public void SetPlayerState(State newstate)
    {
        playerState = newstate;

    }

    // Start is called before the first frame update
    void Start()
    {
        playersetinfo = GetComponent<PlayerSetInfo>();
        info = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveflg)
        {
            //�L�[�{�[�h����
            InputPlayer();
            RotatoPlayer();

        }

    }
    void FixedUpdate()
    {
        if(moveflg)
        {
            //player�ړ�
            MovePlayer();
        }

    }

    /// <summary>
    /// �v���C���[�̈ړ�
    /// </summary>
    private void MovePlayer()
    {
        moveDirection = camera.transform.forward * verticalInput + camera.transform.right * horizontalInput;
        moveDirection.y = 0;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(moveDirection.normalized * info.moveSpeed * 10.0f, ForceMode.Force);
    }

    private void RotatoPlayer()
    {
        Vector3 direction = new Vector3(horizontalInput,0, verticalInput);

        float magnitude = direction.magnitude;

        if (Mathf.Approximately(magnitude, 0f) == false)
        {
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(from, to, RotateSpeed * Time.deltaTime);
        }
    }

    private void InputPlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");   //�G���W�����̐ݒ�
        verticalInput = Input.GetAxisRaw("Vertical");       //�G���W�����̐ݒ�

        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerState = State.RUN;
        }
        else
        {
            playerState = State.IDLE;
        }

        //�}�E�X�Ō���
        if (Input.GetMouseButtonDown(0))
        {
            if (hit != null)
            {
                if (hit.tag == "Signboard")
                    hit.GetComponent<SceneChanger>().SceneChage();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Signboard") 
        {
            hit = collision.gameObject;

        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        hit= null;
    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        
        switch (context.phase)
        {
            case InputActionPhase.Started:
                {
                    if (hit�@!= null)
                    {
                        if(hit.tag == "Signboard")
                             hit.GetComponent<SceneChanger>().SceneChage();
                    }
                }
                break;

            case InputActionPhase.Canceled:
                break;
        }
    }
}
