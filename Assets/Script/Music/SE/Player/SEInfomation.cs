using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SEInfomation : MonoBehaviour
{
    PlayerSEManager Manager;

    [Header("歩きのSE")]
    [SerializeField] List<AudioClip> walkclips= new List<AudioClip>();

    [Header("銛のチャージのSE")]
    [SerializeField] List<AudioClip> chargeclips = new List<AudioClip>();

    [Header("銛の発射のSE")]
    [SerializeField] List<AudioClip> shotclips = new List<AudioClip>();

    [Header("銛の回収のSE")]
    [SerializeField] List<AudioClip> collectclips = new List<AudioClip>();

    [Header("被ダメージのSE")]
    [SerializeField] List<AudioClip>　damageclips = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        Manager = GetComponent<PlayerSEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager != null &&
            !Manager.source.isPlaying)
        {
            //歩きのnull
            if(walkclips.Count > 0)
            {
                Manager.walk = walkclips[Random.Range(0, walkclips.Count)]; //歩き
            }

            //チャージのnull
            if (chargeclips.Count > 0)
            {
                Manager.charge = chargeclips[Random.Range(0, chargeclips.Count)];   //チャージ
            }

            //ショットのnull
            if (shotclips.Count > 0)
            {
                Manager.shot = shotclips[Random.Range(0, shotclips.Count)]; //銛発射
            }

            //銛回収のnull
            if (collectclips.Count > 0)
            {
                Manager.collect = collectclips[Random.Range(0, collectclips.Count)];    //銛回収
            }

            //被ダメージのnull
            if (damageclips.Count > 0)
            {
                Manager.damage = damageclips[Random.Range(0, damageclips.Count)];    //被ダメージ
            }


        }


    }
}
