using UnityEngine;

public class Elevator : MonoBehaviour
{
    // �ړ����x
    public float moveSpeed = 2.0f;

    // �I���̎��ԁi�G���x�[�^�[�������Ă��鎞�ԁj
    [Header("�I���̂Ƃ��̎���")]
    public float onDuration = 2.0f;

    // �I�t�̎��ԁi�G���x�[�^�[���~�܂��Ă��鎞�ԁj
    [Header("�I�t�̂Ƃ��̎���")]
    public float offDuration = 2.0f;

    // �����^�C�}�[
    private float timer = 0.0f;

    // ���݂̏�Ԃ������t���O�itrue = ���쒆, false = ��~���j
    private bool isActive = true;

    // �G���x�[�^�[��Collider
    private Collider elevatorCollider;

    private GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        // �G���x�[�^�[��Collider���擾
        elevatorCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // �^�C�}�[���X�V
        timer += Time.deltaTime;

        // ���݂̏�ԂɊ�Â��ă^�C�}�[���m�F
        if (isActive && timer >= onDuration)
        {
            // ���쒆�̎������Ԃ𒴂����ꍇ
            SwitchState();
        }
        else if (!isActive && timer >= offDuration)
        {
            // ��~���̎������Ԃ𒴂����ꍇ
            SwitchState();
        }
    }

    private void SwitchState()
    {
        // �^�C�}�[�����Z�b�g
        timer = 0.0f;

        // ��Ԃ𔽓]
        isActive = !isActive;

        //�摜�\���ύX
        if (transform.childCount != 0)
        {
            transform.GetChild(0).gameObject.SetActive(isActive);

        }


        // Collider�̏�Ԃ𔽉f
        elevatorCollider.enabled = isActive;
        if (!isActive)
        {
            playerObj.transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void ElevatorMove(GameObject _obj)
    {
        // �㏸���������I�u�W�F�N�g��Y���̒l�����Z����
        _obj.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerStay(Collider col)
    {
        // ���������I�u�W�F�N�g�̃^�O��Player�Ȃ�
        if (col.gameObject.name == "Player" && isActive)
        {
            // �G���x�[�^�[�̈ړ��������s��
            ElevatorMove(col.gameObject);
            col.transform.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        // ���������I�u�W�F�N�g�̃^�O��Player�Ȃ�
        if (col.gameObject.name == "Player")
        {
            col.transform.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
