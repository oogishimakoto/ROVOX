using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewWarp : MonoBehaviour
{
    public Transform targetObject; // 目標となるオブジェクト
    [SerializeField] float moveSpeed = 1f; // 移動速度

    private float startTime= 0 ; // 開始時間

    [field:SerializeField] public bool warpend { get; set; } = false; // 移動速度
    public float journeyLength { get; set; } = 0;

    Vector3 velocity;

    private void Start()
    {
        velocity = GetComponent<Rigidbody>().velocity;

    }

    void Update()
    {

        if(targetObject!= null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (journeyLength == 0)
            {
                // 現在の経過時間を計算
                journeyLength = Vector3.Distance(transform.position, targetObject.position);
                startTime = Time.time;
            }
           
            // 移動にかかった時間を計算
            float distCovered = (Time.time - startTime) * moveSpeed;
            // 全体の距離に対する現在の進行割合を計算
            float fractionOfJourney = distCovered / journeyLength;

            // 目標位置に近づける
            Vector3 newpos = targetObject.position;
            newpos.y = targetObject.position.y + transform.position.y /2;
            transform.position = Vector3.Lerp(transform.position, targetObject.position, fractionOfJourney);
            //Debug.Log(transform.position + "::" + targetObject.position + "::"+ (Vector3.Distance(transform.position, targetObject.position) <= 1.0f));
            
            // 目標に到達したら停止
            if (Vector3.Distance(transform.position, targetObject.position) <= 10.0f)
            {
                //transform.position = targetObject.position;
                targetObject = null;
                warpend = true;
                journeyLength = 0;
                GetComponent<Rigidbody>().velocity = velocity;

            }

        }


    }

}
