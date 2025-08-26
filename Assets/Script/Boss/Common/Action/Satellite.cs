using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public enum satelliteType
{
    NORMAL,
    PREDICTION,
}

[RequireComponent(typeof(SphereCollider))]

public class Satellite : MonoBehaviour
{


    public float amplitude = 8.0f; // �Ԃ�̕�
    public float frequency = 0.2f; // �Ԃ�̎��g��
    public GameObject projectilePrefab; // ���˂��鋅�̃v���n�u
    public float fireRate = 4.0f; // ���˂���Ԋu�i�b�j
    public Transform centerPosition; // ���V�I�u�W�F�N�g�̒��S�|�W�V����
    public float followSpeed = 0.5f; // �Ǐ]���x
    [Header("�e�̑��x")]
    public float bulletSpeed = 10.0f; // �Ǐ]���x

    [Header("�e�̃^�C�v")]
    public satelliteType type = satelliteType.NORMAL; // �Ǐ]���x


    private float randomOffsetX;
    private float randomOffsetY;
    private float randomOffsetZ;
    private float nextFireTime;

    GameObject PlayerObj;
    Rigidbody playerRb;

    int TargetNum = -1;

    void Start()
    {
        // Perlin Noise�p�̃����_���ȃI�t�Z�b�g��ݒ�
        randomOffsetX = UnityEngine.Random.Range(0f, 100f);
        randomOffsetY = UnityEngine.Random.Range(0f, 100f);
        randomOffsetZ = UnityEngine.Random.Range(0f, 100f);

        // ���̔��ˎ��Ԃ�ݒ�
        nextFireTime = Time.time + fireRate + UnityEngine.Random.Range(0f, 5f);

        PlayerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetHeadObj();
        playerRb = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<Rigidbody>();

        transform.parent = null;
        transform.GetComponent<SphereCollider>().radius *= 2.0f;
    }

    void Update()
    {
        // ���S�|�W�V������Ǐ]
        Vector3 targetPosition = centerPosition.position;

        // Perlin Noise���g�p���ď㉺���E�����z�������ɂԂ�铮�����쐬
        float offsetX = (Mathf.PerlinNoise(Time.time * frequency + randomOffsetX, 0) - 0.5f) * amplitude;
        float offsetY = (Mathf.PerlinNoise(0, Time.time * frequency + randomOffsetY) - 0.5f) * amplitude;
        float offsetZ = (Mathf.PerlinNoise(Time.time * frequency + randomOffsetZ, 0) - 0.5f) * amplitude;

        // �V�����ʒu���v�Z
        Vector3 newPosition = targetPosition + new Vector3(offsetX, offsetY, offsetZ);

        // �ʒu�����X�ɍX�V
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);

        // ��莞�Ԃ��Ƃɋ��𔭎�
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireProjectile()
    {
        // �v���n�u�̋��𔭎�
        GameObject bulletObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BulletSatellite bullet = bulletObj.GetComponent<BulletSatellite>();
        bulletObj.gameObject.tag = "Wall";
        if (bullet != null)
        {
            if (type == satelliteType.NORMAL)
            {
                bullet.Shot(PlayerObj.transform.position - transform.position, bulletSpeed);
            }
            else
            {
                Vector3 playerpos = playerRb.velocity;
                playerpos.y = 0;
                playerpos *= Mathf.Abs((PlayerObj.transform.position - transform.position).magnitude) / bulletSpeed;
                playerpos += PlayerObj.transform.position;
                bullet.Shot(playerpos - transform.position, bulletSpeed);
                bullet.Shot(playerpos - transform.position, bulletSpeed);

            }
        }
    }

    public void SetFollowTarget(Transform target)
    {
        centerPosition = target;
    }

    public void SetBulletType(satelliteType _type)
    {
        type = _type;
    }

    public void SetBulletObj(GameObject _obj)
    {
        projectilePrefab = _obj;
    }

    public void SetTargetNum(int _num)
    {
        TargetNum = _num;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "mori")
        {

            other.GetComponent<Bullet>().BulletReturn();
            centerPosition.parent.GetComponent<SatelliteTargetPos>().ReleaseUseFlg(TargetNum);
            Destroy(this.gameObject);
        }
    }
}
