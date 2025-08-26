using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEManager : MonoBehaviour
{
    public static SEManager instance;

    [SerializeField] List<AudioSource> seAudioSource;

    public float sevolume { get; private set; } = 1;

    public void SetAudioSource(AudioSource source)
    {
        if (seAudioSource[0] == null)
        {
            seAudioSource[0] =source ;
        }
        else
        {
            seAudioSource.Add(source);
        }
       
    }

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

    public void SetBGMVolume(float volume)
    {
        for(int i = 0; i < seAudioSource.Count; i++)
        {
            if (seAudioSource[i])
            {
                seAudioSource[i].volume = volume;
            }
        }

        sevolume = volume;
    }

}
