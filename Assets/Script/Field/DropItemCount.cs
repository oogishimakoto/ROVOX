using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemCount : MonoBehaviour
{
    [SerializeField]  private int n_ItemCount = 0;

    private bool b_DropCount = false;

    public void SetDropItemCount() { n_ItemCount++; }
    public int GetDropItemCount() { return n_ItemCount; }

    public void SetDropItem(bool flg) { b_DropCount = flg; }
    public bool GetDropItem() { return b_DropCount; }

    //public int ItemCount
    //{
    //    get { return n_ItemCount; }
    //    set { n_ItemCount++; }
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
