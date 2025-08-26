using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//2024/05/08 ’Ç‰Á
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class thirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    [Header("CombatCamera")]
    public Transform combatLookAt;

    public GameObject basicCamera;
    public GameObject combatCamera;

    public CameraStyle currentStyle;


    public enum CameraStyle
    {
        Basic,
        Combat,
    }

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

       // basicCamera.gameObject.SetActive(true);
       // combatCamera.gameObject.SetActive(false);
    }


    private void Update()
    {
        CameraFunction();
        CameraInput();
    }

    public void CameraFunction()
    {
        // Get camera's view direction
        // Rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;


        // Rotate player Object
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");    
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        /////////////////////
        // Aiming Camera
        /////////////////////
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 combatviewDir = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = combatviewDir.normalized;

            playerObj.forward = combatviewDir.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {

        if (newStyle == CameraStyle.Basic) basicCamera.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCamera.SetActive(true);

        currentStyle = newStyle;
    }

    private void CameraInput()
    {
        /*
        // Switch Camera style Input
        if (Input.GetKey(Keycode.Alpha1))
        {
            SwitchCameraStyle(CameraStyle.Combat);
        }
        if (Input.GetKey(Keycode Alpha2))
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }
         */

        if (Input.GetAxis("Fire1") == 1 || Input.GetAxis("Aim") == 1)
        {
            basicCamera.gameObject.SetActive(false);
            combatCamera.gameObject.SetActive(true);
            SwitchCameraStyle(CameraStyle.Combat);
        }
        else
        {
            basicCamera.gameObject.SetActive(true);
            combatCamera.gameObject.SetActive(false);
            SwitchCameraStyle(CameraStyle.Basic);
        }
    }
}
