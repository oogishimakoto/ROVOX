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
        Debug.Log("�A�j���[�V�����Đ�");
        anim.speed = 1;
    }
}
