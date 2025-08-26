using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g�ۑ��ϐ�
    private GameObject PlayerObj = null;

    [Header("��]���x")]
    [SerializeField] float rotationSpeed;

    [Header("���̃W���C���g")]
    [SerializeField] Transform headJoint;

    //�㔼�g��]���ۑ��p�ϐ�
    Quaternion headRotation;

    private void Start()
    {
        //�v���C���[���擾
        PlayerObj = GameObject.Find("Player");
    }

    void Update()
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


        // ���݂̉�]�ƖڕW�̉�]����`��ԂŃX���[�Y�ɉ�]������
        headRotation = Quaternion.Lerp(headRotation, targetRotation, Time.deltaTime * rotationSpeed);

        headJoint.transform.rotation = headRotation;

    }
}
