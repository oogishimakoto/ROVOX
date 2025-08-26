using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    public string name;
    public List<Collider> collider;
}

public class AttackCollider : MonoBehaviour
{
   

    [SerializeField] public List<Attack> attackCol;

  

    public List<Collider> GetAttackCollider(string colName)
    {
        List<Collider> attack = new List<Collider>(); ;

        for (int i = 0; i < attackCol.Count; ++i)
        {
            if(attackCol[i].name == colName)
            {
                return attackCol[i].collider;
                //attack.Add(attackCol[i].collider);
            }
        }
            return attack;
    }
}
