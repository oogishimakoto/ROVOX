using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColSwitch : StateMachineBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAttacking", true);
        animator.gameObject.GetComponent<Boss1>().AttackColliderOn();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Boss1>().AttackColliderOff();
        animator.SetBool("IsAttacking", false);

    }


}
