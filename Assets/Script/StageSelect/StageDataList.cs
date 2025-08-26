using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataList : MonoBehaviour
{
    public List<Sprite> gimmickSprite;
    public List<StageData> List;
}

[Serializable]
public class StageData
{
    [Tooltip("遷移するシーンの名前")] public string LinkSceneName;
    [Tooltip("ステージイメージのオブジェクト")] public Sprite StageObj;
    [Tooltip("表示するステージの名前")] public string stagename;
    [Tooltip("ステージのギミックUIの番号")] public List<int> Gimmick;
    [Tooltip("コア数")] public int CoreNum;

}
