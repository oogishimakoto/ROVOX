using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [Header("爆発時間"),SerializeField] float lifeTime;
    [Header("爆発のサイズがもとからどれだけ大きくなるか"),SerializeField] float scaleIncrease;
    [Header("ダメージ判定発生感覚"),SerializeField] float AttackColInterval;

    float timeCount;
    float colliderONCount;
    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        colliderONCount += Time.deltaTime;

        this.transform.localScale += Vector3.one * scaleIncrease *Time.deltaTime;

        if(colliderONCount > AttackColInterval ) {

            this.transform.GetComponent<Collider>().enabled = true;
            colliderONCount = 0.0f;
        }

        if (timeCount > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            PlayerDamage info = other.GetComponent<PlayerDamage>();
            if(info !=  null)
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
