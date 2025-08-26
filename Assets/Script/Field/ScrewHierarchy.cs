using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrewHierarchy : MonoBehaviour
{
    [SerializeField, Header("このオブジェクトの階層")] int HierarchyNum;
    [SerializeField ] PlayerAction player;
    [SerializeField] GameObject target;
    [SerializeField] GameObject common;

    [SerializeField] Bullet bullet;

    [SerializeField] RustedScrew rustedComp;

    TutorialTextManager tutorial;//チュートリアル用


    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
      
        rustedComp = this.transform.GetComponent<RustedScrew>();

        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }
    }

    private void Update()
    {
        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (tutorial.GetNowCount() >= 3)
            {
                GetComponent<MeshRenderer>().enabled= true;
                GetComponent<Collider>().enabled = true;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

            }
        }


        if(player != null && target != null)
        {
            if(player.StageHierarchical == HierarchyNum && bullet.GetBulletState() != Bullet.State.WARP && bullet.GetBulletState() != Bullet.State.RETURN)
            {
                transform.position = Vector3.Lerp(transform.position,target.transform.position,0.25f);
                if (rustedComp == null || rustedComp.enabled == false)
                {
                    gameObject.tag = "Untagged";
                }
            }
            else if(bullet.GetBulletState() != Bullet.State.WARP && bullet.GetBulletState() != Bullet.State.RETURN)
            {
                transform.position = Vector3.Lerp(transform.position, common.transform.position, 0.25f);
                if (rustedComp == null || rustedComp.enabled == false)
                {
                    gameObject.tag = "Warp";
                }
            }
        }
        
    }
}
