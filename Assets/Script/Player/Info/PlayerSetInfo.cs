using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetInfo : MonoBehaviour
{
    [Header("�v���C���[��]")]
    public Transform orientation;   //�v���C���[��]�iCinemachine camera�p�j

    [Header("���ߍU���̒����_")]
    public Transform combatLookAt;  //���ߍU������ꏊ�iCinemachine camera�p�j

    [Header("�U��")]
    public Bullet BulletObj;
    //[SerializeField] private float f_ShotPowerMaxTime = 5.0f;

    public GameObject camera;

    [Header("�A�j���[�^�[")]
    public PlayerAnimationController animator;

    [Header("�V�[���J�ڗp")]
    public GameEvent gamevent;

    [field: SerializeField] public bool haveWeapon { get; set; } = true;
    [field: SerializeField] public bool isCharge { get; set; } = false;


    [field: SerializeField, Header("���݂���K�w")] public int StageHierarchical { get; set; } = 1;

}
