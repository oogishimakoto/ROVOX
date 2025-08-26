using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotDamage : MonoBehaviour,IEnemyDamage
{
    [Header("�������镔��")]
    [SerializeField] GameObject BreakParts;

    [Header("�R�A��HP�R���|�[�l���g")]
    [SerializeField] CarrotHeal coreHp;


    [Header("�_���[�W�e�X�g�p")]
    public bool BreakTest;
    public int damagetest = 1;
    private int damageCount;

    [SerializeField] float rotationSpeed = 90.0f; // ��]���x (�x/�b)
    [SerializeField] float totalRotation = 60.0f; // ����]�� (�x)

    public float currentRotation = 0.0f; // ���݂̉�]�� (�x)

    bool IsRotation;

    Bullet bullet;

    private void Reset()
    {
        coreHp = GetComponent<CarrotHeal>();
    }

    private void Start()
    {
        //���g��o�^����
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
        coreHp = GetComponent<CarrotHeal>();

    }

    private void Update()
    {
        if (BreakTest)
        {
            GetComponent<IEnemyDamage>().Damage(1);

            damageCount += 1;
            if (damagetest <= damageCount)
            {
                BreakTest = false;
                damageCount = 0;
            }
        }

        if (IsRotation)
        {

            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
            currentRotation += rotationAmount;
            if (currentRotation >= totalRotation)
            {
                IsRotation = false;
            }

        }


        if (coreHp.Check())
        {

            bullet.BulletReturn();

            this.enabled = false;

            Transform BreakParts = transform.GetChild(0);

            //�擾���������蔻��̂��Ă��镔�ʂ̃u���b�N�ɑ΂��ĂP�P�������邩�̃`�F�b�N
            for (int i = 0; i < BreakParts.transform.childCount; i++)
            {
                Transform childObj = BreakParts.transform.GetChild(i);

                var skin = childObj.GetComponent<SkinnedMeshRenderer>();

                //��͔��肹���ɔ�����
                if (childObj.name == "mori")
                {
                    continue;
                }



                if (skin)
                {
                    //MeshRenderer�ɒu������
                    var mesh = childObj.AddComponent<MeshRenderer>();
                    mesh.material = skin.material;
                    childObj.AddComponent<MeshFilter>();
                    var meshfil = childObj.GetComponent<MeshFilter>();
                    meshfil.mesh = skin.sharedMesh;
                    Destroy(skin);
                }

                //�u���b�N��Ɨ�������
                childObj.transform.parent = null;
                //�u���b�N���폜����R���[�`���Ăяo��
                StartCoroutine(BlockDestroy(childObj.gameObject));
                i--;

                //�d�͂��󂯂�悤�ɂ���
                Rigidbody rb = childObj.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;



                //��̐�[���玩���̃|�W�V�����̃x�N�g���̕��ɔ�΂�
                rb.AddForce((childObj.GetComponent<MeshRenderer>().bounds.center - BreakParts.transform.position).normalized * Random.Range(5.0f, 10.0f) , ForceMode.Impulse);

               
            }
            enabled = false;

        }

    }

    void IEnemyDamage.Damage(int _DamageNum)
    {
        //�R�A��HP�����炷
        coreHp.Decrease(_DamageNum);
        IsRotation = true;
        currentRotation = 0.0f;

        //HP���c���Ă��邩�ǂ���
        if (!coreHp.Check())
        {
            return;
        }


    }


    //��莞�Ԍ�Ƀu���b�N�I�u�W�F�N�g���폜����֐�
    private IEnumerator BlockDestroy(GameObject _block)
    {
        //���b�҂�
        yield return new WaitForSeconds(4f);
        //�폜
        Destroy(_block);

    }
}
