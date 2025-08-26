using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultChange : MonoBehaviour
{
    StageDataManager manager;
    StageClearDataList.StageClearData datalist;

    PlayerInfo player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInfo>();

        manager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        Debug.Log(manager.GetStageData().name);
    }

    public void ChangeScene()
    {
        datalist = manager.GetStageData();

        datalist.RestHP = player.HP;
        Debug.Log(datalist.name);
        FadeManager.Instance.LoadScene("Result", 2);
    }
}
