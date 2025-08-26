using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSatellite : MonoBehaviour
{
    private Vector3 moveVec;
    private float moveSpeed = 8.0f;
    public float lifeTime = 10.0f; // íeÇÃéıñΩ

    void Start()
    {
       }

    void Update()
    {
        transform.position += moveVec * moveSpeed * Time.deltaTime;
    }

    public void Shot(Vector3 _moveVec, float _moveSpeed)
    {
        moveVec = _moveVec.normalized;
        moveSpeed = _moveSpeed;
        // lifeTimeå„Ç…íeÇè¡ñ≈Ç≥ÇπÇÈ
        Destroy(gameObject, lifeTime);

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerDamage info = other.GetComponent<PlayerDamage>();
            if (info != null)
            {
                Vector3 force = other.transform.position - transform.position;
                force.Normalize();
                force *= 10.0f;
                force.y = 7.0f;
                info.Damage(1, force);
            }
        }
        this.transform.GetComponent<Collider>().enabled = false;
    }
}
