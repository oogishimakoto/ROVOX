using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelectMove : MonoBehaviour
{
    [SerializeField] Vector3 rotationAxis = Vector3.up;
    [SerializeField] float MoveTime = 5.0f;
    [SerializeField] float moveStep;

    [Header("UI�̍���")]
    [SerializeField] float SelectUIHeight;
    [SerializeField] float CommonUIHeight;
    [Header("UI�̃X�P�[��")]
    [SerializeField] float SelectUIScale;
    [SerializeField] float CommonUIScale;

    float moveCount = 0.0f;
    public int StageNum = 0;
    private int previousStageNum = 0;

    [SerializeField] StageDataList stagedata;

    StageDataManager dataManager;

    enum RotateStete
    {
        STOP = 0,
        LEFT = 1,
        RIGHT = -1,
    }
    RotateStete state = RotateStete.STOP;

    private void Start()
    {
        dataManager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        int objectCount = transform.childCount;
    }

    void Update()
    {
        int sign = (int)state;
        float move = sign * moveStep * Time.deltaTime / MoveTime;

        // �I�u�W�F�N�g�Ɉړ���K�p
        Vector3 posBuf = transform.position;
        posBuf.x += move;
        transform.position = posBuf;

        // �ړ��i�s�x�̌v�Z
        moveCount += Mathf.Abs(move);
        float progress = Mathf.Clamp01(moveCount / moveStep);
        float smoothProgress = Mathf.SmoothStep(0, 1, progress);

        // ���X�ɃI�u�W�F�N�g�̃X�P�[���ƍ�����ω�������
        Transform currentStage = transform.GetChild(StageNum);
        Transform previousStage = transform.GetChild(previousStageNum);

        currentStage.localScale = Vector3.Lerp(new Vector3(CommonUIScale, CommonUIScale, CommonUIScale), new Vector3(SelectUIScale, SelectUIScale, SelectUIScale), smoothProgress);
        currentStage.localPosition = Vector3.Lerp(new Vector3(currentStage.localPosition.x, CommonUIHeight, currentStage.localPosition.z), new Vector3(currentStage.localPosition.x, SelectUIHeight, currentStage.localPosition.z), smoothProgress);

        previousStage.localScale = Vector3.Lerp(new Vector3(SelectUIScale, SelectUIScale, SelectUIScale), new Vector3(CommonUIScale, CommonUIScale, CommonUIScale), smoothProgress);
        previousStage.localPosition = Vector3.Lerp(new Vector3(previousStage.localPosition.x, SelectUIHeight, previousStage.localPosition.z), new Vector3(previousStage.localPosition.x, CommonUIHeight, previousStage.localPosition.z), smoothProgress);

        if (Mathf.Abs(moveCount) >= moveStep)
        {
            Vector3 pos = transform.position;
            pos.x = -moveStep * StageNum;
            transform.position = pos;
            state = RotateStete.STOP;
            moveCount = 0.0f;

            // �������ɐ��m�ȃT�C�Y�ƍ�����ݒ�
            currentStage.localScale = new Vector3(SelectUIScale, SelectUIScale, SelectUIScale);
            currentStage.localPosition = new Vector3(currentStage.localPosition.x, SelectUIHeight, currentStage.localPosition.z);

            previousStage.localScale = new Vector3(CommonUIScale, CommonUIScale, CommonUIScale);
            previousStage.localPosition = new Vector3(previousStage.localPosition.x, CommonUIHeight, previousStage.localPosition.z);

            previousStageNum = StageNum;
        }
    }

    //�Q�[���p�b�h��L�X�e�B�b�N�ŃX�e�[�W��]
    public void LStickRotate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (state != RotateStete.STOP)
        {
            return;
        }
        Vector2 inputValue = context.ReadValue<Vector2>();

        if (inputValue.x > 0.0f)
        {
            RightRotate();
        }
        else if (inputValue.x < 0.0f)
        {
            LeftRotate();
        }
    }

    //�L�[�{�[�h�ŉE��]
    public void OnRightRotate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (state != RotateStete.STOP)
        {
            return;
        }

        RightRotate();
    }

    //�L�[�{�[�h�ō���]
    public void OnLeftRotate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (state != RotateStete.STOP)
        {
            return;
        }

        LeftRotate();
    }

    //�X�e�[�W�̉E��]
    public void RightRotate()
    {
        if (StageNum < stagedata.List.Count - 1)
        {
            state = RotateStete.RIGHT;
            moveCount = 0.0f;
            previousStageNum = StageNum;
            StageNum++;
        }
    }

    //�X�e�[�W�̍���]
    void LeftRotate()
    {
        if (StageNum > 0)
        {
            state = RotateStete.LEFT;
            moveCount = 0.0f;
            previousStageNum = StageNum;
            StageNum--;
        }
    }

    //�X�e�[�W����
    public void OnStageDecision(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (state != RotateStete.STOP)
        {
            return;
        }

        if(StageNum - 1 == 3)
            dataManager.videoclipnumber = 0;
        if (StageNum - 1 == 5)
            dataManager.videoclipnumber = 1;
        if (StageNum - 1 == 7)
            dataManager.videoclipnumber = 2;
        if (StageNum - 1 == 9)
            dataManager.videoclipnumber = 3;
        string SceneName = stagedata.List[StageNum].LinkSceneName;
        FadeManager.Instance.LoadScene(SceneName, 1);
    }
}
