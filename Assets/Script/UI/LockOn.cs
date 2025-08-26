using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
   [SerializeField] GameObject[] lockOnUIList;

   public void UIDisplay(bool _isDisplay)
    {
        for(int i = 0; i < lockOnUIList.Length; i++)
        {
            lockOnUIList[i].SetActive(_isDisplay);
        }
    }

}
