using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public Transform p0; // 制御点1
    public Transform p1; // 制御点2
    public Transform p2; // 制御点3
    public Transform p3; // 制御点4

    [Header("true=三次,false=二次")]
    public bool Tertiary = false;

    public float duration { get; set; } = 1.0f; // 移動にかかる時間

    private float startTime;

   
    public void SetStartTime() { startTime = Time.time; }

    void Start()
    {
        
        startTime = Time.time;
    }

    void Update()
    {
        if(p0!= null && p1 != null && p2 != null)
        {
            // 経過時間を計算
            float elapsed = Time.time - startTime;
            // 正規化された t (0 から 1 までの値)
            float t = Mathf.Clamp01(elapsed / duration);
            //Debug.Log(t);
            Vector3 position = p0.position;
            if (Tertiary)
            {
                if(p3!= null)
                {
                    // 三次ベジェ曲線の位置を計算
                    position = CalculateCubicBezierPoint(t, p0.position, p1.position, p2.position, p3.position);
                }
                else
                {
                    Debug.Log("p3がないよ");
                }
            }
            else
            {

                // 二次ベジェ曲線の位置を計算
                position = CalculateQuadraticBezierPoint(t, p0.position, p1.position, p2.position);
            }

            if(position != p0.position)
            {
                // オブジェクトの位置を設定
                transform.position = position;
            }


            // tが1に達したら、再度最初からやり直す
            if (t >= 1)
            {
                p0 = null;
                //p1 = null;
                //p2 = null;
                p3 = null;
            }
        }
        else
        {
            //Debug.Log("p0,p1,p2のどれかがないよ");
        }


        //再度最初からやり直す
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startTime = Time.time; // 新たな移動を開始
        }
    }

    // 三次ベジェ曲線の点を計算する関数
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; // (1-t)^3 * p0
        point += 3 * uu * t * p1; // 3 * (1-t)^2 * t * p1
        point += 3 * u * tt * p2; // 3 * (1-t) * t^2 * p2
        point += ttt * p3; // t^3 * p3

        return point;
    }

    // 二次ベジェ曲線の点を計算する関数
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0; // (1-t)^2 * p0
        point += 2 * u * t * p1; // 2 * (1-t) * t * p1
        point += tt * p2; // t^2 * p2

        return point;
    }
}
