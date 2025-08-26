using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultScrew : MonoBehaviour
{
    StageDataManager manager;
    StageClearDataList.StageClearData stagedatalist;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        stagedatalist = manager.GetStageData();
        for (int i = 0; i < stagedatalist.ScrewBreakCount; i++)
        {
            UnityEngine.Color color = transform.GetChild(i).gameObject.GetComponent<Image>().color;
            color.a = 1;
            transform.GetChild(i).GetComponent<Image>().color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
