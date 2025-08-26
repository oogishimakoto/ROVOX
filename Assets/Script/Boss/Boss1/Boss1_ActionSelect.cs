using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_ActionSelect : MonoBehaviour, IActionSelect
{

    AttackDataList dataList;
    [SerializeField] float SpecialAttackTime = 180;       //特殊攻撃をする時間
    [SerializeField] float SpecialAttackCount = 0;       //特殊攻撃用タイマー

    [field :SerializeField] public int AttackPatternNum { get; private set; }      //攻撃パターン格納用


     PlayerAction hierarchyInfo;


    private void Start()
    {
        hierarchyInfo = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<PlayerAction>();
        dataList = transform.root.transform.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }

    private void Update()
    {
        SpecialAttackCount += Time.deltaTime;

    }

    //攻撃パターン決定
    public void ActionSelect()
    {

        //プレイヤーのいるフロアから行動可能な攻撃をランダムで抽選
        AttackPatternNum = dataList.Process[hierarchyInfo.StageHierarchical].paternNum[Random.Range(0, dataList.Process[hierarchyInfo.StageHierarchical].paternNum.Length)];

#if UNITY_EDITOR

        Debug.Log(dataList.data[AttackPatternNum].C_AttackName +  "攻撃");

#endif

    }
}
