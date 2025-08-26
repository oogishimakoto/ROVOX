using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;

public class CameraMove : MonoBehaviour
{

    [SerializeField] GameObject targetObj;

    [SerializeField] private Vector3 Offset; //ターゲットからの距離
    [SerializeField] private float f_Distance; //ターゲットからの距離
    [SerializeField] private float f_RotationSpeed = 3.0f; // 回転速度

    private float f_Rotation = 0.0f; //回転


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ターゲットの周りを回転
        // 左に回転
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            f_Rotation -=  f_RotationSpeed * Time.deltaTime;
        }
        // 右に回転
        if (Input.GetKey(KeyCode.RightArrow))
        {
            f_Rotation +=  f_RotationSpeed * Time.deltaTime;
        }
      
        Vector3 newPos = new(targetObj.transform.position.x - f_Distance * Mathf.Sin(f_Rotation),
                             0.0f,
                             targetObj.transform.position.z - f_Distance * Mathf.Cos(f_Rotation));

        transform.position = newPos  + Offset;
        

        transform.LookAt(targetObj.transform.position +( transform.forward * 1.5f) );
    }
}
