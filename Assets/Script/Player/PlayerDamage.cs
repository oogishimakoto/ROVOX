using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] PlayerAction player;
    Bullet bullet;

    float starttime = 0;
    public bool hit { get; private set; } = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if(playerInfo == null)
            playerInfo = GetComponent<PlayerInfo>();

        if (player == null)
            player = GetComponent<PlayerAction>();

        rb = GetComponent<Rigidbody>();

        bullet = GameObject.Find("mori").GetComponent<Bullet>();
    }

    public void Damage(int damage,Vector3 _vec)
    {
        if(playerInfo != null && player != null)
        {
            if(hit) return;

            rb.AddForce(_vec, ForceMode.Impulse);

            playerInfo.HP -= damage;
            player.SetPlayerState(PlayerAction.State.DAMAGE);
            player.SetHaveWeapon(true);
            if (bullet.GetBulletState() != Bullet.State.NORMAL)
                bullet.BulletReturn();
            starttime = Time.time;
            hit = true;
        }
    }

    private void Update()
    {
        if (playerInfo != null && player != null)
        {
            if (Time.time - starttime >= playerInfo.InvincibilityTime && hit)
                hit = false;
        }
    }
}
