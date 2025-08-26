using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Takenoko : MonoBehaviour
{
    [SerializeField] float StartHeight;
    [SerializeField] GameObject objAttack;

    [Header("�U���̃p�����[�^�[")]
    [SerializeField] float[] radius = new float[2];

    [Header("�I�u�W�F��")]
    [SerializeField] int ObjNum = 30;

    [Header("�˂��o�����̏�鍂���i1�i�K�A�Q�i�K�j")]
    [SerializeField] float protrudeHeight_1;
    [SerializeField] float protrudeHeight_2;

    [Header("�˂��o�����̎��ԁi1�i�K�A�Q�i�K�j")]
    [SerializeField] float[] protrudeIntervl = new float[3];

    [Header("�K�w�̍���")]
    [SerializeField] float height;


    float timeCount = 0;
    int AttackCount = 0;
    List<GameObject> takenokoObjects = new List<GameObject>();

    [SerializeField] float duration = 0.5f; // �A�j���[�V�����̎���
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

                    takenoko.SetActive(false); // �I�u�W�F�N�g��\��
                }
            }
        }
    }

    // �I�u�W�F�N�g���~�`�ɔz�u����
    void PlaceObjectsInCircle()
    {
        // �I�u�W�F�N�g�̐�
        int objectCount = ObjNum;

        // 1�̃I�u�W�F�N�g����߂�p�x
        float angleStep = 360f / objectCount;

        // �e�I�u�W�F�N�g���~�`�ɔz�u����
        for (int i = 0; i < objectCount; i++)
        {
            // �p�x���v�Z
            float angle = angleStep * i;

            // �p�x���烉�W�A���ɕϊ�
            float angleRad = angle * Mathf.Deg2Rad;

            // �~����̈ʒu���v�Z
            float x = radius[i % 2] * Mathf.Cos(angleRad);
            float z = radius[i % 2] * Mathf.Sin(angleRad);

            // �I�u�W�F�N�g�̈ʒu��ݒ�
            Vector3 objectPosition = new Vector3(x, StartHeight, z);

            // �I�u�W�F�N�g��z�u
            GameObject newObj = Instantiate(objAttack, objectPosition, Quaternion.identity);
            newObj.transform.SetParent(transform); // �v���n�u�̎q�ɐݒ�
            newObj.AddComponent<BillBoard>();
            newObj.SetActive(false); // ������ԂŔ�\���ɂ���
            
            takenokoObjects.Add(newObj);
        }
    }


    IEnumerator Protrude(int stage)
    {
        float targetHeight = stage == 1 ? protrudeHeight_1 : protrudeHeight_2;

        float elapsedTime = 0;

        foreach (GameObject takenoko in takenokoObjects)
        {
            takenoko.SetActive(true); // �I�u�W�F�N�g��\��
          
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
            takenoko.transform.position = endPos; // �ŏI�ʒu���m��
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
