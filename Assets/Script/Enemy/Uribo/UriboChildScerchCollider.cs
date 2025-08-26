using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UriboMove;

public class UriboChildScerchCollider : MonoBehaviour
{
   // public GameObject hakken;

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


        if (other != null && other.name == "Player" &&
            this.transform.parent.GetComponent<UriboMove>().GetEnemyState() != UriboMove.EnemyState.Attack &&
            this.transform.parent.GetComponent<UriboMove>().GetEnemyState() != UriboMove.EnemyState.AttackAccumulate)
        {
            this.transform.parent.GetComponent<UriboMove>().SetEnemyState(UriboMove.EnemyState.AttackAccumulate);
            this.transform.parent.GetComponent<UriboMove>().SetNowtime(Time.time);

            //state = EnemyState.Chase;

           // hakken.SetActive(true);

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.name == "Player" && 
            this.transform.parent.GetComponent<UriboMove>().GetEnemyState() != UriboMove.EnemyState.Attack &&
            this.transform.parent.GetComponent<UriboMove>().GetEnemyState() != UriboMove.EnemyState.AttackAccumulate)
        {
            //Debug.Log
            //hakken.SetActive(false);
            this.transform.parent.GetComponent<UriboMove>().SetEnemyState(UriboMove.EnemyState.Chase);

        }
    }

}
