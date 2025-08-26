using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Boss1_ActionSelect attackpatern;
     AttackDataList attackData;

    private void Start()
    {
        attackData = transform.root.transform.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
           
            PlayerDamage player = other.GetComponent<PlayerDamage>();
            if(player != null )
            {
                Vector3 force = other.transform.position - transform.position;
                force.Normalize();
                force *= 10.0f;
                force.y = 7.0f;
                player.Damage(attackData.data[attackpatern.AttackPatternNum].N_Damege, force);
            }
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
