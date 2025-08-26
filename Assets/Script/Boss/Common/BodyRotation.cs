using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g�ۑ��ϐ�
    private GameObject PlayerObj = null;
 
    [Header("��]���x")]
    [SerializeField] float rotationSpeed;
  
    [Header("�㔼�g�̃W���C���g")]
    [SerializeField] Transform bodyJoint;
     AttackDataList dataList;

    [Header("�v���C���[�̊K�w�擾�p"), SerializeField] PlayerAction hierarchyInfo;

    //�U���p�^�[������擾�p�R���|�[�l���g
    IActionSelect actionPaternComp;
    public bool isRotate  = false;
    //�㔼�g��]���ۑ��p�ϐ�
    Quaternion bodyRotation;

    void Start()
    {
        //�v���C���[���擾
        PlayerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj();
        actionPaternComp = GetComponent<IActionSelect>();
        dataList = transform.root.transform.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }

    private void LateUpdate()
    {
        //�^�[�Q�b�g�̕��ɑ̂���]������
        if (isRotate)
        {
            //�L�����N�^�[����]
            // �^�[�Q�b�g�ւ̕������v�Z
            Vector3 direction = PlayerObj.transform.position - transform.position;
            // direction��y������0�ɂ��邱�ƂŁAXZ���ʂł̕����݂̂��l��
            direction.y = 0;
            Quaternion targetRotation;
            // �ڕW�̉�]���v�Z
            if (direction.x >= 0)
            {
                targetRotation = Quaternion.LookRotation(direction, -Vector3.forward);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(direction, Vector3.forward);
            }

            //�U���p�^�[�������]�̃I�t�Z�b�g���擾
            float rotationOffset = 1.0f;
            //�U���𓖂Ă邽�߂ɉ�]��␳
            Quaternion adjustment = Quaternion.Euler(rotationOffset, 0, 0);
            targetRotation *= adjustment;

            // ���݂̉�]�ƖڕW�̉�]����`��ԂŃX���[�Y�ɉ�]������
            bodyRotation = Quaternion.Lerp(bodyRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        else if(isRotate == false)
        { 
            //�������Ȃ�
        }
   

        bodyJoint.transform.rotation = bodyRotation;
    }

    public void IsRotation(bool _isRotate)
    {
        isRotate = _isRotate;
    }
}
