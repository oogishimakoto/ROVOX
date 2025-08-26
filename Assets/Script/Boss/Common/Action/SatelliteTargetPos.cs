using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteTargetPos : MonoBehaviour
{
    [SerializeField] float[] HierarchyHeight;

    int HierarchyBuf = 0;
    PlayerAction playerHierarchy;

    public bool[] targetFlg;
    
    // Start is called before the first frame update
    void Start()
    {
        targetFlg = new bool[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 posBuf = child.position;

             child.position = posBuf;
        }

        playerHierarchy =transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HierarchyBuf != playerHierarchy.StageHierarchical)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Vector3 posBuf = child.position;

                posBuf.y += HierarchyHeight[playerHierarchy.StageHierarchical] - HierarchyHeight[HierarchyBuf];
                child.position = posBuf;
            }
            HierarchyBuf = playerHierarchy.StageHierarchical;

        }
    }

    public bool GetUseFlg(int _idx)
    {
        return targetFlg[_idx];
    }
    public void SetUseFlg(int _idx)
    {
       targetFlg[_idx] = true;
    }
    public void ReleaseUseFlg(int _idx)
    {
        targetFlg[_idx] = false;
    }
}
