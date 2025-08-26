using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Takenoko : MonoBehaviour
{
    [SerializeField] float StartHeight;
    [SerializeField] GameObject objAttack;

    [Header("攻撃のパラメーター")]
    [SerializeField] float[] radius = new float[2];

    [Header("オブジェ数")]
    [SerializeField] int ObjNum = 30;

    [Header("突き出す時の上る高さ（1段階、２段階）")]
    [SerializeField] float protrudeHeight_1;
    [SerializeField] float protrudeHeight_2;

    [Header("突き出す時の時間（1段階、２段階）")]
    [SerializeField] float[] protrudeIntervl = new float[3];

    [Header("階層の高さ")]
    [SerializeField] float height;


    float timeCount = 0;
    int AttackCount = 0;
    List<GameObject> takenokoObjects = new List<GameObject>();

    [SerializeField] float duration = 0.5f; // アニメーションの時間
    [SerializeField] Animator anim; 

    bool IsAttack;

    PlayerAction hierarchyInfo;
    int hierarchyNum;
    private void Start()
    {
        PlaceObjectsInCircle();
        hierarchyInfo = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<PlayerAction>();
    }

    private void Update()
    {
        if (IsAttack)
        {
            timeCount += Time.deltaTime;

            if (AttackCount < protrudeIntervl.Length -1 && timeCount >= protrudeIntervl[AttackCount])
            {
                AttackCount++;
                timeCount = 0.0f;
                hierarchyNum = 0;
                StartCoroutine(Protrude(AttackCount));
               
            }
            else if (AttackCount < protrudeIntervl.Length && timeCount >= protrudeIntervl[AttackCount])
            {
                timeCount = 0.0f;
                IsAttack = false;
                foreach (GameObject takenoko in takenokoObjects)
                {
                    GetComponent<Collider>().enabled = false;

                    takenoko.SetActive(false); // オブジェクトを表示
                }
            }
        }
    }

    // オブジェクトを円形に配置する
    void PlaceObjectsInCircle()
    {
        // オブジェクトの数
        int objectCount = ObjNum;

        // 1つのオブジェクトが占める角度
        float angleStep = 360f / objectCount;

        // 各オブジェクトを円形に配置する
        for (int i = 0; i < objectCount; i++)
        {
            // 角度を計算
            float angle = angleStep * i;

            // 角度からラジアンに変換
            float angleRad = angle * Mathf.Deg2Rad;

            // 円周上の位置を計算
            float x = radius[i % 2] * Mathf.Cos(angleRad);
            float z = radius[i % 2] * Mathf.Sin(angleRad);

            // オブジェクトの位置を設定
            Vector3 objectPosition = new Vector3(x, StartHeight, z);

            // オブジェクトを配置
            GameObject newObj = Instantiate(objAttack, objectPosition, Quaternion.identity);
            newObj.transform.SetParent(transform); // プレハブの子に設定
            newObj.AddComponent<BillBoard>();
            newObj.SetActive(false); // 初期状態で非表示にする
            
            takenokoObjects.Add(newObj);
        }
    }


    IEnumerator Protrude(int stage)
    {
        float targetHeight = stage == 1 ? protrudeHeight_1 : protrudeHeight_2;

        float elapsedTime = 0;

        foreach (GameObject takenoko in takenokoObjects)
        {
            takenoko.SetActive(true); // オブジェクトを表示
          
        }
        if (stage == 2)
        {

            GetComponent<Collider>().enabled = true;
            float newYPosition = hierarchyNum * height;

            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }
        while (elapsedTime < duration)
        {
            foreach (GameObject takenoko in takenokoObjects)
            {
                float startHeight = stage == 1 ? StartHeight + (hierarchyNum * height) : StartHeight + (hierarchyNum * height) + protrudeHeight_1;
                Vector3 startPos = new Vector3(takenoko.transform.position.x, startHeight, takenoko.transform.position.z);
                Vector3 endPos = new Vector3(takenoko.transform.position.x, startHeight + targetHeight, takenoko.transform.position.z);
                takenoko.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject takenoko in takenokoObjects)
        {
            float finalHeight = stage == 1 ? StartHeight + (hierarchyNum * height) + protrudeHeight_1 : StartHeight + (hierarchyNum * height) + protrudeHeight_1 + protrudeHeight_2;
            Vector3 endPos = new Vector3(takenoko.transform.position.x, finalHeight, takenoko.transform.position.z);
            takenoko.transform.position = endPos; // 最終位置を確定
        }
    }

    public void OnTakenokoAttack()
    {
        IsAttack = true;
        AttackCount = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerDamage info = other.GetComponent<PlayerDamage>();
            if (info != null)
            {
                info.Damage(1, new Vector3(0.0f,20.0f,0.0f));
                //this.transform.GetComponent<Collider>().enabled = false;
            }
        }
       
    }
}
