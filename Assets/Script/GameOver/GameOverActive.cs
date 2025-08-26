using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverActive : MonoBehaviour
{
    public bool gameover;
    private bool isCursorLocked = true; // カーソルがロックされているかどうか

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeOption();

        }
        transform.GetChild(0).gameObject.SetActive(gameover);
    }

    public void ChangeOption()
    {
        gameover = !gameover;
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
