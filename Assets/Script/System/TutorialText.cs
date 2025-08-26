using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TutorialTextData")]
public class TutorialText : ScriptableObject
{
    public List<textData> TextListData = new List<textData>();

    [System.Serializable]
    public class textData
    {
        public List<string> TextList = new List<string>();
    }
}
