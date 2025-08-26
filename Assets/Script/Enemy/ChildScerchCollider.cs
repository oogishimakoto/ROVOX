using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlimeMove;

public class ChildScerchCollider : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {


        if (other != null && other.name == "Player")
        {
            this.transform.parent.GetComponent<SlimeMove>().SetEnemyState(EnemyState.Attack);

            //state = EnemyState.Chase;



        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.name == "Player")
        {
            this.transform.parent.GetComponent<SlimeMove>().SetEnemyState(EnemyState.Chase);

        }
    }
}
