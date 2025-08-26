using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetInfo : MonoBehaviour
{
    [Header("プレイヤー回転")]
    public Transform orientation;   //プレイヤー回転（Cinemachine camera用）

    [Header("溜め攻撃の注視点")]
    public Transform combatLookAt;  //溜め攻撃見る場所（Cinemachine camera用）

    [Header("攻撃")]
    public Bullet BulletObj;
    //[SerializeField] private float f_ShotPowerMaxTime = 5.0f;

    public GameObject camera;

    [Header("アニメーター")]
    public PlayerAnimationController animator;

    [Header("シーン遷移用")]
    public GameEvent gamevent;

    [field: SerializeField] public bool haveWeapon { get; set; } = true;
    [field: SerializeField] public bool isCharge { get; set; } = false;


    [field: SerializeField, Header("現在いる階層")] public int StageHierarchical { get; set; } = 1;

}
