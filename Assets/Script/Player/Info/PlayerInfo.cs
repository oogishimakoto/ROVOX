using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{  

    [field:SerializeField,Tooltip("体力")] public int HP { get;  set; } = 5;

    [field: SerializeField, Tooltip("移動速度")] public float moveSpeed { get; private set; } = 5;
    [field: SerializeField, Tooltip("チャージ中の移動速度")] public float chargeSpeed { get; private set; } = 4;

    [field: SerializeField, Tooltip("回復量")] public int healPoint { get; private set; } = 1;

    [field: SerializeField, Tooltip("ワープ移動到着時間")] public float warpTime { get; private set; } = 0.5f;

    [field: SerializeField, Tooltip("チャージ段階1の時間")] public float chargetime1 { get; private set; } = 1;
    [field: SerializeField, Tooltip("チャージ段階2の時間")] public float chargetime2 { get; private set; } = 2;
    [field: SerializeField, Tooltip("チャージ段階3の時間")] public float chargetime3 { get; private set; } = 3;

    [field: SerializeField, Tooltip("チャージ段階1のスピード")] public float shotspeed1 { get; private set; } = 2;
    [field: SerializeField, Tooltip("チャージ段階2のスピード")] public float shotspeed2 { get; private set; } = 4;
    [field: SerializeField, Tooltip("チャージ段階3のスピード")] public float shotspeed3 { get; private set; } = 6;

    [field: SerializeField, Tooltip("武器が戻る時のスピード")] public float returnspeed { get; private set; } = 6;
    [field: SerializeField, Tooltip("ショットの時間")] public float shottime { get; private set; } = 3;

    [field: SerializeField, Tooltip("ワープ時の着地場所の半径")] public float warpradius { get; private set; } = 2;

    [field: SerializeField, Tooltip("被ダメ時の無敵時間")] public float InvincibilityTime { get; private set; } = 3;
    [field: SerializeField, Tooltip("チャージUI表示時間")] public float ChargeUIDrawTime { get; private set; } = 0.3f;

}
