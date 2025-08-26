using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackDataList", menuName = "ScriptableObjects/EnemyAttackDataList")]
public class AttackDataList : ScriptableObject
{
    [Header("�G�L�����N�^�[�̍U�����̏��(���Ԋ֌W����)")]
    public AttackData[] data;
    [Header("�U��������Ƃ��̏���(�K�w�łǂ̃p�^�[���̐������ł邩")]
    public AttackProcess[] Process;
}

[Serializable]
public class AttackData 
{
    [field:Header("�U���̃p�����[�^�[")]
    [field: SerializeField, Tooltip("�U������̃��X�g�̖��O�i�����ɂ���K�v����j")] public string C_AttackName { get; private set; } = "none";
    [field: SerializeField, Tooltip("�U���O�̍d������")] public float f_BeforeTime { get; private set; } = 5;
    [field: SerializeField, Tooltip("�U���O�̍d������")] public float f_ChargeTime { get; private set; } = 5;
    [field: SerializeField, Tooltip("�U����̍d������")] public float f_AfterTime { get ; private set; } = 5;
    [field: SerializeField, Tooltip("�U���̗^����_���[�W")] public int N_Damege { get; private set; } = 1;

    [field: Header("�U�����̋���")]
    [field: SerializeField, Tooltip("�U���O�ɒǏ]���邩")] public bool B_isFollow { get; private set; } = false;
    [field: SerializeField, Tooltip("�U���\����\�����邩")] public bool B_isAnnounce { get; private set; } = true;

}

[Serializable]
public class AttackProcess
{

    [field: SerializeField, Tooltip("�U���p�^�[���̐���")] public int[] paternNum { get; private set; }
}


