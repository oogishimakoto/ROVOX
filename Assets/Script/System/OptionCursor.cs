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

    [Header("�c�̃J�[�\���p")]
    [SerializeField] private Image cursorselect;

    [Header("�X���C�_�[")]
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider se;
    [SerializeField] private Slider sensitiv;

    [Header("���̃J�[�\���I�����ɑ傫���������")]
    [SerializeField] private Image sidecursorretry;
    [SerializeField] private Image sidecursorselect;

    [Header("�J�[�\���͈�")]
    [SerializeField] private int cursorcount = 4;
    [SerializeField] private int sidecursorcount = 2;

    private SoundActive optioncanvas;
    private Vector2 inputValue;
    // �{�^���̉������
    private bool isPressed;

    [Header("�J�[�\���ʒu")]
    [SerializeField] int cursor = 1;
    [SerializeField] int sidecursor = 1;

    [Header("�傫������T�C�Y")]
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
        //null�`�F�b�N
        if (!optioncanvas) return;
        //�������Ƃ��ȊO ���@�I�v�V������ʂ��J����Ă���Ƃ��ȊO�@�I��
        if (!context.performed || !optioncanvas.option) return;

        // �X�e�B�b�N��2�����͎擾
        Vector2 inputValue = context.ReadValue<Vector2>();

        //inputValue.y�̒l����+-����
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
        //null�`�F�b�N
        if (!optioncanvas) return;
        //�������Ƃ��ȊO ���@�I�v�V������ʂ��J����Ă���Ƃ��ȊO�@�I��
        if (!context.performed || !optioncanvas.option) return;

        // �X�e�B�b�N��2�����͎擾
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
        //null�`�F�b�N
        if (!optioncanvas) return;
        //�I�v�V������ʂ��J����Ă���Ƃ��ȊO�@�I��
        if (!optioncanvas.option) return;
        // �X�e�B�b�N��2�����͎擾
        inputValue = context.ReadValue<Vector2>();

        switch (context.phase)
        {
            case InputActionPhase.Started:
                isPressed = true;
                cursorselect.gameObject.SetActive(true);
                break;

            case InputActionPhase.Canceled:
                // �{�^���������ꂽ���̏���
                isPressed = false;
                break;
        }

        

    }

    /// <summary>
    /// �I������Ă���J�[�\���ʒu�̃X���C�_�[��ύX
    /// </summary>
    private void Cursor()
    {
        //
        if (cursor == 1)
        {
            //���ʂ������ɂ��đ��
            bgm.value += inputValue.x / sensitiv.maxValue;
            Vector3 vec = bgm.transform.position;
            vec.x = cursorselect.transform.position.x;
            cursorselect.transform.position = vec;
        }

        if (cursor == 2)
        {
            //���ʂ������ɂ��đ��
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
        //�J�[�\����4�ԖڂȂ牡�ɂ��ړ��ł���
        if(cursor == 4)
        {
            //���g���C
            if (sidecursor == 1)
            {
                sidecursorselect.transform.localScale = scaleBU;
                sidecursorretry.transform.localScale = scale;

            }

            //�X�e�[�W�I��
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
        //null�`�F�b�N
        if (!optioncanvas) return;
        //�������Ƃ��ȊO ���@�I�v�V������ʂ��J����Ă���Ƃ��ȊO�@�I��
        if (!context.performed || !optioncanvas.option) return;

        if (cursor == 4)
        {
            //���g���C
            if (sidecursor == 1)
            {
                sidecursorretry.transform.parent.GetChild(2).GetComponent<SceneChanger>().OptionSceneChage();

            }

            //�X�e�[�W�I��
            if (sidecursor == 2)
            {
                sidecursorselect.transform.parent.GetChild(2).GetComponent<SceneChanger>().OptionSelectSceneChage();
            }
        }
    }

}
