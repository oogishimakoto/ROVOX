using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SEManager", menuName = "ScriptableObjects/EnemyAttackDataList")]
public class SEData : ScriptableObject
{
    [Header("プレイヤーのSEまとめ")]
    public PlayerSE[] data;


}

[Serializable]
public class PlayerSE
{
    [field: SerializeField, Tooltip("攻撃前の硬直時間")] public List<AudioClip> audioClips= new List<AudioClip>();

}
