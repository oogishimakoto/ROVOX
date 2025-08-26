using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectCursor : MonoBehaviour
{
    [Header("カーソル対象")]
    [SerializeField] List<GameObject> CursorObj = new List<GameObject>();
    [SerializeField] float scale ;

    [Header("カーソル対象")]
    [SerializeField] List<Image> CursorImage = new List<Image>();
    [SerializeField] Image image;

    List<Vector3> CursorObjScale = new List<Vector3>();

    int cursorMax = 1;
   public int cursor = 3;    


    private SceneChanger changer;

    private SoundActive option;


    // Start is called before the first frame update
    void Start()
    {

        cursorMax = CursorObj.Count;
        for(int i = 0;i < CursorObj.Count;i++)
        {
            CursorObjScale.Add(CursorObj[i].transform.localScale);
        }
        changer = GetComponent<SceneChanger>();
        option = GameObject.Find("OptionCanvas").GetComponent<SoundActive>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(CursorObj != null)
        {
            if (CursorObj.Count != 0 && !option.transform.GetChild(0).gameObject.activeSelf)
            {
                KeyBoardCursor();

                CursorNumber();

            }

            //決定
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //カーソルにあった行動をする
                SelectCursorNumber();
            }
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

    public void Cursor(InputAction.CallbackContext context)
    {

        //押したとき以外終了
        if (!context.performed || option.transform.GetChild(0).gameObject.activeSelf) return;


        // スティックの2軸入力取得
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (inputValue.y > 0.0f)
        {

            cursor--;

            if (cursor <= 0)
            {

                cursor = cursorMax;
            }
        }
        else if (inputValue.y < 0.0f)
        {


            cursor++;
            if (cursor > cursorMax)
            {
                cursor = 1;
            }

        }
    }

    public void KeyBoardCursor()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            cursor++;
            if (cursor > cursorMax)
            {
                cursor = 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            cursor--;

            if (cursor <= 0)
            {

                cursor = cursorMax;
            }

        }
    }

    public void CursorNumber()
    {
        //オプション
        if (cursor == 1)
        {
            //オブジェクトを大きくする
            Vector3 scele = CursorObj[cursor - 1].transform.localScale;
            if((CursorObj[cursor - 1].transform.localScale - CursorObjScale[0]).magnitude < scale)
                scele += new Vector3(scale, scale, scale);
            CursorObj[cursor - 1].transform.localScale = scele;

            //文字も連動させる
            image.transform.position =  CursorImage[cursor - 1].transform.position;
        }
        else
        {
            CursorObj[0].transform.localScale = CursorObjScale[0];
        }

        //ゲーム修了
        if (cursor == 2)
        {
            Vector3 scele = CursorObj[cursor - 1].transform.localScale;
            if ((CursorObj[cursor - 1].transform.localScale - CursorObjScale[0]).magnitude < scale)
                scele += new Vector3(scale, scale, scale);
            CursorObj[cursor - 1].transform.localScale = scele;

            //文字も連動させる
            image.transform.position = CursorImage[cursor - 1].transform.position;

        }
        else
        {
            CursorObj[1].transform.localScale = CursorObjScale[1];
        }

        //看板
        if (cursor == 3)
        {
            Vector3 scele = CursorObj[cursor - 1].transform.localScale;
            if ((CursorObj[cursor - 1].transform.localScale - CursorObjScale[0]).magnitude < scale)
                scele += new Vector3(scale, scale, scale);
            CursorObj[cursor -1].transform.localScale = scele;

            //文字も連動させる
            image.transform.position = CursorImage[cursor - 1].transform.position;

        }
        else
        {
            CursorObj[2].transform.localScale = CursorObjScale[2];
        }
      

    }

    public void SelectCursorNumber()
    {
      

        //オプション
        if (cursor == 1)
        {
            option.ChangeOption();
        }

        //ゲーム修了
        if (cursor == 2)
        {
            transform.LookAt(CursorObj[cursor - 1].transform.position);
            PlayerEnd();
        }

        //看板
        if (cursor == 3)
        {
            transform.LookAt(CursorObj[cursor - 1].transform.position);
            changer.SceneChage();
        }
       

    }

    public void Select(InputAction.CallbackContext context)
    {

        //押したとき以外終了
        if (!context.performed) return;

        SelectCursorNumber();
    }
}
