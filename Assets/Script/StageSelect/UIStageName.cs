using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageName : MonoBehaviour
{
    Text text;


    public void SetStageName(string _StageName)
    {
       text.text = _StageName;
    }

    private void OnEnable()
    {
        text = GetComponent<Text>();

    }
}
