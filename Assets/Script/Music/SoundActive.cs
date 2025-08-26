using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundActive : MonoBehaviour
{
    public bool option;
    private bool isCursorLocked = true; // カーソルがロックされているかどうか

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "StageSelect")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeOption();

            }

        }
        transform.GetChild(0).gameObject.SetActive(option);
    }

    public void ChangeOption()
    {
        option= !option;
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
        }

        isCursorLocked = !isCursorLocked; // カーソルのロック状態をトグル
        Cursor.visible = !isCursorLocked;
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
