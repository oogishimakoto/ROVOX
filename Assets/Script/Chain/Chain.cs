using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [Header("�J�n�ʒu")]
    [SerializeField] Transform StartObj;
    Vector3 StartPosition;

    [Header("�I���ʒu")]
    [SerializeField] Transform EndObj;
    Vector3 EndPosition;

    [Header("���Ƃ��Đ�������obj")]
    [SerializeField] GameObject CreateObj;

    
    [SerializeField,Tooltip("��������Ԋu")] float interval = 0.5f;

    [Header("�������ꂽobj")]
    [SerializeField] List<GameObject> ObjectList = new List<GameObject>();// �v���C�t�@�u�����郊�X�g

    float space = 0; //�J�n����I���ւ̋���
    bool end = false;   //�Ō�܂Ő���������
    int ObjectCount = 0;// ���������v���t�@�u�̐�
    Bullet bullet;

    // Start is called before the first frame update
    void Start()
    {
        if(StartObj!= null)
        {
            StartPosition = StartObj.position;
        }
        else
        {
            Debug.Log("Chain�X�N���v�g��StartObj���Ȃ���");
        }

        if (EndObj != null)
        {
            EndPosition = EndObj.position;
        }
        else
        {
            Debug.Log("Chain�X�N���v�g��EndObj���Ȃ���");
        }
        if (GameObject.Find("Player") != null)
        {
            bullet = GameObject.Find("mori").GetComponent<Bullet>();

        }
        else
        {
            Debug.Log("Chain�X�N���v�g��Bullet���ǂݎ��܂���ł���");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateObj != null && !end && ObjectCount == 0 &&
            interval < Vector3.Distance(StartPosition, EndPosition) &&
            bullet.GetBulletState() != Bullet.State.NORMAL)
        {
            Vector3 vec = EndPosition- StartPosition;
            vec.Normalize();

            space =  Vector3.Distance(StartPosition, EndPosition);
           
            for (float i = interval; i <= space;i += interval)
            {
               
                // �C���X�^���X�𐶐�
                GameObject ListObjects = GameObject.Instantiate(CreateObj) as GameObject;// ListObjects�Ƃ��ăv���t�@�u�𐶐�����
                ListObjects.transform.position = StartPosition + vec * i;// ���̃I�u�W�F�N�g�̈ʒu�Ƀv���t�@�u�̍��W���ړ�������
                ListObjects.transform.rotation = Quaternion.identity;// �v���t�@�u�̌��������̂܂܂ɂ���
                ObjectList.Add(ListObjects);// ���X�g�Ƀv���t�@�u��������

                //�I�u�W�F�N�g��1�ȏ゠�邩
                if(ObjectCount > 1)
                {
                    if(ObjectList[ObjectCount - 2] != CreateObj)
                        ObjectList[ObjectCount - 1].GetComponent<HingeJoint>().connectedBody = ObjectList[ObjectCount - 2].GetComponent<Rigidbody>();
                }
                else if(ObjectCount == 1)
                {

                    StartObj.GetComponent<HingeJoint>().connectedBody = ObjectList[ObjectCount - 1].GetComponent<Rigidbody>();
                }
                //���������I�u�W�F�N�g���J�E���g
                ObjectCount++;
            }
            EndObj.GetComponent<HingeJoint>().connectedBody = ObjectList[ObjectCount - 1].GetComponent<Rigidbody>();

            end = true;
        }
        else
        {
            
            StartPosition = StartObj.position;
            EndPosition = EndObj.position;
            if (space != Vector3.Distance(StartPosition, EndPosition))
            {
                end = false;
                for(int i = 0 ; i < ObjectCount;i++) 
                {
                    Destroy(ObjectList[i].gameObject);
                }

                ObjectList.Clear();
                ObjectCount = 0;

            }
        }
    }
}
