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

    [Header("プレイヤーの状態")]
    [SerializeField] private State playerState = State.IDLE;

    [Header("プレイヤーの移動の可否")]
    [SerializeField] bool moveflg = false;
    private GameObject hit;

    PlayerInfo info;

    public float RotateSpeed = 5;

    float horizontalInput;          //水平入力
    float verticalInput;            //垂直入力

    Vector3 moveDirection;          //移動方向


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
            //キーボード入力
            InputPlayer();
            RotatoPlayer();

        }

    }
    void FixedUpdate()
    {
        if(moveflg)
        {
            //player移動
            MovePlayer();
        }

    }

    /// <summary>
    /// プレイヤーの移動
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
        horizontalInput = Input.GetAxisRaw("Horizontal");   //エンジン側の設定
        verticalInput = Input.GetAxisRaw("Vertical");       //エンジン側の設定

        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerState = State.RUN;
        }
        else
        {
            playerState = State.IDLE;
        }

        //マウスで決定
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
                    if (hit　!= null)
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
