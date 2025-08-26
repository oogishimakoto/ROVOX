using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [Header("オンのときの時間")]
    public float onDuration = 2.0f;

    [Header("オフのときの時間")]
    public float offDuration = 2.0f;

    [Header("マテリアルを変える地面")]
    public GameObject linkGround;

    [Header("地面のマテリアル")]
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;

    [SerializeField] int materialIndexNum;

    private float timer = 0.0f;
    private bool isActive = true;

    [SerializeField] bool isDamage = true;
    float timeCount;

    private Collider magmaCollider;
    private MeshRenderer MagmaMesh;

    [Header("点滅開始時間（秒）")]
    public float blinkStartTime = 2.0f;
    public float blinkInterval = 0.2f;

    private bool isBlinking = false;
    private float blinkTimer = 0.0f;
    private bool blinkState = false;

    private void Start()
    {
        magmaCollider = GetComponent<Collider>();
        MagmaMesh = linkGround.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if ((isActive && timer >= onDuration - blinkStartTime) ||
            (!isActive && timer >= offDuration - blinkStartTime))
        {
            if (!isBlinking)
            {
                isBlinking = true;
                blinkTimer = 0.0f;
                blinkState = false;
            }
            HandleBlinking();
        }
        else
        {
            isBlinking = false;
            UpdateMaterial(isActive ? OnMaterial : OffMaterial);
        }

        if (isActive && timer >= onDuration)
        {
            SwitchState();
        }
        else if (!isActive && timer >= offDuration)
        {
            SwitchState();
        }
    }

    private void HandleBlinking()
    {
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= blinkInterval)
        {
            blinkTimer = 0.0f;
            blinkState = !blinkState;
            UpdateMaterial(blinkState ? OffMaterial : OnMaterial);
        }
    }

    private void SwitchState()
    {
        timer = 0.0f;
        isActive = !isActive;
        isDamage = isActive;
        isBlinking = false;
        UpdateMaterial(isActive ? OnMaterial : OffMaterial);
    }

    private void UpdateMaterial(Material material)
    {
        if (MagmaMesh.materials.Length > 1)
        {
            Material[] materialBuf = MagmaMesh.materials;
            materialBuf[materialIndexNum] = material;
            MagmaMesh.materials = materialBuf;
        }
        else
        {
            MagmaMesh.material = material;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && isDamage == true)
        {
            PlayerDamage info = other.gameObject.GetComponent<PlayerDamage>();
            if (info != null)
            {
                info.Damage(1, Vector3.zero);
                isDamage = false;
            }
        }
    }
}
