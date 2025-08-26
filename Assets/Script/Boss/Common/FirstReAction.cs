using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstReAction : MonoBehaviour
{
    Boss1 boss;
    HeadRotation head;
    
     float startTime = 3.5f;
    float count = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss1>();
        head = GetComponent<HeadRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(count >= startTime)
        {
            boss.enabled = true;
            Destroy(this);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.name == "mori") 
    //    {
    //        boss.enabled = true;
    //        //head.enabled = true;

    //        Destroy(this);
    //    }
    //}
}
