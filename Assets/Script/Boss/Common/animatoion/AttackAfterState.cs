using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAfterState : StateMachineBehaviour
{



    //state‚ªI—¹A‚Ù‚©‚Ìstate‚ÉˆÚ“®‚·‚é‚Æ‚«ˆê‰ñ‚¾‚¯ŒÄ‚Î‚ê‚Ü‚·B
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Boss1>().ChangeState_Idle();
    }
}
