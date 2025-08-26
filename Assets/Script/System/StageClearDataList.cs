using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StageClearData")]
public class StageClearDataList : ScriptableObject
{
    public List<StageClearData> Stage = new List<StageClearData>();

    [System.Serializable]
    public class StageClearData
    {
        public string name;
        public int ScrewBreakCount;
        public int RestHP;
    }
}
