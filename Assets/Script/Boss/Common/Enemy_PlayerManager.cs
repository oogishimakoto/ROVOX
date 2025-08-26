using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PlayerManager : MonoBehaviour
{
    [Header ("�v���C���[�I�u�W�F�N�g")]
    [SerializeField] GameObject PlayerObj;

    [Header("�v���C���[�̓��I�u�W�F�N�g")]
    [SerializeField] GameObject HeadObj;

    [Header("�G�L�����N�^�[�̍U�����̏��")]
    [SerializeField] AttackDataList dataList;


    public GameObject GetPlayerObj()
    {
        return PlayerObj;
    }

    public GameObject GetHeadObj()
    {
        return HeadObj;
    }

    public AttackDataList GetAttackData()
    {
        return dataList;
    }
}
