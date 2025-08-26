using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WoodAttack : MonoBehaviour
{
    [SerializeField] GameObject CreatTree;
    [SerializeField] GameObject Field;

    [Header("ツリーを生成する位置の範囲")]
    [SerializeField] Vector3 v3_CenterPosition;

    [SerializeField] Vector2 v2_Range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Char")
        {
            Debug.Log("敵にダメージ");

            //Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            //for (int i = 0; i < other.transform.childCount; i++)
            //{
            //    Transform childObj = other.transform.GetChild(i);
            //    //コライダーがないなら付ける
            //    if (!childObj.GetComponent<BoxCollider>())
            //    {
            //        childObj.AddComponent<BoxCollider>();


            //        // 元のGameObjectのサイズを取得
            //        Bounds bounds = childObj.GetComponent<MeshFilter>().sharedMesh.bounds;

            //        // BoxColliderのサイズを設定
            //        childObj.GetComponent<BoxCollider>().size = bounds.size;

            //        // BoxColliderの中心を元のGameObjectの中心に設定
            //        childObj.GetComponent<BoxCollider>().center = bounds.center;
            //    }
            //    //ブロックを独立させる
            //    childObj.transform.parent = null;


            //    //重力を受けるようにする
            //    childObj.AddComponent<Rigidbody>().useGravity = true;
            //    childObj.GetComponent<Rigidbody>().isKinematic = false;

            //    //Dropのスクリプトを付ける
            //    childObj.AddComponent<Drop>();

            //    rb.AddForce((transform.position - transform.GetChild(0).transform.position).normalized * 5.0f, ForceMode.Impulse);


            //}

            this.gameObject.SetActive(false);

        }
    }


}
