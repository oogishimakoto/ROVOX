using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : StateMachineBehaviour
{
     //State�ɓ��������Ɉ�x������������܂�
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        animator.gameObject.GetComponent<BossMaterialChange>().Replace();

        animator.gameObject.GetComponent<Boss1>().ChangeState_Dead();
    }

}
