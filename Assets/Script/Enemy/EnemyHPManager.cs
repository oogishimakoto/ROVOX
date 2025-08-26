using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : MonoBehaviour
{
    public float deleatHPLine = 0.2f;

    int StartChildNum;
    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトの数を取得
        StartChildNum = this.transform.childCount;
    }

  
    public void CheckHP()
    {
        //子オブジェクトが一定以下の場合消去
        Debug.Log(this.transform.childCount * deleatHPLine <= StartChildNum);
        if(this.transform.childCount  <= StartChildNum * deleatHPLine) {
            Destroy( this.gameObject);
        }
    }
}
