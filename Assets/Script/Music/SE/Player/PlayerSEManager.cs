using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{

    [Header("���g����Ȃ��đ��v�ł�")]
    [SerializeField, Tooltip("����")]
    public AudioClip walk;

    [SerializeField, Tooltip("�U���̗���")]
    public AudioClip charge;

    [SerializeField, Tooltip("�U���̔���")]
    public AudioClip shot;

    [SerializeField, Tooltip("������")]
    public AudioClip collect;

    [SerializeField, Tooltip("������")]
    public AudioClip damage;

    //�v���C���[���擾
    private PlayerAction player;

    [SerializeField]
    public AudioSource source;

   // [field: SerializeField, Tooltip("�v���C���[�̈ړ�SE")] public List<AudioClip> audioClips = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        player  = gameObject.transform.parent.GetComponent<PlayerAction>();
        source = GetComponent<AudioSource>();
        GameObject.Find("SEManager").GetComponent<SEManager>().SetAudioSource(source);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && source != null && 
            !source.isPlaying)
        {
            if (player.GetPlayerState() == PlayerAction.State.RUN && walk != null)
            {
                source.clip = walk;
                source.loop = false;
                source.PlayOneShot(source.clip);
            }

            if (player.GetPlayerState() == PlayerAction.State.CHARGE && charge != null)
            {
                source.clip = charge;
                source.loop= false;
                source.PlayOneShot(source.clip);
            }

            if (player.GetPlayerState() == PlayerAction.State.THROW && shot != null)
            {
                source.clip = shot;
                source.loop = false;
                source.PlayOneShot(source.clip);
            }

            if (player.GetPlayerState() == PlayerAction.State.DAMAGE && shot != null)
            {
                //Debug.Log("��_���[�W");
                source.clip = damage;
                source.loop = false;
                source.PlayOneShot(source.clip);
            }
        }
    }
}
