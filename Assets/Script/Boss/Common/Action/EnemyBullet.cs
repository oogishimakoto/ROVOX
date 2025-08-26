using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float f_DeleteTime;
    [SerializeField] float f_MoveSpeed;

    [SerializeField] GameObject exprosionObj;

    float count;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * f_MoveSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count > f_DeleteTime) {
            Destroy(this.gameObject);
        }
    }

    void CreateExprosion()
    {
        Instantiate(exprosionObj , this.transform.GetChild(0).position, Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Enemy" && other.transform.tag != "Wall" && other.transform.tag != "Core")
        {
             CreateExprosion();
            Destroy(this.gameObject);
        }
    }
}
