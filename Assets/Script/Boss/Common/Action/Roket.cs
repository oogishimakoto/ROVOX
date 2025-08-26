using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Roket : MonoBehaviour
{
  
    public float bounceHeight = 2.0f;
    public float gravity = -4.9f; // �������x��x�����邽�߂̏d��
    public Vector3 rotationPoint; // ��]���̈ʒu
    public float rotationSpeed = 20.0f; // ��]���x

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
        rb.useGravity = false; // Unity �̏d�͂𖳌��ɂ���

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
            // �J�X�^���d�͂�K�p
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
        // �I�u�W�F�N�g�̈ʒu����]������̑��Έʒu�ɕϊ�
        Vector3 relativePos = transform.position - rotationPoint;

        // y�����̉�]�̂��߂̃N�H�[�^�j�I�����쐬
        Quaternion rotation = Quaternion.Euler(0, rotationSpeed * rotationDirection * Time.deltaTime, 0) ;

        // ���Έʒu����]
        relativePos = rotation * relativePos ;

        // ��]��̈ʒu���v�Z
        transform.position = rotationPoint + relativePos;

        // �I�u�W�F�N�g�̌�������]������̑��Έʒu�Ɋ�Â��čX�V
        Vector3 lookDirection = rotationPoint - transform.position;
        lookDirection.y = 0; // y�������̉�]�݂̂��l�����邽�߂ɁAy�������[���ɂ���
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
        // �v���C���[�̈ʒu���擾
        Vector3 playerPosition = playerObj.gameObject.transform.position;

        // �I�u�W�F�N�g�̈ʒu����]������̑��Έʒu�ɕϊ�
        Vector3 relativePos = transform.position - rotationPoint;

        // �v���C���[�̈ʒu����]������̑��Έʒu�ɕϊ�
        Vector3 playerRelativePos = playerPosition - rotationPoint;

        // �v���C���[�������ɂ��邩�E���ɂ��邩�𔻒�
        float direction = Vector3.Cross(relativePos, playerRelativePos).y;

        // ��]����������
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
