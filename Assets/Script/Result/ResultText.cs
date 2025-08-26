using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ResultText : MonoBehaviour
{
    Text text; // Text�I�u�W�F�N�g
    StageDataManager manager;
    StageClearDataList.StageClearData datalist;
    // ������
    void Start()
    {
        text = gameObject.GetComponent<Text>();

        manager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        datalist = manager.GetStageData();

    }

    // �X�V
    void Update()
    {
        // �e�L�X�g�̕\�������ւ���
        text.text = datalist.name;
    }
}
