using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    #region Singleton

    private static OptionManager instance;

    public static OptionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (OptionManager)FindObjectOfType(typeof(OptionManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(OptionManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    SoundActive option;
    OptionCursor cursor;

    [field:SerializeField] public float BGMValue { get; private set; } = 0.5f;
    [field: SerializeField] public float SEValue { get; private set; } = 0.5f;
    [field: SerializeField] public float CameraValue { get; private set; } = 105.0f;

    string nawscene;

    // Start is called before the first frame update
    void Start()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        //シーンを跨げるようにする
        DontDestroyOnLoad(this.gameObject);

        //初期設定
        Init();

        //シーン遷移ごとに初期値を入れるためにシーン名を取得
        nawscene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if(nawscene != SceneManager.GetActiveScene().name)
        {
            Init();
            nawscene = SceneManager.GetActiveScene().name;
        }
        else
        {
            BGMValue = cursor.GetBgmValue();
            SEValue=cursor.GetSeValue();
            CameraValue=cursor.GetCameraValue();
        }
    }

    private void Init()
    {

        if(SceneManager.GetActiveScene().name != "StageSelect" && SceneManager.GetActiveScene().name != "Result")
        {
            //オプション用のキャンバスデータを取得
            GameObject optionobj = GameObject.Find("OptionCanvas");
            option = optionobj.GetComponent<SoundActive>();
            cursor = optionobj.transform.GetChild(0).GetComponent<OptionCursor>();

            cursor.SetBgmValue(BGMValue);
            cursor.SetSeValue(SEValue);
            cursor.SetCameraValue(CameraValue);

        }

    }
}
