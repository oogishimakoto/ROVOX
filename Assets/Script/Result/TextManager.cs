using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] Text text;

    [Header("�e�L�X�g�̕\������(����)")]
    [SerializeField] List<float> TextCount = new List<float>();

    [Header("�\������e�L�X�g(����)")]
    [SerializeField] List<string> MessageList = new List<string>();

    //�X�e�[�W�f�[�^
    StageDataManager manager;
    StageClearDataList.StageClearData datalist;


    // Start is called before the first frame update
    void Start()
    {
        //null�`�F�b�N
        if (TextCount.Count <= 0 )
            Debug.Log("TextCount�@���ݒ肳��ĂȂ���");

        if (MessageList.Count <= 0)
            Debug.Log("TextCount�@���ݒ肳��ĂȂ���");

        manager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(TextCount.Count > 0 && MessageList.Count > 0)
        {
            datalist = manager.GetStageData();

            for(int i = 0;i < TextCount.Count;i++)
            {
                //��l�𖞂����Ă��邩
                if (TextCount[(TextCount.Count - 1)-i] <= (datalist.RestHP + datalist.ScrewBreakCount))
                {
                    text.text = MessageList[(TextCount.Count - 1) - i]; //�e�L�X�g������
                    break; //���v����𒴂���l�ɂȂ�����I������
                }
            }
        }
    }
}
