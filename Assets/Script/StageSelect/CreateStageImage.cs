using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStageImage : MonoBehaviour
{
    //ステージデータのリスト
    [SerializeField] StageDataList　StageList;


     [SerializeField] float moveStep = 5f;

    // 角度のオフセット

    //ステージUIのオフセット
    [SerializeField] float SelectUIHeight = 4.66f;
    [SerializeField] float CommonUIHeight = 2.88f;

    [Header("選択していないUIのスケール")]
    [SerializeField] float CommonUIScale;

    [SerializeField] GameObject PaperUI;
    [SerializeField] GameObject StageTextUI;

    // Start is called before the first frame update
    void Awake()
    {
        // オブジェクトを円形に配置する
        PlaceObjects();
    }

    // オブジェクトを円形に配置する
    void PlaceObjects()
    {
        // オブジェクトの数
        int objectCount = StageList.List.Count;

        // 各オブジェクトを円形に配置する
        for (int i = 0; i < objectCount; i++)
        {
            // 角度を計算
            float pos = moveStep * i;


            // オブジェクトを配置
            GameObject newObj = Instantiate(PaperUI);
            newObj.transform.SetParent(transform); // プレハブの子に設定
            Vector3 posBuf = newObj.transform.position;
            posBuf.x += pos;
            newObj.transform.position = posBuf;


            //ステージの上に表示するUI作成
            //オブジェクトを配置
            GameObject UIStageName = Instantiate(StageTextUI);
            //ステージ名をセット
            UIStageName.transform.GetChild(0).GetChild(0).GetComponent<UIStageName>().SetStageName(StageList.List[i].stagename);
            //ギミック画像変更
            for (int j = 0; j < StageList.List[i].Gimmick.Count; ++j)
            {
                UIStageName.transform.GetChild(0).GetChild(1).GetChild(j).GetComponent<Image>().sprite = StageList.gimmickSprite[ StageList.List[i].Gimmick[j]];
                UIStageName.transform.GetChild(0).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.white;

            }
            posBuf = UIStageName.transform.position;
            posBuf.x += pos;
            UIStageName.transform.position = posBuf;
            //コア数表示変更
            for (int j = 0; j < StageList.List[i].CoreNum; ++j)
            {
                UIStageName.transform.GetChild(0).GetChild(2).GetChild(j).GetComponent<Image>().color = Color.white;
            }

            //ボス画像変更
            UIStageName.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = StageList.List[i].StageObj;

            UIStageName.transform.SetParent(newObj.transform); // プレハブの子に設定


            if (i != 0)
            {
                newObj.transform.localScale = CommonUIScale * Vector3.one;
                Vector3 CommonPosBuf = newObj.transform.position;
                CommonPosBuf.y = CommonUIHeight;
                newObj.transform.position = CommonPosBuf;

            }

         

        }
    }


}
