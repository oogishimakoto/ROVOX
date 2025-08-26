using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ResultText : MonoBehaviour
{
    Text text; // Textオブジェクト
    StageDataManager manager;
    StageClearDataList.StageClearData datalist;
    // 初期化
    void Start()
    {
        text = gameObject.GetComponent<Text>();

        manager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        datalist = manager.GetStageData();

    }

    // 更新
    void Update()
    {
        // テキストの表示を入れ替える
        text.text = datalist.name;
    }
}
