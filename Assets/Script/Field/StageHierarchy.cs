using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHierarchy : MonoBehaviour
{
    [SerializeField, Header("このオブジェクトの階層")] int HierarchyNum;
    [SerializeField]private FieldStageRotation stageRotation;

    public int GetHierarchyNum()
    {
        return HierarchyNum;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") )
        {
            collision.transform.GetComponent<PlayerAction>().StageHierarchical = HierarchyNum;
            if (stageRotation != null)
            {
                stageRotation.OnCollisionEnter(collision);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (stageRotation != null)
            {
                stageRotation.OnCollisionExit(collision);
            }
        }
    }


}
