using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamgeState : StateMachineBehaviour
{
     //State�ɓ��������Ɉ�x������������܂�
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.ResetTrigger("IsAttack");
        animator.SetBool("IsCharge", false);
        animator.SetBool("IsAttackAfterIsAttackAfter", false);

        animator.gameObject.GetComponent<Boss1>().ChangeState_Damage(true);
        animator.gameObject.GetComponent<Boss1>().ResetAttackNotice();
    }


     //state���I���A�ق���state�Ɉړ�����Ƃ���񂾂��Ă΂�܂��B
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.gameObject.GetComponent<Boss1>().ChangeState_Damage(false);
    //}
}
