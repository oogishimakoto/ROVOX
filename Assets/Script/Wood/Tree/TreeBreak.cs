using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeBreak : MonoBehaviour
{
    //�G�L�����N�^�[�̏����u���b�N��
    private int n_MaxHP;
    private int n_NowHP;

    public GameObject player;

    public int NeedKillEnemy = 3;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //�u���b�N�̏����̏��������v�Z
            n_MaxHP += transform.GetChild(i).childCount;
        }

        //�����̏����������݂̏������ɍ��킹��
        n_NowHP = n_MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (n_NowHP < n_MaxHP * 0.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (player.gameObject.GetComponent<PlayerMove>().GetKillenemyCount() >= NeedKillEnemy && other.name == "mori")
        {
            Rigidbody rb = transform.GetComponent<Rigidbody>();
            for (int i = 0; i < transform.GetChild(1).childCount; i++)
            {
                Transform childObj = transform.GetChild(1).GetChild(i);
                //�R���C�_�[���Ȃ��Ȃ�t����
                if (!childObj.GetComponent<BoxCollider>())
                {
                    childObj.AddComponent<BoxCollider>();
                   

                    // ����GameObject�̃T�C�Y���擾
                    Bounds bounds = childObj.GetComponent<MeshFilter>().sharedMesh.bounds;

                    // BoxCollider�̃T�C�Y��ݒ�
                    childObj.GetComponent<BoxCollider>().size = bounds.size;

                    // BoxCollider�̒��S������GameObject�̒��S�ɐݒ�
                    childObj.GetComponent<BoxCollider>().center = bounds.center;
                }
                //�u���b�N��Ɨ�������
                childObj.transform.parent = null;


                //�d�͂��󂯂�悤�ɂ���
                childObj.AddComponent<Rigidbody>().useGravity = true;
                childObj.GetComponent<Rigidbody>().isKinematic = false;

                //Drop�̃X�N���v�g��t����
                childObj.AddComponent<Drop>();

                rb.AddForce((transform.position - childObj.transform.position).normalized * 5.0f, ForceMode.Impulse);

                n_NowHP--;
            }

        }
    }
}
