using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] Text text;

    [Header("テキストの表示する基準(昇順)")]
    [SerializeField] List<float> TextCount = new List<float>();

    [Header("表示するテキスト(同上)")]
    [SerializeField] List<string> MessageList = new List<string>();

    //ステージデータ
    StageDataManager manager;
    StageClearDataList.StageClearData datalist;


    // Start is called before the first frame update
    void Start()
    {
        //nullチェック
        if (TextCount.Count <= 0 )
            Debug.Log("TextCount　が設定されてないよ");

        if (MessageList.Count <= 0)
            Debug.Log("TextCount　が設定されてないよ");

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
                //基準値を満たしているか
                if (TextCount[(TextCount.Count - 1)-i] <= (datalist.RestHP + datalist.ScrewBreakCount))
                {
                    text.text = MessageList[(TextCount.Count - 1) - i]; //テキストを決定
                    break; //合計が基準を超える値になったら終了する
                }
            }
        }
    }
}
