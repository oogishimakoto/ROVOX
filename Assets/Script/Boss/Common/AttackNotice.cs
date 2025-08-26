using UnityEngine;

public class AttackNotice : MonoBehaviour
{
    [SerializeField] AttackDataList attackData;
    [SerializeField] Boss1_ActionSelect actionComp;
    [SerializeField] Transform childObject; // �T�C�Y��ύX����q�I�u�W�F�N�g�ւ̎Q��
    [SerializeField] Vector3 targetScale = Vector3.one; // �\�����̖ڕW�T�C�Y
    Vector3 initialScale = Vector3.one; // �����T�C�Y
    bool isDisplaying = false; // �\����Ԃ�ǐՂ���t���O
    float elapsedTime = 0f; // �o�ߎ���
    float duration = 1f; // �T�C�Y�ύX�ɂ����鎞�ԁi�U�����ԁj

    // Start is called before the first frame update
    void Awake()
    {
        if (childObject != null)
        {
            initialScale = childObject.localScale; // �����T�C�Y��ۑ�
        }

        attackData = transform.root.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }



    void OnDisable()
    {
        isDisplaying = false;
        elapsedTime = 0f; // ���Z�b�g�o�ߎ���
     
    }

    public void Init()
    {
        isDisplaying = true;
        elapsedTime = 0f; // ���Z�b�g�o�ߎ���
        if (childObject != null)
        {
            childObject.localScale = initialScale; // ���������ɃT�C�Y�����Z�b�g
        }
        if (attackData != null && actionComp != null && attackData.data.Length > actionComp.AttackPatternNum)
        {
            duration = attackData.data[actionComp.AttackPatternNum].f_BeforeTime + attackData.data[actionComp.AttackPatternNum].f_ChargeTime; // �U�����Ԃ��擾
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (childObject == null) return;

        if (isDisplaying)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // �o�ߎ��Ԃ̊���
            childObject.localScale = Vector3.Lerp(initialScale, targetScale, t);
        }
       
    }
}
