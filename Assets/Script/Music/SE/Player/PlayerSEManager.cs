using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{

    [Header("中身入れなくて大丈夫です")]
    [SerializeField, Tooltip("歩き")]
    public AudioClip walk;

    [SerializeField, Tooltip("攻撃の溜め")]
    public AudioClip charge;

    [SerializeField, Tooltip("攻撃の発射")]
    public AudioClip shot;

    [SerializeField, Tooltip("武器回収")]
    public AudioClip collect;

    [SerializeField, Tooltip("武器回収")]
    public AudioClip damage;

    //プレイヤーを取得
    private PlayerAction player;

    [SerializeField]
    public AudioSource source;

   // [field: SerializeField, Tooltip("プレイヤーの移動SE")] public List<AudioClip> audioClips = new List<AudioClip>();

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
                //Debug.Log("被ダメージ");
                source.clip = damage;
                source.loop = false;
                source.PlayOneShot(source.clip);
            }
        }
    }
}
