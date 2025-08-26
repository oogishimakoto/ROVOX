using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Slap : MonoBehaviour
{
    [Header("たたきつけ回数")]
    [SerializeField] int loopNum = 3;
    [Header("回転速度")]
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
            //回転
            //キャラクターを回転
            // ターゲットへの方向を計算
            Vector3 direction = PlayerObj.transform.position - transform.position;

            // directionのy成分を0にすることで、XZ平面での方向のみを考慮
            direction.y = 0;

            // 目標の回転を計算
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // 現在の回転と目標の回転を線形補間でスムーズに回転させる
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
