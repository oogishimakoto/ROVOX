using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamgeState : StateMachineBehaviour
{
     //Stateに入った時に一度だけ実効されます
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.ResetTrigger("IsAttack");
        animator.SetBool("IsCharge", false);
        animator.SetBool("IsAttackAfterIsAttackAfter", false);

        animator.gameObject.GetComponent<Boss1>().ChangeState_Damage(true);
        animator.gameObject.GetComponent<Boss1>().ResetAttackNotice();
    }


     //stateが終了、ほかのstateに移動するとき一回だけ呼ばれます。
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.gameObject.GetComponent<Boss1>().ChangeState_Damage(false);
    //}
}
