using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerAction;

public class OptionCursor : MonoBehaviour
{
    OptionManager optionManager;

    [Header("縦のカーソル用")]
    [SerializeField] private Image cursorselect;

    [Header("スライダー")]
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider se;
    [SerializeField] private Slider sensitiv;

    [Header("横のカーソル選択時に大きくするもの")]
    [SerializeField] private Image sidecursorretry;
    [SerializeField] private Image sidecursorselect;

    [Header("カーソル範囲")]
    [SerializeField] private int cursorcount = 4;
    [SerializeField] private int sidecursorcount = 2;

    private SoundActive optioncanvas;
    private Vector2 inputValue;
    // ボタンの押下状態
    private bool isPressed;

    [Header("カーソル位置")]
    [SerializeField] int cursor = 1;
    [SerializeField] int sidecursor = 1;

    [Header("大きくするサイズ")]
    [SerializeField] Vector3 scale= new Vector3(0.25f,0.15f,0.25f);
    Vector3 scaleBU = new Vector3(0.23f, 0.13f, 0.23f);

    public float GetBgmValue() { return bgm.value; }
    public void SetBgmValue(float value) { bgm.value = value; }

    public float GetSeValue() { return se.value; }
    public void SetSeValue(float value) { se.value = value; }

    public float GetCameraValue() { return sensitiv.value; }
    public void SetCameraValue(float value) { sensitiv.value = value; }


    // Start is called before the first frame update
    void Start()
    {
        optionManager = GameObject.Find("OptionManager").GetComponent<OptionManager>();

        optioncanvas = transform.parent.GetComponent<SoundActive>();
        scale = new Vector3(0.25f, 0.15f, 0.25f);
        scaleBU = new Vector3(0.23f, 0.13f, 0.23f);

        bgm.value = optionManager.BGMValue;
        se.value = optionManager.SEValue;

        //sensitiv.value = optionManager.CameraValue;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title1.02")
        {
            sidecursorretry.transform.parent.gameObject.SetActive(false);
            sidecursorselect.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            sidecursorretry.transform.parent.gameObject.SetActive(true);
            sidecursorselect.transform.parent.gameObject.SetActive(true);
        }

        if (isPressed)
        {
            Cursor();
        }

        SideCursor();
    }

    public void CursorUp(InputAction.CallbackContext context)
    {
        //nullチェック
        if (!optioncanvas) return;
        //押したとき以外 ＆　オプション画面が開かれているとき以外　終了
        if (!context.performed || !optioncanvas.option) return;

        // スティックの2軸入力取得
        Vector2 inputValue = context.ReadValue<Vector2>();

        //inputValue.yの値から+-する
        if (inputValue.y > 0.0f)
        {
            cursor--;

            if (cursor <= 0)
            {
               
                cursor = cursorcount;
            }
        }
        else if (inputValue.y < 0.0f)
        {
            cursor++;
            
            if (cursor > cursorcount)
            {
                cursor = 1;
            }

        }


    }

    public void CursorSide(InputAction.CallbackContext context)
    {
        //nullチェック
        if (!optioncanvas) return;
        //押したとき以外 ＆　オプション画面が開かれているとき以外　終了
        if (!context.performed || !optioncanvas.option) return;

        // スティックの2軸入力取得
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (cursor == 4)
        {
            if (inputValue.x > 0.0f)
            {
                sidecursor--;

                if (sidecursor <= 0)
                {

                    sidecursor = sidecursorcount;
                }
            }
            else if (inputValue.x < 0.0f)
            {
                sidecursor++;

                if (sidecursor > sidecursorcount)
                {
                    sidecursor = 1;
                }

            }
        }
    }

    public void Slider(InputAction.CallbackContext context)
    {
        //nullチェック
        if (!optioncanvas) return;
        //オプション画面が開かれているとき以外　終了
        if (!optioncanvas.option) return;
        // スティックの2軸入力取得
        inputValue = context.ReadValue<Vector2>();

        switch (context.phase)
        {
            case InputActionPhase.Started:
                isPressed = true;
                cursorselect.gameObject.SetActive(true);
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                isPressed = false;
                break;
        }

        

    }

    /// <summary>
    /// 選択されているカーソル位置のスライダーを変更
    /// </summary>
    private void Cursor()
    {
        //
        if (cursor == 1)
        {
            //音量を少数にして代入
            bgm.value += inputValue.x / sensitiv.maxValue;
            Vector3 vec = bgm.transform.position;
            vec.x = cursorselect.transform.position.x;
            cursorselect.transform.position = vec;
        }

        if (cursor == 2)
        {
            //音量を少数にして代入
            se.value += inputValue.x / sensitiv.maxValue;
            Vector3 vec = se.transform.position;
            vec.x = cursorselect.transform.position.x;
            cursorselect.transform.position = vec;
        }

        if (cursor == 3)
        {
            sensitiv.value += inputValue.x;
            Vector3 vec = sensitiv.transform.position;
            vec.x = cursorselect.transform.position.x;
            cursorselect.transform.position = vec;
        }

        if (cursor == 4)
        {
            cursorselect.gameObject.SetActive(false);
        }
    }

    private void SideCursor()
    {
        //カーソルが4番目なら横にも移動できる
        if(cursor == 4)
        {
            //リトライ
            if (sidecursor == 1)
            {
                sidecursorselect.transform.localScale = scaleBU;
                sidecursorretry.transform.localScale = scale;

            }

            //ステージ選択
            if (sidecursor == 2)
            {
                sidecursorretry.transform.localScale = scaleBU;
                sidecursorselect.transform.localScale = scale;
            }
            
        }
        else
        {
            sidecursorretry.transform.localScale = scaleBU;
            sidecursorselect.transform.localScale = scaleBU;
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        //nullチェック
        if (!optioncanvas) return;
        //押したとき以外 ＆　オプション画面が開かれているとき以外　終了
        if (!context.performed || !optioncanvas.option) return;

        if (cursor == 4)
        {
            //リトライ
            if (sidecursor == 1)
            {
                sidecursorretry.transform.parent.GetChild(2).GetComponent<SceneChanger>().OptionSceneChage();

            }

            //ステージ選択
            if (sidecursor == 2)
            {
                sidecursorselect.transform.parent.GetChild(2).GetComponent<SceneChanger>().OptionSelectSceneChage();
            }
        }
    }

}
