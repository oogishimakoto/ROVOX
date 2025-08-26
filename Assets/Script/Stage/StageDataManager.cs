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

    [Header("ステージデータリスト")]
    [SerializeField] StageClearDataList list;
    int sceneIndexnum = 0;

    bool sceneend = true;

    public StageClearDataList.StageClearData GetStageData() { return list.Stage[sceneIndexnum]; }

    //ムービー用
    public int videoclipnumber { get; set; } = 0;

    /// <summary>
    /// 次のステージを返す
    /// </summary>
    /// <returns>次のステージがない場合ステージ1へ</returns>
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

        //初期化
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
    /// 現在のシーン名を保存
    /// </summary>
    private void NowScene()
    {
        for(int i = 0; i<list.Stage.Count;i++ )
        {
            if(list.Stage[i].name == SceneManager.GetActiveScene().name)
            {
                //Debug.Log(list.Stage[i].name);
                //現在のシーンを保存
                sceneIndexnum = i;
                sceneend = false;

                Debug.Log("シーン名を更新" + i);

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
    /// ステージセレクト画面が終わったら次のシーン名に切り替える
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
