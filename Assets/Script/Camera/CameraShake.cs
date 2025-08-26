using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineFreeLook cinemachineFreeLookCamera;
    private float shakeTimer;

    //Second : Use lerp
    private float startIntensity;


    private void Awake()
    {
        Instance = this;
        //First : Simple Timer
        //Use Instance like this : CameraShake.Instance.ShakeCamera((float)intensity,(float)timer);
        cinemachineFreeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCamera(float intensity, float timer)
    {
        cinemachineFreeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cinemachineFreeLookCamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;

        //First : Simple Timer
        //Shake camera [timer] second
        shakeTimer = timer;

        cinemachineFreeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain= timer;
        cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = timer;
        cinemachineFreeLookCamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = timer;
    }

    public void Update()
    {
        //First : Simple Timer
        //Count Down Shake Timer
        if (shakeTimer > 0.0f)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0.0f)
            {
                //Time Over!
                //Stop Shake
                cinemachineFreeLookCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                cinemachineFreeLookCamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            }
        }
    }

}
