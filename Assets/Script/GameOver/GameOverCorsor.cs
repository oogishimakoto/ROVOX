using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverCorsor : MonoBehaviour
{
    [Header("カーソル対象")]
    [SerializeField] List<GameObject> CursorObj = new List<GameObject>();
    [SerializeField] float scale;

    List<Vector3> CursorObjScale = new List<Vector3>();

    int cursorMax = 1;
    [SerializeField] int cursor = 2;

    StageDataManager stagedatalist;

    // Start is called before the first frame update
    void Start()
    {
        stagedatalist = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();

        cursorMax = CursorObj.Count;
        for (int i = 0; i < CursorObj.Count; i++)
        {
            CursorObjScale.Add(CursorObj[i].transform.localScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorObj != null)
        {
            if (CursorObj.Count != 0)
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

    public void Cursor(InputAction.CallbackContext context)
    {

        //押したとき以外終了
        if (!context.performed) return;


        // スティックの2軸入力取得
        Vector2 inputValue = context.ReadValue<Vector2>();
        if (inputValue.x > 0.0f)
        {

            cursor--;

            if (cursor <= 0)
            {

                cursor = cursorMax;
            }
        }
        else if (inputValue.x < 0.0f)
        {


            cursor++;
            if (cursor > cursorMax)
            {
                cursor = 1;
            }

        }
    }
    public void CursorNumber()
    {
        //ステージセレクト
        if (cursor == 1)
        {
            //オブジェクトを大きくする
            Vector3 scele = CursorObj[cursor - 1].transform.localScale;
            if ((CursorObj[cursor - 1].transform.localScale - CursorObjScale[0]).magnitude < scale)
                scele += new Vector3(scale, scale, scale);
            CursorObj[cursor - 1].transform.localScale = scele;
        }
        else
        {
            CursorObj[0].transform.localScale = CursorObjScale[0];
        }

        //次のステージへ
        if (cursor == 2)
        {
            Vector3 scele = CursorObj[cursor - 1].transform.localScale;
            if ((CursorObj[cursor - 1].transform.localScale - CursorObjScale[0]).magnitude < scale)
                scele += new Vector3(scale, scale, scale);
            CursorObj[cursor - 1].transform.localScale = scele;

        }
        else
        {
            CursorObj[1].transform.localScale = CursorObjScale[1];
        }


    }

    public void KeyBoardCursor()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            cursor++;
            if (cursor > cursorMax)
            {
                cursor = 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            cursor--;

            if (cursor <= 0)
            {

                cursor = cursorMax;
            }

        }
    }

    public void SelectCursorNumber()
    {
        //ステージセレクト
        if (cursor == 1)
        {
            FadeManager.Instance.LoadScene("StageSelect", 1);
        }

        //次のステージへ
        if (cursor == 2)
        {
            //次のステージの名前を取得して遷移
            //次のステージがない場合現在のステージを繰り返す
            FadeManager.Instance.LoadScene(stagedatalist.GetStageData().name, 1);
        }

    }
    public void Select(InputAction.CallbackContext context)
    {

        //押したとき以外終了
        if (!context.performed) return;

        SelectCursorNumber();
    }
}
