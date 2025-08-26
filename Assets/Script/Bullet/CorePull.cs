using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAction;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CorePull : MonoBehaviour
{
    [Header("武器")]
    Bullet bullet;
    int damege = 0;
    [SerializeField] PlayerAnimationController animator;
    public bool HitCoreflg { get; set; } = false;
    public Collider HitCoreObject { get; set; } = null;

    [SerializeField]GameObject PullUI;

    TutorialTextManager tutorial;

    // Start is called before the first frame update
    void Start()
    {
        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }

        bullet = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HitCoreflg && transform.parent != null)
        {
            if (animator.PlayerDamage())
            {
                Init();
                bullet.BulletReturn();
                PullUI.SetActive(false);
           
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(1))
            {
                if (bullet.GetBulletState() != Bullet.State.PULL)
                {
                    bullet.SetBulletState(Bullet.State.PULL);
                    bullet.ResetCount();
                }
            }
            else
            {
                //チュートリアル用
                if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    //現在のミッション確認
                    if (tutorial.GetNowCount() == 2 && tutorial.GetTextCount() == 1)
                    {
                        tutorial.TextCount(2);
                    }

                    //現在のミッション確認
                    if (tutorial.GetNowCount() == 4 && tutorial.GetTextCount() == 1)
                    {
                        tutorial.TextCount(2);
                    }

                    //現在のミッション確認
                    if (tutorial.GetNowCount() == 5 && tutorial.GetTextCount() == 1)
                    {
                        tutorial.TextCount(2);
                    }
                }
                PullUI.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Return) || Input.GetMouseButtonUp(1))
            {
                 bullet.BulletReturn();
                PullUI.SetActive(false);

            }


            if (bullet.GetBulletPullState() && HitCoreObject != null)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    var hit = HitCoreObject.GetComponent<IEnemyDamage>();
                    if (hit != null)
                    {
                        hit.Damage(1);
                        animator.SetPullAnimation();
                    }
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
        HitCoreflg = false;
        HitCoreObject = null;
        damege = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Core")
        {
            HitCoreflg = true;
            HitCoreObject = other;
            damege = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("抜けました");

        Init();
    }

    public void EnemyPullModePress(InputAction.CallbackContext context)
    {
        
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (HitCoreflg && transform.parent != null)
                {
                    if(bullet.GetBulletState() != Bullet.State.PULL)
                    {
                        bullet.SetBulletState(Bullet.State.PULL);
                        bullet.ResetCount();
                    }

                    
                }
                break;
        }

    }

    public void EnemyPullModeRelese(InputAction.CallbackContext context)
    {

        switch (context.phase)
        {
            case InputActionPhase.Canceled:
                if (HitCoreflg && transform.parent != null)
                {
                    if (bullet.GetBulletState() == Bullet.State.PULL)
                    {
                        PullUI.SetActive(false);

                        bullet.BulletReturn();
                    }


                }
                break;
        }

    }


    public void EnemyDamage(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (HitCoreflg)
                {

                    if (bullet.GetBulletPullState() && HitCoreObject != null)
                    {
                        HitCoreObject.GetComponent<IEnemyDamage>().Damage(1);
                        animator.SetPullAnimation();
                        //Debug.Log("引っこ抜き");
                    }
                }
                break;
        }


    }
}
