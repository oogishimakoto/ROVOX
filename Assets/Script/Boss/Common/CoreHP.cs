using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoreHP : MonoBehaviour
{
    [SerializeField] int MAXHP = 60;
    [SerializeField] int MinHP = 10;
    [SerializeField] float endPos;

    [SerializeField] int HP = 60;

    [Header("�_���[�WSE")]
    [SerializeField] AudioClip SE;
    AudioSource source;

    private float damageVec;  //1HP������̈ړ��x�N�g��

    private void Start()
    {
         damageVec = endPos / MAXHP;
        HP = MAXHP;

        source = transform.AddComponent<AudioSource>();
        GameObject.Find("SEManager").GetComponent<SEManager>().SetAudioSource(source);

    }

    //HP�����炷�֐�
    public void HPDecrease(int _value)
    {
        HP -= _value;
        this.transform.position -= transform.forward* damageVec * _value;

        source.clip = SE;
        source.loop = false;
        source.PlayOneShot(source.clip);
    }

    //HP���c���Ă��邩�`�F�b�N�֐�
    public bool CheckHP()
    {
        //�R�A��HP��0�ȉ��Ȃ�true��Ԃ�
        if(HP <= 0)
        {
            return true;
        }

        return false;
    }

    //�R�A�̍Œ�̗͂܂ŉ񕜂�����֐�
    private void CoreRefresh()
    {
        if (HP < MinHP)
        {
            //�Œ�̗͂ƌ��݂�HP�̍����R�A�̈ʒu��߂��čŒ�̗͂܂ŉ񕜂�����
            this.transform.position += transform.forward * damageVec *( MinHP - HP);
            HP = MinHP;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        //�󂪈��������ꂽ�Ƃ��ɃR�A�̍Œ�̗͂܂ŉ񕜂�����
        if (other.transform.tag == "Player")
        {

            CoreRefresh();
        }
    }

}
