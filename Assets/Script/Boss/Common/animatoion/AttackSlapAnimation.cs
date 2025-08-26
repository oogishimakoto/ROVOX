using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlapAnimation : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetInteger("AttackLoopCount",animator.GetInteger("AttackLoopCount") +1);
        if (animator.GetInteger("AttackLoopTime") == 1)
        {
            animator.SetBool("IsLoop", false);
        }
    }

}
