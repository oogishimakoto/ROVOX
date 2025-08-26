using UnityEngine;

public class Elevator : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 2.0f;

    // オンの時間（エレベーターが動いている時間）
    [Header("オンのときの時間")]
    public float onDuration = 2.0f;

    // オフの時間（エレベーターが止まっている時間）
    [Header("オフのときの時間")]
    public float offDuration = 2.0f;

    // 内部タイマー
    private float timer = 0.0f;

    // 現在の状態を示すフラグ（true = 動作中, false = 停止中）
    private bool isActive = true;

    // エレベーターのCollider
    private Collider elevatorCollider;

    private GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        // エレベーターのColliderを取得
        elevatorCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // 現在の状態に基づいてタイマーを確認
        if (isActive && timer >= onDuration)
        {
            // 動作中の持続時間を超えた場合
            SwitchState();
        }
        else if (!isActive && timer >= offDuration)
        {
            // 停止中の持続時間を超えた場合
            SwitchState();
        }
    }

    private void SwitchState()
    {
        // タイマーをリセット
        timer = 0.0f;

        // 状態を反転
        isActive = !isActive;

        //画像表示変更
        if (transform.childCount != 0)
        {
            transform.GetChild(0).gameObject.SetActive(isActive);

        }


        // Colliderの状態を反映
        elevatorCollider.enabled = isActive;
        if (!isActive)
        {
            playerObj.transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void ElevatorMove(GameObject _obj)
    {
        // 上昇させたいオブジェクトのY軸の値を加算する
        _obj.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerStay(Collider col)
    {
        // 当たったオブジェクトのタグがPlayerなら
        if (col.gameObject.name == "Player" && isActive)
        {
            // エレベーターの移動処理を行う
            ElevatorMove(col.gameObject);
            col.transform.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        // 当たったオブジェクトのタグがPlayerなら
        if (col.gameObject.name == "Player")
        {
            col.transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
