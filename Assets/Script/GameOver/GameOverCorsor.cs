using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverCorsor : MonoBehaviour
{
    [Header("�J�[�\���Ώ�")]
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

            //����
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //�J�[�\���ɂ������s��������
                SelectCursorNumber();
            }
        }
    }

    public void Cursor(InputAction.CallbackContext context)
    {

        //�������Ƃ��ȊO�I��
        if (!context.performed) return;


        // �X�e�B�b�N��2�����͎擾
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
        //�X�e�[�W�Z���N�g
        if (cursor == 1)
        {
            //�I�u�W�F�N�g��傫������
            Vector3 scele = CursorObj[cursor - 1].transform.localScale;
            if ((CursorObj[cursor - 1].transform.localScale - CursorObjScale[0]).magnitude < scale)
                scele += new Vector3(scale, scale, scale);
            CursorObj[cursor - 1].transform.localScale = scele;
        }
        else
        {
            CursorObj[0].transform.localScale = CursorObjScale[0];
        }

        //���̃X�e�[�W��
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
        //�X�e�[�W�Z���N�g
        if (cursor == 1)
        {
            FadeManager.Instance.LoadScene("StageSelect", 1);
        }

        //���̃X�e�[�W��
        if (cursor == 2)
        {
            //���̃X�e�[�W�̖��O���擾���đJ��
            //���̃X�e�[�W���Ȃ��ꍇ���݂̃X�e�[�W���J��Ԃ�
            FadeManager.Instance.LoadScene(stagedatalist.GetStageData().name, 1);
        }

    }
    public void Select(InputAction.CallbackContext context)
    {

        //�������Ƃ��ȊO�I��
        if (!context.performed) return;

        SelectCursorNumber();
    }
}
