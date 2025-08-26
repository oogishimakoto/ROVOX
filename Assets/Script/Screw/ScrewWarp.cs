using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewWarp : MonoBehaviour
{
    public Transform targetObject; // �ڕW�ƂȂ�I�u�W�F�N�g
    [SerializeField] float moveSpeed = 1f; // �ړ����x

    private float startTime= 0 ; // �J�n����

    [field:SerializeField] public bool warpend { get; set; } = false; // �ړ����x
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
                // ���݂̌o�ߎ��Ԃ��v�Z
                journeyLength = Vector3.Distance(transform.position, targetObject.position);
                startTime = Time.time;
            }
           
            // �ړ��ɂ����������Ԃ��v�Z
            float distCovered = (Time.time - startTime) * moveSpeed;
            // �S�̂̋����ɑ΂��錻�݂̐i�s�������v�Z
            float fractionOfJourney = distCovered / journeyLength;

            // �ڕW�ʒu�ɋ߂Â���
            Vector3 newpos = targetObject.position;
            newpos.y = targetObject.position.y + transform.position.y /2;
            transform.position = Vector3.Lerp(transform.position, targetObject.position, fractionOfJourney);
            //Debug.Log(transform.position + "::" + targetObject.position + "::"+ (Vector3.Distance(transform.position, targetObject.position) <= 1.0f));
            
            // �ڕW�ɓ��B�������~
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
