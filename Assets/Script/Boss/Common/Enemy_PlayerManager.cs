using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PlayerManager : MonoBehaviour
{
    [Header ("プレイヤーオブジェクト")]
    [SerializeField] GameObject PlayerObj;

    [Header("プレイヤーの頭オブジェクト")]
    [SerializeField] GameObject HeadObj;

    [Header("敵キャラクターの攻撃時の情報")]
    [SerializeField] AttackDataList dataList;


    public GameObject GetPlayerObj()
    {
        return PlayerObj;
    }

    public GameObject GetHeadObj()
    {
        return HeadObj;
    }

    public AttackDataList GetAttackData()
    {
        return dataList;
    }
}
