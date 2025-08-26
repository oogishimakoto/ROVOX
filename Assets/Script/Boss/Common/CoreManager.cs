using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreManager : MonoBehaviour
{

    private int coreNum;    //�j�󂳂�Ă��Ȃ��R�A�̐�

    [Header("�R�A�̐����Ƃ����������������(�v�f�O�����ׂĔj�󂳂ꂽ�Ƃ�)"),SerializeField]
    float[] pullTime;
    [Header("�R�A�̐����Ƃ̃_���[�W�{��"), SerializeField]
    int[] damageRate;

    [Header("�S����core�̃Q�[���I�u�W�F�N�g")]
    [SerializeField] GameObject MainCore;
    [SerializeField] float rotationSpeed = 50.0f; // ��]���x (�x/�b)
    [SerializeField] float totalRotation = 60.0f; // ����]�� (�x)

    [Header("�R�A��UI�Q�[���I�u�W�F�N�g")]
    [SerializeField] GameObject CoreUI;

    [Header("�R�A�̃X�R�A�f�[�^")]
    [SerializeField] StageDataManager StageData;

    private float currentRotation = 0.0f; // ���݂̉�]�� (�x)

    int BreakCoreNum = 0;

    bool IsRotation;

    private void Start()
    {
        StageData = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
    }

    private void Update()
    {
        if (IsRotation)
        {

            float rotationAmount = rotationSpeed * Time.deltaTime;
            MainCore.transform.Rotate(0,0 , rotationAmount);
            currentRotation += rotationAmount;
            if (currentRotation >= totalRotation)
            {
                IsRotation = false;
            }
        }
    }

    //�R�A�̐��𑝂₷
    public void AddCoreNum()
    {
        coreNum += 1;
    }
    //�R�A�̐��𑝂₷
    public void SubCoreNum()
    {
        coreNum -= 1;
        CoreUI.transform.GetChild(BreakCoreNum).GetComponent<Image>().color = Color.white;
        BreakCoreNum += 1;
        IsRotation = true;
        currentRotation = 0.0f;
    }

    //�c���Ă���R�A�̐��ɉ�������������������Ԃ�Ԃ�
    public float GetPullTime()
    {
        return pullTime[coreNum];
    }

    public int GetDamageRate()
    {
        return damageRate[coreNum];
    }

    public void SetScoreCoreNum()
    {
        StageData.GetStageData().ScrewBreakCount = 5-coreNum;
    }
}
