using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealPull : MonoBehaviour
{
    [Header("ïêäÌ")]
    Bullet bullet;
    int damege = 0;
    [SerializeField] PlayerAnimationController animator;
    public bool HitCarrotflg { get; set; } = false;
    public Collider HitCsrrotObject { get; set; } = null;

    [SerializeField] GameObject PullUI;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HitCarrotflg && transform.parent != null)
        {
            if (animator.PlayerDamage())
            {
                Init();
                bullet.BulletReturn();
                PullUI.SetActive(false);

            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(1))
            {
                if (bullet.GetBulletState() != Bullet.State.HEALPULL)
                {
                    bullet.SetBulletState(Bullet.State.HEALPULL);
                    bullet.ResetCount();
                }

            }
            else
            {
                
                PullUI.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Return) || Input.GetMouseButtonUp(1))
            {
                bullet.BulletReturn();
                PullUI.SetActive(false);
            }


            if (bullet.GetBulletPullState() && HitCsrrotObject != null)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    if(HitCsrrotObject.GetComponent<CarrotHeal>())
                    {
                        HitCsrrotObject.GetComponent<CarrotHeal>().Decrease(1);

                    }
                    else
                    {
                        HitCsrrotObject.transform.parent.GetComponent<CarrotHeal>().Decrease(1);

                    }
                    animator.SetPullAnimation();

                }
            }
        }
        else
        {
            PullUI.SetActive(false);

        }

    }
    private void Init()
    { 
        HitCarrotflg = false;
        HitCsrrotObject = null;
        damege = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Heal")
        {
            HitCarrotflg = true;
            HitCsrrotObject = other;
            damege = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //  Debug.Log("î≤ÇØÇ‹ÇµÇΩ");

        Init();
    }

    public void CarrotPullModePress(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (HitCarrotflg)
                {
                    if(bullet != null)
                    {
                        if (bullet.GetBulletState() != Bullet.State.HEALPULL)
                        {
                            bullet.SetBulletState(Bullet.State.HEALPULL);
                            bullet.ResetCount();
                        }
     
                    }
                    else
                    {
                        Debug.Log("BulletÇ™Ç»Ç¢ÇÊ");
                    }

                }
                break;
        }

    }

    public void CarrotPullModeRelese(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Canceled:
                if (HitCarrotflg)
                {
                    if (bullet != null)
                    {
                        if (bullet.GetBulletState() == Bullet.State.HEALPULL)
                        {
                            bullet.BulletReturn();
                            PullUI.SetActive(false);
                        }

                    }
                    else
                    {
                        Debug.Log("BulletÇ™Ç»Ç¢ÇÊ");
                    }

                }
                break;
        }

    }

    public void CarrotDamage(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (HitCarrotflg)
                {
                    
                    if (bullet.GetBulletPullState() && HitCsrrotObject != null)
                    {
                        
                        if (HitCsrrotObject.transform.GetComponent<CarrotHeal>())
                        {
                            HitCsrrotObject.transform.GetComponent<CarrotHeal>().Decrease(1);
                        }
                        else
                        {
                            HitCsrrotObject.transform.parent.GetComponent<CarrotHeal>().Decrease(1);

                        }
                        animator.SetPullAnimation();
                       // Debug.Log("à¯Ç¡Ç±î≤Ç´");
                    }
                }
                break;
        }


    }
}

