using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensitivity : MonoBehaviour
{
    Camera camera;
    Mcamera mcamera;

    OptionCursor cursor;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mcamera = camera.GetComponent<Mcamera>();
        cursor = transform.root.GetChild(0).GetComponent<OptionCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSensitivity(cursor.GetCameraValue());
    }

    public void SetSensitivity(float volume)
    {
        mcamera.mouseSensitivityX = volume;
        mcamera.mouseSensitivityY = volume;
      
    }
}
