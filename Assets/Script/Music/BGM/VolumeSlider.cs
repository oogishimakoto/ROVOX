using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public void SetBGMVolume(float volume)
    {
        BGMManager.instance.SetBGMVolume(volume);
    }

    public void SetSEVolume(float volume)
    {
        SEManager.instance.SetBGMVolume(volume);
    }
}
