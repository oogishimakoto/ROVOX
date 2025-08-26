using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDataManager : MonoBehaviour
{
    #region Singleton

    private static StageDataManager instance;

    public static StageDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (StageDataManager)FindObjectOfType(typeof(StageDataManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(StageDataManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    [Header("�X�e�[�W�f�[�^���X�g")]
    [SerializeField] StageClearDataList list;
    int sceneIndexnum = 0;

    bool sceneend = true;

    public StageClearDataList.StageClearData GetStageData() { return list.Stage[sceneIndexnum]; }

    //���[�r�[�p
    public int videoclipnumber { get; set; } = 0;

    /// <summary>
    /// ���̃X�e�[�W��Ԃ�
    /// </summary>
    /// <returns>���̃X�e�[�W���Ȃ��ꍇ�X�e�[�W1��</returns>
    public StageClearDataList.StageClearData GetNextStageData() 
    { 
        if(sceneIndexnum + 1 < 14)
        {
            return list.Stage[sceneIndexnum + 1];

        }
        else
        {
            //sceneIndexnum = 0;
            return list.Stage[0];
        }
    }


    private void Start()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }


        DontDestroyOnLoad(this.gameObject);

        //������
        for (int i = 0; i < list.Stage.Count; i++)
        {
            list.Stage[i].ScrewBreakCount = 0;
            list.Stage[i].RestHP = 0;

        }
    }

    private void Update()
    {
        SelectSceneChange();

        
    }
    /// <summary>
    /// ���݂̃V�[������ۑ�
    /// </summary>
    private void NowScene()
    {
        for(int i = 0; i<list.Stage.Count;i++ )
        {
            if(list.Stage[i].name == SceneManager.GetActiveScene().name)
            {
                //Debug.Log(list.Stage[i].name);
                //���݂̃V�[����ۑ�
                sceneIndexnum = i;
                sceneend = false;

                Debug.Log("�V�[�������X�V" + i);

                if (i == 1)
                    videoclipnumber = 0;
                if (i == 3)
                    videoclipnumber = 1;
                if (i == 5)
                    videoclipnumber = 2;
                if (i == 7)
                    videoclipnumber = 3;
            }
        }
    }

    /// <summary>
    /// �X�e�[�W�Z���N�g��ʂ��I������玟�̃V�[�����ɐ؂�ւ���
    /// </summary>
    public void SelectSceneChange()
    {
        //if (SceneManager.GetActiveScene().name == "StageSelect")
        //{
        //    sceneend = true;
        //}
        if (SceneManager.GetActiveScene().name != "StageSelect" && 
            SceneManager.GetActiveScene().name != "Result" && 
            SceneManager.GetActiveScene().name != "GameOver")
        {
            if (SceneManager.GetActiveScene().name != list.Stage[sceneIndexnum].name)
                NowScene();
        }
    }
}
