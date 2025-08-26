using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTouchSearch : MonoBehaviour
{
    private Roket roketComp;

    private void Start()
    {
        roketComp = transform.parent.GetComponent<Roket>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            roketComp.SetGroundTouchFlg(true, collision.GetComponent<StageHierarchy>().GetHierarchyNum());
        }

    }


    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            roketComp.SetGroundTouchFlg(false, -1);
            
        }

    }


}
