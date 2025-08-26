using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Slap : MonoBehaviour
{
    [Header("����������")]
    [SerializeField] int loopNum = 3;
    [Header("��]���x")]
    [SerializeField] float rotationSpeed = 0.5f;
    bool isAttack = false;
    GameObject PlayerObj;
    Animator anim;

    private void Start()
    {
        GetComponent<Animator>().SetInteger("AttackLoopTime", loopNum);
        PlayerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isAttack)
        {
            //��]
            //�L�����N�^�[����]
            // �^�[�Q�b�g�ւ̕������v�Z
            Vector3 direction = PlayerObj.transform.position - transform.position;

            // direction��y������0�ɂ��邱�ƂŁAXZ���ʂł̕����݂̂��l��
            direction.y = 0;

            // �ڕW�̉�]���v�Z
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // ���݂̉�]�ƖڕW�̉�]����`��ԂŃX���[�Y�ɉ�]������
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            
            isAttack = anim.GetBool("IsLoop");
            
        }
    }

    public void AttackOn()
    {
        isAttack = true;
        GetComponent<Animator>().SetBool("IsLoop", true);
        GetComponent<Animator>().SetInteger("AttackLoopCount",0) ;
    }
}
