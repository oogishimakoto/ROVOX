using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLife : MonoBehaviour
{
    [Header("体力を生成する場所")]
    [SerializeField] private GameObject LifeAll;

    [Header("1体力")]
    [SerializeField] private Image Life;

    [Tooltip("Lifeを生成する幅")]
    [SerializeField] private float LifeWidthSize = 100.0f;
    private float distance = 100.0f;

    StageDataManager manager;
    StageClearDataList.StageClearData stagedatalist;
    //生成したオブジェクトを保存
    [SerializeField] List<Image> Lifelist = new List<Image>();

    int LifeCount;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        stagedatalist = manager.GetStageData();

        //間隔を計算
        distance = LifeWidthSize / stagedatalist.RestHP;
        LifeCount = stagedatalist.RestHP;  //現在のHP
        if (LifeAll != null && Life != null)
            CreateLife();
    }

    // Update is called once per frame
    void Update()
    {
        //DamegeLife();

        //HealLife();
    }


    private void CreateLife()
    {
        for (int i = 0; i < stagedatalist.RestHP; i++)
        {
            //配置する位置を計算
            Vector3 pos = LifeAll.gameObject.transform.position;
            pos.x -= distance * i;

            //ライフを生成して保存
            Lifelist.Add(Instantiate(Life, pos, Quaternion.identity));
            Lifelist[i].transform.parent = LifeAll.transform;

        }
    }

    //public void DamegeLife()
    //{
    //    if (LifeCount > playerinfo.HP)
    //    {
    //        for (int i = 0; i < LifeCount - playerinfo.HP; i++)
    //        {
    //            Lifelist[LifeCount - (i + 1)].gameObject.SetActive(false);
    //        }
            
    //        //HP更新
    //        LifeCount = playerinfo.HP;
    //    }
    //}

    //public void HealLife()
    //{
    //    if (LifeCount < playerinfo.HP)
    //    {
    //        for(int i = 0;i < playerinfo.HP;i++)
    //        {
    //            Lifelist[i].gameObject.SetActive(true);
    //        }
           
    //    }
    //}
}
