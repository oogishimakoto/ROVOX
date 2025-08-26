using UnityEngine;

public class AttackNotice : MonoBehaviour
{
    [SerializeField] AttackDataList attackData;
    [SerializeField] Boss1_ActionSelect actionComp;
    [SerializeField] Transform childObject; // サイズを変更する子オブジェクトへの参照
    [SerializeField] Vector3 targetScale = Vector3.one; // 表示時の目標サイズ
    Vector3 initialScale = Vector3.one; // 初期サイズ
    bool isDisplaying = false; // 表示状態を追跡するフラグ
    float elapsedTime = 0f; // 経過時間
    float duration = 1f; // サイズ変更にかかる時間（攻撃時間）

    // Start is called before the first frame update
    void Awake()
    {
        if (childObject != null)
        {
            initialScale = childObject.localScale; // 初期サイズを保存
        }

        attackData = transform.root.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }



    void OnDisable()
    {
        isDisplaying = false;
        elapsedTime = 0f; // リセット経過時間
     
    }

    public void Init()
    {
        isDisplaying = true;
        elapsedTime = 0f; // リセット経過時間
        if (childObject != null)
        {
            childObject.localScale = initialScale; // 無効化時にサイズをリセット
        }
        if (attackData != null && actionComp != null && attackData.data.Length > actionComp.AttackPatternNum)
        {
            duration = attackData.data[actionComp.AttackPatternNum].f_BeforeTime + attackData.data[actionComp.AttackPatternNum].f_ChargeTime; // 攻撃時間を取得
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (childObject == null) return;

        if (isDisplaying)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 経過時間の割合
            childObject.localScale = Vector3.Lerp(initialScale, targetScale, t);
        }
       
    }
}
