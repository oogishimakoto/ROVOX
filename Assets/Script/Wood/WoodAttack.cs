using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WoodAttack : MonoBehaviour
{
    [SerializeField] GameObject CreatTree;
    [SerializeField] GameObject Field;

    [Header("�c���[�𐶐�����ʒu�͈̔�")]
    [SerializeField] Vector3 v3_CenterPosition;

    [SerializeField] Vector2 v2_Range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Char")
        {
            Debug.Log("�G�Ƀ_���[�W");

            //Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            //for (int i = 0; i < other.transform.childCount; i++)
            //{
            //    Transform childObj = other.transform.GetChild(i);
            //    //�R���C�_�[���Ȃ��Ȃ�t����
            //    if (!childObj.GetComponent<BoxCollider>())
            //    {
            //        childObj.AddComponent<BoxCollider>();


            //        // ����GameObject�̃T�C�Y���擾
            //        Bounds bounds = childObj.GetComponent<MeshFilter>().sharedMesh.bounds;

            //        // BoxCollider�̃T�C�Y��ݒ�
            //        childObj.GetComponent<BoxCollider>().size = bounds.size;

            //        // BoxCollider�̒��S������GameObject�̒��S�ɐݒ�
            //        childObj.GetComponent<BoxCollider>().center = bounds.center;
            //    }
            //    //�u���b�N��Ɨ�������
            //    childObj.transform.parent = null;


            //    //�d�͂��󂯂�悤�ɂ���
            //    childObj.AddComponent<Rigidbody>().useGravity = true;
            //    childObj.GetComponent<Rigidbody>().isKinematic = false;

            //    //Drop�̃X�N���v�g��t����
            //    childObj.AddComponent<Drop>();

            //    rb.AddForce((transform.position - transform.GetChild(0).transform.position).normalized * 5.0f, ForceMode.Impulse);


            //}

            this.gameObject.SetActive(false);

        }
    }


}
