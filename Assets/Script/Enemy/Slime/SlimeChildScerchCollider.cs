using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlimeMove;

public class SlimeChildScerchCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {


        if (other != null && other.name == "Player" &&
            this.transform.parent.GetComponent<SlimeMove>().GetEnemyState() != SlimeMove.EnemyState.Attack &&
            this.transform.parent.GetComponent<SlimeMove>().GetEnemyState() != SlimeMove.EnemyState.AttackAccumulate)
        {
            this.transform.parent.GetComponent<SlimeMove>().SetEnemyState(SlimeMove.EnemyState.AttackAccumulate);
            this.transform.parent.GetComponent<SlimeMove>().SetNowtime(Time.time);
            
            //state = EnemyState.Chase;



        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.name == "Player" &&
            this.transform.parent.GetComponent<SlimeMove>().GetEnemyState() != SlimeMove.EnemyState.Attack &&
            this.transform.parent.GetComponent<SlimeMove>().GetEnemyState() != SlimeMove.EnemyState.AttackAccumulate)
        {
            this.transform.parent.GetComponent<SlimeMove>().SetEnemyState(SlimeMove.EnemyState.Chase);

        }
    }
}
