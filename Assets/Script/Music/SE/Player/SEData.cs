using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SEManager", menuName = "ScriptableObjects/EnemyAttackDataList")]
public class SEData : ScriptableObject
{
    [Header("�v���C���[��SE�܂Ƃ�")]
    public PlayerSE[] data;


}

[Serializable]
public class PlayerSE
{
    [field: SerializeField, Tooltip("�U���O�̍d������")] public List<AudioClip> audioClips= new List<AudioClip>();

}
