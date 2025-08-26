using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;
using static PlayerAction;

public class TitleCursor : MonoBehaviour
{
    private Image start;
    private Image option;
    private Image end;

    private Canvas optioncanvas;

    int cursor = 0;
    float Distans = -159;

    // Start is called before the first frame update
    void Start()
    {
        Image back = transform.parent.GetComponent<Image>();
        for(int i = 0; i < back.transform.childCount; i++)
        {
            if(back.transform.GetChild(i).name == "start")
                start = back.transform.GetChild(i).GetComponent<Image>();

            if (back.transform.GetChild(i).name == "option")
                option = back.transform.GetChild(i).GetComponent<Image>();

            if (back.transform.GetChild(i).name == "end")
                end = back.transform.GetChild(i).GetComponent<Image>();
        }

        optioncanvas = GameObject.Find("SoundCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) 
        {
            Decision();

        }

        //カーソル位置を更新
        Vector3 pos = GetComponent<RectTransform>().transform.position;
        pos.y = start.rectTransform.position.y + Distans * cursor;
        GetComponent<RectTransform>().transform.position = pos;
    }

    public void CursorUp(InputAction.CallbackContext context)
    {

        //押したとき以外 ＆　オプション画面が開かれているとき　終了
        if (!context.performed || optioncanvas.GetComponent<SoundActive>().option ) return;

        // スティックの2軸入力取得
        Vector2 inputValue = context.ReadValue<Vector2>();

        if (inputValue.y > 0.0f)
        {
            cursor--;
            if (cursor < 0)
            {
                cursor = 2;
            }
        }
        else if (inputValue.y < 0.0f)
        {
            cursor++;
            if (cursor > 2)
            {
                cursor = 0;
            }
 
        }
    }

    public void DecisionCursor(InputAction.CallbackContext context)
    {
        //押したとき以外終了
        if (!context.performed) return;

        Decision();
    }

    private void Decision()
    {
        if (cursor == 0)
        {
            GetComponent<SceneChanger>().SceneChage();
        }

        if (cursor == 1)
        {
            optioncanvas.GetComponent<SoundActive>().option = !optioncanvas.GetComponent<SoundActive>().option;
        }

        if (cursor == 2)
        {
            PlayerEnd();
        }
    }

    public void PlayerEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
