using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SEInfomation : MonoBehaviour
{
    PlayerSEManager Manager;

    [Header("������SE")]
    [SerializeField] List<AudioClip> walkclips= new List<AudioClip>();

    [Header("��̃`���[�W��SE")]
    [SerializeField] List<AudioClip> chargeclips = new List<AudioClip>();

    [Header("��̔��˂�SE")]
    [SerializeField] List<AudioClip> shotclips = new List<AudioClip>();

    [Header("��̉����SE")]
    [SerializeField] List<AudioClip> collectclips = new List<AudioClip>();

    [Header("��_���[�W��SE")]
    [SerializeField] List<AudioClip>�@damageclips = new List<AudioClip>();

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
            //������null
            if(walkclips.Count > 0)
            {
                Manager.walk = walkclips[Random.Range(0, walkclips.Count)]; //����
            }

            //�`���[�W��null
            if (chargeclips.Count > 0)
            {
                Manager.charge = chargeclips[Random.Range(0, chargeclips.Count)];   //�`���[�W
            }

            //�V���b�g��null
            if (shotclips.Count > 0)
            {
                Manager.shot = shotclips[Random.Range(0, shotclips.Count)]; //�󔭎�
            }

            //������null
            if (collectclips.Count > 0)
            {
                Manager.collect = collectclips[Random.Range(0, collectclips.Count)];    //����
            }

            //��_���[�W��null
            if (damageclips.Count > 0)
            {
                Manager.damage = damageclips[Random.Range(0, damageclips.Count)];    //��_���[�W
            }


        }


    }
}
