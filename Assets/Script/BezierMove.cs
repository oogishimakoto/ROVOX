using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public Transform p0; // ����_1
    public Transform p1; // ����_2
    public Transform p2; // ����_3
    public Transform p3; // ����_4

    [Header("true=�O��,false=��")]
    public bool Tertiary = false;

    public float duration { get; set; } = 1.0f; // �ړ��ɂ����鎞��

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
            // �o�ߎ��Ԃ��v�Z
            float elapsed = Time.time - startTime;
            // ���K�����ꂽ t (0 ���� 1 �܂ł̒l)
            float t = Mathf.Clamp01(elapsed / duration);
            //Debug.Log(t);
            Vector3 position = p0.position;
            if (Tertiary)
            {
                if(p3!= null)
                {
                    // �O���x�W�F�Ȑ��̈ʒu���v�Z
                    position = CalculateCubicBezierPoint(t, p0.position, p1.position, p2.position, p3.position);
                }
                else
                {
                    Debug.Log("p3���Ȃ���");
                }
            }
            else
            {

                // �񎟃x�W�F�Ȑ��̈ʒu���v�Z
                position = CalculateQuadraticBezierPoint(t, p0.position, p1.position, p2.position);
            }

            if(position != p0.position)
            {
                // �I�u�W�F�N�g�̈ʒu��ݒ�
                transform.position = position;
            }


            // t��1�ɒB������A�ēx�ŏ������蒼��
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
            //Debug.Log("p0,p1,p2�̂ǂꂩ���Ȃ���");
        }


        //�ēx�ŏ������蒼��
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startTime = Time.time; // �V���Ȉړ����J�n
        }
    }

    // �O���x�W�F�Ȑ��̓_���v�Z����֐�
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

    // �񎟃x�W�F�Ȑ��̓_���v�Z����֐�
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
