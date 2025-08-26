using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : StateMachineBehaviour
{
     //State‚É“ü‚Á‚½Žž‚Éˆê“x‚¾‚¯ŽÀŒø‚³‚ê‚Ü‚·
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        animator.gameObject.GetComponent<BossMaterialChange>().Replace();

        animator.gameObject.GetComponent<Boss1>().ChangeState_Dead();
    }

}
