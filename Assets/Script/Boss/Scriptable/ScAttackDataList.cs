using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackDataList", menuName = "ScriptableObjects/EnemyAttackDataList")]
public class AttackDataList : ScriptableObject
{
    [Header("“GƒLƒƒƒ‰ƒNƒ^[‚ÌUŒ‚‚Ìî•ñ(‡”ÔŠÖŒW‚ ‚è)")]
    public AttackData[] data;
    [Header("UŒ‚‚ğ‚·‚é‚Æ‚«‚ÌğŒ(ŠK‘w‚Å‚Ç‚Ìƒpƒ^[ƒ“‚Ì”š‚ª‚Å‚é‚©")]
    public AttackProcess[] Process;
}

[Serializable]
public class AttackData 
{
    [field:Header("UŒ‚‚Ìƒpƒ‰ƒ[ƒ^[")]
    [field: SerializeField, Tooltip("UŒ‚”»’è‚ÌƒŠƒXƒg‚Ì–¼‘Oi“¯‚¶‚É‚·‚é•K—v‚ ‚èj")] public string C_AttackName { get; private set; } = "none";
    [field: SerializeField, Tooltip("UŒ‚‘O‚Ìd’¼ŠÔ")] public float f_BeforeTime { get; private set; } = 5;
    [field: SerializeField, Tooltip("UŒ‚‘O‚Ìd’¼ŠÔ")] public float f_ChargeTime { get; private set; } = 5;
    [field: SerializeField, Tooltip("UŒ‚Œã‚Ìd’¼ŠÔ")] public float f_AfterTime { get ; private set; } = 5;
    [field: SerializeField, Tooltip("UŒ‚‚Ì—^‚¦‚éƒ_ƒ[ƒW")] public int N_Damege { get; private set; } = 1;

    [field: Header("UŒ‚‚Ì‹““®")]
    [field: SerializeField, Tooltip("UŒ‚‘O‚É’Ç]‚·‚é‚©")] public bool B_isFollow { get; private set; } = false;
    [field: SerializeField, Tooltip("UŒ‚—\‚ğ•\¦‚·‚é‚©")] public bool B_isAnnounce { get; private set; } = true;

}

[Serializable]
public class AttackProcess
{

    [field: SerializeField, Tooltip("UŒ‚ƒpƒ^[ƒ“‚Ì”š")] public int[] paternNum { get; private set; }
}


