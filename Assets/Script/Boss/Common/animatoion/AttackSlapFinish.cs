using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlapFinish : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.GetComponent<Boss1>().AttackNoticeDisplay(true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("AttackLoopCount") >= animator.GetInteger("AttackLoopTime") -1)
        {
            animator.SetBool("IsLoop", false);
            animator.gameObject.transform.GetComponent<Boss1>().AttackNoticeDisplay(false); ;

        }
    }
}
