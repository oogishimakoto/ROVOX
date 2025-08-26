using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotHeal : MonoBehaviour
{
    //�ő�̗�
    [SerializeField] int MAXHP = 20;
    [SerializeField] float endPos; //�ړI�ꏊ

    [SerializeField] int HP = 20;//�c��̗�

    [Header("stem_ALL������")]
    [SerializeField] GameObject targetpos;  //�i�ޕ���

    private float damageVec;  //1HP������̈ړ��x�N�g��

    PlayerInfo playerInfo;�@ //�v���C���[��hp����ǂݎ��p
    int playermaxhp = 100;  //�v���C���[�̗̑�

    Bullet bullet;�@ //�o���b�g��Ԃ����Ɏg��


    // Start is called before the first frame update
    void Start()
    {
        //������
        damageVec = endPos / HP;
        HP = MAXHP;
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        bullet = GameObject.Find("mori").GetComponent<Bullet>();
        playermaxhp = playerInfo.HP;
        if (targetpos == null)
        {
            for(int i = 0;i < transform.childCount; i++) 
            {
                if(transform.GetChild(i).name == "stem_ALL")
                    targetpos = transform.GetChild(1).gameObject;
            }

            

        }
    }

    private void Update()
    {
        //�l�Q�̗̑̓`�F�b�N
        if(Check())
        {
            //�v���C���[�̗̑͂��񕜂ł���ʂ𔻒�
            if(playerInfo.HP + playerInfo.healPoint <= playermaxhp)
            {
                //���v�l���ő�̗͂�菭�Ȃ��̂ŉ񕜗ʕ���
                playerInfo.HP += playerInfo.healPoint;

            }
            else
            {
                //���v�l���ő�̗͂�葽���̂ōő�̗͂ɂ���
                playerInfo.HP = playermaxhp;
            }
            
            bullet.BulletReturn();  //���Ԃ�

           
        }

        if (transform.GetChild(0).childCount <= 0)
        {
            gameObject.SetActive(false);
            enabled = false;
        }
           
    }

    //HP�����炷�֐�
    public void Decrease(int _value)
    {
        if(enabled)
        {
            HP -= _value;
            //�l�Q�̈ʒu���_���[�W���󂯂邲�ƂɈړ�������@�����@�~�@1HP������̈ړ��x�N�g���@�~�@�󂯂��_���[�W
            this.transform.position -= (targetpos.transform.position - transform.position).normalized * damageVec * _value;
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
