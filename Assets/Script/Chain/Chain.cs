using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [Header("開始位置")]
    [SerializeField] Transform StartObj;
    Vector3 StartPosition;

    [Header("終了位置")]
    [SerializeField] Transform EndObj;
    Vector3 EndPosition;

    [Header("鎖として生成するobj")]
    [SerializeField] GameObject CreateObj;

    
    [SerializeField,Tooltip("生成する間隔")] float interval = 0.5f;

    [Header("生成されたobj")]
    [SerializeField] List<GameObject> ObjectList = new List<GameObject>();// プレイファブを入れるリスト

    float space = 0; //開始から終了への距離
    bool end = false;   //最後まで生成したか
    int ObjectCount = 0;// 生成したプレファブの数
    Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        if(StartObj!= null)
        {
            StartPosition = StartObj.position;
        }
        else
        {
            Debug.Log("ChainスクリプトのStartObjがないよ");
        }

        if (EndObj != null)
        {
            EndPosition = EndObj.position;
        }
        else
        {
            Debug.Log("ChainスクリプトのEndObjがないよ");
        }
        if (GameObject.Find("Player") != null)
        {
            bullet = GameObject.Find("mori").GetComponent<Bullet>();

        }
        else
        {
            Debug.Log("ChainスクリプトのBulletが読み取れませんでした");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateObj != null && !end && ObjectCount == 0 &&
            interval < Vector3.Distance(StartPosition, EndPosition) &&
            bullet.GetBulletState() != Bullet.State.NORMAL)
        {
            Vector3 vec = EndPosition- StartPosition;
            vec.Normalize();

            space =  Vector3.Distance(StartPosition, EndPosition);
           
            for (float i = interval; i <= space;i += interval)
            {
               
                // インスタンスを生成
                GameObject ListObjects = GameObject.Instantiate(CreateObj) as GameObject;// ListObjectsとしてプレファブを生成する
                ListObjects.transform.position = StartPosition + vec * i;// このオブジェクトの位置にプレファブの座標を移動させる
                ListObjects.transform.rotation = Quaternion.identity;// プレファブの向きを元のままにする
                ObjectList.Add(ListObjects);// リストにプレファブを加える

                //オブジェクトが1つ以上あるか
                if(ObjectCount > 1)
                {
                    if(ObjectList[ObjectCount - 2] != CreateObj)
                        ObjectList[ObjectCount - 1].GetComponent<HingeJoint>().connectedBody = ObjectList[ObjectCount - 2].GetComponent<Rigidbody>();
                }
                else if(ObjectCount == 1)
                {

                    StartObj.GetComponent<HingeJoint>().connectedBody = ObjectList[ObjectCount - 1].GetComponent<Rigidbody>();
                }
                //生成したオブジェクトをカウント
                ObjectCount++;
            }
            EndObj.GetComponent<HingeJoint>().connectedBody = ObjectList[ObjectCount - 1].GetComponent<Rigidbody>();

            end = true;
        }
        else
        {
            
            StartPosition = StartObj.position;
            EndPosition = EndObj.position;
            if (space != Vector3.Distance(StartPosition, EndPosition))
            {
                end = false;
                for(int i = 0 ; i < ObjectCount;i++) 
                {
                    Destroy(ObjectList[i].gameObject);
                }

                ObjectList.Clear();
                ObjectCount = 0;

            }
        }
    }
}
