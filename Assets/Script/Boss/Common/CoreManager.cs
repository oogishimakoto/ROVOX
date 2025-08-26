using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreManager : MonoBehaviour
{

    private int coreNum;    //破壊されていないコアの数

    [Header("コアの数ごとの銛を引き抜く時間(要素０がすべて破壊されたとき)"),SerializeField]
    float[] pullTime;
    [Header("コアの数ごとのダメージ倍率"), SerializeField]
    int[] damageRate;

    [Header("心臓のcoreのゲームオブジェクト")]
    [SerializeField] GameObject MainCore;
    [SerializeField] float rotationSpeed = 50.0f; // 回転速度 (度/秒)
    [SerializeField] float totalRotation = 60.0f; // 総回転量 (度)

    [Header("コアのUIゲームオブジェクト")]
    [SerializeField] GameObject CoreUI;

    [Header("コアのスコアデータ")]
    [SerializeField] StageDataManager StageData;

    private float currentRotation = 0.0f; // 現在の回転量 (度)

    int BreakCoreNum = 0;

    bool IsRotation;

    private void Start()
    {
        StageData = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
    }

    private void Update()
    {
        if (IsRotation)
        {

            float rotationAmount = rotationSpeed * Time.deltaTime;
            MainCore.transform.Rotate(0,0 , rotationAmount);
            currentRotation += rotationAmount;
            if (currentRotation >= totalRotation)
            {
                IsRotation = false;
            }
        }
    }

    //コアの数を増やす
    public void AddCoreNum()
    {
        coreNum += 1;
    }
    //コアの数を増やす
    public void SubCoreNum()
    {
        coreNum -= 1;
        CoreUI.transform.GetChild(BreakCoreNum).GetComponent<Image>().color = Color.white;
        BreakCoreNum += 1;
        IsRotation = true;
        currentRotation = 0.0f;
    }

    //残っているコアの数に応じて銛を引き抜く時間を返す
    public float GetPullTime()
    {
        return pullTime[coreNum];
    }

    public int GetDamageRate()
    {
        return damageRate[coreNum];
    }

    public void SetScoreCoreNum()
    {
        StageData.GetStageData().ScrewBreakCount = 5-coreNum;
    }
}
