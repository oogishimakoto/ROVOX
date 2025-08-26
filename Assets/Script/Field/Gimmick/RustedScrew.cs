using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

public class RustedScrew : MonoBehaviour, IEnemyDamage
{
    [SerializeField] int MAXHP = 20;
    [SerializeField] int HP = 20;

    private float damageVec;  //1HP������̈ړ��x�N�g��

    Bullet bullet;


    [SerializeField] GameObject blockPrehub;
    [SerializeField] float createBlockTime = 1;
    [SerializeField] int createBlockNum = 30;
    float createBlockCount;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
     
        transform.tag = "Core";
    }

    private void Update()
    {
        createBlockCount += Time.deltaTime;

        if (Check())
        {

            bullet.BulletReturn();

            transform.tag = "Warp";
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
                rb.AddForce((childObj.GetComponent<MeshRenderer>().bounds.center - BreakParts.transform.position).normalized * 40.0f, ForceMode.Impulse);
            }

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

    void IEnemyDamage.Damage(int _DamageNum)
    {
        HP -= _DamageNum;
        //damageVec.y = 0;
        this.transform.position -= transform.forward * damageVec * _DamageNum;


        if (createBlockCount >= createBlockTime )
        {


            for (int i = 0; i < createBlockNum; ++i)
            {
                Vector3 force;
                force.x = Random.Range(-5, 5);
                force.z = Random.Range(-5, 5);
                force.y = Random.Range(0, 20);
                GameObject obj = Instantiate(blockPrehub, transform);
                obj.transform.localScale *= 1.5f;
                obj.transform.parent = null;
                obj.transform.AddComponent<Rigidbody>().useGravity = true;
                obj.transform.GetComponent<Rigidbody>().AddForce(force * 2.0f, ForceMode.Impulse);


                //�u���b�N���폜����R���[�`���Ăяo��
                StartCoroutine(BlockDestroy(obj.gameObject));

            }
            createBlockCount = 0;
        }

    }

    public bool Check()
    {
        //�R�A��HP��0�ȉ��Ȃ�true��Ԃ�
        if (HP <= 0)
        {

            return true;

        }

        return false;
    }

}
