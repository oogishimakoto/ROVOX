using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;

public class Missile : MonoBehaviour
{
    private Transform target;
    public Rigidbody HomingRigidbody;
    public float HomingTime;
    private float HomingCount;

    public float power;
    public float rotationSpeed;
    private bool isShot;

    [SerializeField] GameObject exprosionObj;


    [SerializeField] float lifeTime;
    float timeCount = 0.0f;

    private void Start()
    {
        target = transform.root.GetComponent<Enemy_PlayerManager>().GetHeadObj().transform;
    }
    private void Update()
    {
        if (target != null && isShot)
        {
            HomingCount += Time.deltaTime;

            Vector3 direction = target.transform.position - transform.position;

            // 目標の回転を計算
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.forward);

            // 現在の回転と目標の回転を線形補間でスムーズに回転させる
            if (HomingCount <= HomingTime)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
            }

            // 力の方向を更新
            HomingRigidbody.velocity = transform.forward * power;


            timeCount += Time.deltaTime;
            if (timeCount > lifeTime)
            {
                CreateExprosion();
                ParentConstraint constraint = transform.GetComponent<ParentConstraint>();
                if (constraint != null)
                {
                    constraint.constraintActive = true;
                }
                isShot = false;
            }
        }
    }

    public void ShotBullet(float _randomPower, float _randomRotation)
    {
        ParentConstraint constraint = transform.GetComponent<ParentConstraint>();
        if(constraint != null)
        {
            constraint.constraintActive = false;
        }
        isShot = true;
        HomingRigidbody.AddForce(transform.forward * (power + _randomPower), ForceMode.Impulse);
        rotationSpeed += _randomRotation;
        timeCount = 0.0f;
        HomingCount = 0.0f;
    }

    void CreateExprosion()
    {
        Instantiate(exprosionObj, this.transform.GetChild(0).position, Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isShot)
        {
            if (other.transform.tag != "Enemy" && other.transform.tag != "Wall" && other.transform.tag != "Core")
            {
           
                if (other.transform.tag == "Player")
                {
                    PlayerDamage damage = other.GetComponent<PlayerDamage>();
                    if (damage != null)
                    {
                        Vector3 force = other.transform.position - transform.position;
                        force.Normalize();
                        force *= 10.0f;
                        force.y = 7.0f;

                        damage.Damage(1, force);
                    }
                }
                CreateExprosion();
                ParentConstraint constraint = transform.GetComponent<ParentConstraint>();
                if (constraint != null)
                {
                    constraint.constraintActive = true;
                }
                isShot = false;
            }
        }
    }
}