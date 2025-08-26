using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    [Header("���X�|�[���ʒu")]
    [SerializeField,Tooltip("�X�^�[�g�ŏ����ʒu��ݒ�")]Vector3 spawnPosition;

    private int respawn;

    PlayerDamage damage;

    private void Start()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        damage = player.GetComponent<PlayerDamage>();
        spawnPosition = player.transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.position = spawnPosition;
            respawn++;
            if(respawn >= 5)
            {
                damage.Damage(1, damage.gameObject.transform.position);
                respawn = 0;
            }
        }
    }
}
