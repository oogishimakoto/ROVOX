using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAfterState : StateMachineBehaviour
{



    //state���I���A�ق���state�Ɉړ�����Ƃ���񂾂��Ă΂�܂��B
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Boss1>().ChangeState_Idle();
    }
}
