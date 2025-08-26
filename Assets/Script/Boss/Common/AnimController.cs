using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimStop()
    {
        anim.speed = 0;
    }

    public void AnimStart()
    {
        Debug.Log("アニメーション再生");
        anim.speed = 1;
    }
}
