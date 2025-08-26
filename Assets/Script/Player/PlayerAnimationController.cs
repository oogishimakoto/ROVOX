using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerinfo;
    [SerializeField] private PlayerAction player;
    [SerializeField] private TitleAction titleplayer;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Animator animator;
    private float animationblendtime= 1;

    public Animator GetAnimator() { return animator; }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title1.02")
        {
            //RUNアニメーション
            if (titleplayer.GetPlayerState() == TitleAction.State.RUN)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
        else
        {
            StagePlayer();
        }
       
    }

    private void StagePlayer()
    {
        if (player != null && animator != null)
        {
            //RUNアニメーション
            if (player.GetPlayerState() == PlayerAction.State.RUN)
            {
                animator.SetBool("Run", true);

            }
            else
            {
                animator.SetBool("Run", false);
            }

            //CHARGEアニメーション
            if (player.GetPlayerState() == PlayerAction.State.CHARGE)
            {

                animator.SetBool("Charge", true);

            }
            else
            {
                animator.SetBool("Charge", false);
               

            }

            //THROWアニメーション
            if (player.GetPlayerState() == PlayerAction.State.THROW)
            {

                if (player.GetChargeState() == PlayerAction.Charge.charge3)
                {

                    animator.SetBool("Throw_Large", true);
                    player.SetHaveWeapon(false);


                }
                if (player.GetChargeState() == PlayerAction.Charge.charge2)
                {

                    animator.SetBool("Throw_Medium", true);
                    player.SetHaveWeapon(false);
                }
                if (player.GetChargeState() == PlayerAction.Charge.charge1)
                {

                    animator.SetBool("Throw_Small", true);
                    player.SetHaveWeapon(false);
                }
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationblendtime && player.GetPlayerState() != PlayerAction.State.CHARGE &&
                    (animator.GetCurrentAnimatorStateInfo(0).IsName("Shot_Large") ||
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Shot_Medium") ||
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Shot_Small")))
                {
                    //Debug.Log("投げ終わりました");
                    animator.SetBool("Charge", false);
                    animator.SetBool("Throw_Large", false);
                    animator.SetBool("Throw_Medium", false);
                    animator.SetBool("Throw_Small", false);
                    player.SetPlayerState(PlayerAction.State.IDLE);

                }

            }
            else
            {
                //アニメーションが終わっているか
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationblendtime && player.GetPlayerState() != PlayerAction.State.CHARGE)
                {
                    //Debug.Log("投げ終わりました");
                    animator.SetBool("Charge", false);
                    animator.SetBool("Throw_Large", false);
                    animator.SetBool("Throw_Medium", false);
                    animator.SetBool("Throw_Small", false);
                }

            }

            //COLLECTアニメーション
            if (player.GetPlayerState() == PlayerAction.State.COLLECT)
            {
                animator.SetBool("Collect", true);

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationblendtime &&
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Collect"))
                {
                    Debug.Log("回収アニメーション");
                    animator.SetBool("Collect", false);
                    player.SetPlayerState(PlayerAction.State.IDLE);
                    player.GetComponent<PlayerAction>().SetHaveWeapon(true);
                }
            }
            else
            {
                animator.SetBool("Collect", false);
            }

            //DAMAGEアニメーション
            if (player.GetPlayerState() == PlayerAction.State.DAMAGE)
            {
                animator.SetBool("Damage", true);

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationblendtime &&
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
                {
                    animator.SetBool("Damage", false);
                    player.SetPlayerState(PlayerAction.State.IDLE);

                }
            }
            else
            {
                animator.SetBool("Damage", false);
            }

            //引っこ抜きアニメーション
            if (bullet.GetBulletState() == Bullet.State.PULL || bullet.GetBulletState() == Bullet.State.HEALPULL)
            {


                animator.SetBool("PullTime", true);

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationblendtime)
                {

                    animator.SetBool("PullOut_Medium", false);
                }
            }
            else
            {
                animator.SetBool("PullTime", false);
                animator.SetBool("PullOut_Medium", false);
                animator.SetBool("PullOut_Large", false);
            }

            //DEATHアニメーション
            if (player.GetPlayerState() == PlayerAction.State.DEATH)
            {
                animator.SetBool("Death", true);
            }
            else
            {
                animator.SetBool("Death", false);
            }

            //武器を持っているかどうか
            if (player.GetHaveWeapon())
            {
                animator.SetBool("Weapon", true);

            }
            else
            {
                animator.SetBool("Weapon", false);

            }
        }
        else
        {
            Debug.Log("PlayerAnimationControllerスクリプトにコンポーネントを設定してください");
        }
    }

    public void PlayerShot()
    {
        StartCoroutine(player.Shot());
        
    }

    public void SetPullAnimation()
    {
        if(bullet.GetBulletState() == Bullet.State.PULL)
        {
            
            if (animator.GetBool("PullTime"))
            {
                
                animator.SetBool("PullOut_Medium", true);

            }
        }

    }

    public bool PlayerDamage()
    {
        return animator.GetBool("Damage");

    }

    public void ChangeCharge()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationblendtime && animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
        {
            animator.SetBool("Charge", false);

        }

        if (!animator.GetBool("Charge") )
        {
           
            player.SetPlayerState(PlayerAction.State.IDLE);
        }

    }
}
