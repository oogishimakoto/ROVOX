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
        //�q�I�u�W�F�N�g�̐����擾
        StartChildNum = this.transform.childCount;
    }

  
    public void CheckHP()
    {
        //�q�I�u�W�F�N�g�����ȉ��̏ꍇ����
        Debug.Log(this.transform.childCount * deleatHPLine <= StartChildNum);
        if(this.transform.childCount  <= StartChildNum * deleatHPLine) {
            Destroy( this.gameObject);
        }
    }
}
