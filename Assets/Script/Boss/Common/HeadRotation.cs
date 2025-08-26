using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    //プレイヤーオブジェクト保存変数
    private GameObject PlayerObj = null;

    [Header("回転速度")]
    [SerializeField] float rotationSpeed;

    [Header("頭のジョイント")]
    [SerializeField] Transform headJoint;

    //上半身回転数保存用変数
    Quaternion headRotation;

    private void Start()
    {
        //プレイヤーを取得
        PlayerObj = GameObject.Find("Player");
    }

    void Update()
    {
        //キャラクターを回転
        // ターゲットへの方向を計算
        Vector3 direction = PlayerObj.transform.position - transform.position;
        // directionのy成分を0にすることで、XZ平面での方向のみを考慮
        direction.y = 0;
        Quaternion targetRotation;
        // 目標の回転を計算
        if (direction.x >= 0)
        {
            targetRotation = Quaternion.LookRotation(direction, -Vector3.forward);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(direction, Vector3.forward);
        }


        // 現在の回転と目標の回転を線形補間でスムーズに回転させる
        headRotation = Quaternion.Lerp(headRotation, targetRotation, Time.deltaTime * rotationSpeed);

        headJoint.transform.rotation = headRotation;

    }
}
