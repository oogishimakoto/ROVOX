using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [SerializeField] AudioSource bgmAudioSource;

    public float bgmvolume { get; private set; } = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        bgmAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(bgmAudioSource == null && GameObject.Find("Main Camera").GetComponent<AudioSource>())
        {
            bgmAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            bgmAudioSource.volume = bgmvolume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
        bgmvolume = volume;
    }
}
