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


    public float amplitude = 8.0f; // ぶれの幅
    public float frequency = 0.2f; // ぶれの周波数
    public GameObject projectilePrefab; // 発射する球のプレハブ
    public float fireRate = 4.0f; // 発射する間隔（秒）
    public Transform centerPosition; // 浮遊オブジェクトの中心ポジション
    public float followSpeed = 0.5f; // 追従速度
    [Header("弾の速度")]
    public float bulletSpeed = 10.0f; // 追従速度

    [Header("弾のタイプ")]
    public satelliteType type = satelliteType.NORMAL; // 追従速度


    private float randomOffsetX;
    private float randomOffsetY;
    private float randomOffsetZ;
    private float nextFireTime;

    GameObject PlayerObj;
    Rigidbody playerRb;

    int TargetNum = -1;

    void Start()
    {
        // Perlin Noise用のランダムなオフセットを設定
        randomOffsetX = UnityEngine.Random.Range(0f, 100f);
        randomOffsetY = UnityEngine.Random.Range(0f, 100f);
        randomOffsetZ = UnityEngine.Random.Range(0f, 100f);

        // 次の発射時間を設定
        nextFireTime = Time.time + fireRate + UnityEngine.Random.Range(0f, 5f);

        PlayerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetHeadObj();
        playerRb = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<Rigidbody>();

        transform.parent = null;
        transform.GetComponent<SphereCollider>().radius *= 2.0f;
    }

    void Update()
    {
        // 中心ポジションを追従
        Vector3 targetPosition = centerPosition.position;

        // Perlin Noiseを使用して上下左右およびz軸方向にぶれる動きを作成
        float offsetX = (Mathf.PerlinNoise(Time.time * frequency + randomOffsetX, 0) - 0.5f) * amplitude;
        float offsetY = (Mathf.PerlinNoise(0, Time.time * frequency + randomOffsetY) - 0.5f) * amplitude;
        float offsetZ = (Mathf.PerlinNoise(Time.time * frequency + randomOffsetZ, 0) - 0.5f) * amplitude;

        // 新しい位置を計算
        Vector3 newPosition = targetPosition + new Vector3(offsetX, offsetY, offsetZ);

        // 位置を徐々に更新
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);

        // 一定時間ごとに球を発射
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireProjectile()
    {
        // プレハブの球を発射
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
