using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    //プレイヤーオブジェクト保存変数
    private GameObject PlayerObj = null;
 
    [Header("回転速度")]
    [SerializeField] float rotationSpeed;
  
    [Header("上半身のジョイント")]
    [SerializeField] Transform bodyJoint;
     AttackDataList dataList;

    [Header("プレイヤーの階層取得用"), SerializeField] PlayerAction hierarchyInfo;

    //攻撃パターン決定取得用コンポーネント
    IActionSelect actionPaternComp;
    public bool isRotate  = false;
    //上半身回転数保存用変数
    Quaternion bodyRotation;

    void Start()
    {
        //プレイヤーを取得
        PlayerObj = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj();
        actionPaternComp = GetComponent<IActionSelect>();
        dataList = transform.root.transform.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }

    private void LateUpdate()
    {
        //ターゲットの方に体を回転させる
        if (isRotate)
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

            //攻撃パターンから回転のオフセットを取得
            float rotationOffset = 1.0f;
            //攻撃を当てるために回転を補正
            Quaternion adjustment = Quaternion.Euler(rotationOffset, 0, 0);
            targetRotation *= adjustment;

            // 現在の回転と目標の回転を線形補間でスムーズに回転させる
            bodyRotation = Quaternion.Lerp(bodyRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        else if(isRotate == false)
        { 
            //何もしない
        }
   

        bodyJoint.transform.rotation = bodyRotation;
    }

    public void IsRotation(bool _isRotate)
    {
        isRotate = _isRotate;
    }
}
