using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("�̗͂𐶐�����ꏊ")]
    [SerializeField] private GameObject LifeAll;

    [Header("1�̗�")]
    [SerializeField] private Image Life;

    [Tooltip("Life�𐶐����镝")]
    [SerializeField] private float LifeWidthSize = 100.0f;
    private float distance = 100.0f;

    PlayerInfo playerinfo;

    //���������I�u�W�F�N�g��ۑ�
    [SerializeField] List<Image> Lifelist = new List<Image>();

    int LifeCount;

    // Start is called before the first frame update
    void Start()
    {
        playerinfo = GetComponent<PlayerInfo>();
        //�Ԋu���v�Z
        distance = LifeWidthSize / playerinfo.HP;
        LifeCount = playerinfo.HP;  //���݂�HP
        if (LifeAll != null && Life != null)
            CreateLife();
    }

    // Update is called once per frame
    void Update()
    {
        DamegeLife();

        HealLife();
    }


    private void CreateLife()
    {
        for (int i = 0; i < playerinfo.HP; i++)
        {
            //�z�u����ʒu���v�Z
            Vector3 pos = LifeAll.gameObject.transform.position;
            pos.x -= distance * i;

            //���C�t�𐶐����ĕۑ�
            Lifelist.Add(Instantiate(Life, pos, Quaternion.identity));
            Lifelist[i].transform.parent = LifeAll.transform;

        }
    }

    public void DamegeLife()
    {
        if (LifeCount > playerinfo.HP)
        {
            for (int i = 0; i < LifeCount - playerinfo.HP; i++)
            {
                Lifelist[LifeCount - (i + 1)].gameObject.SetActive(false);
            }
            
            //HP�X�V
            LifeCount = playerinfo.HP;
        }

    }

    public void HealLife()
    {
        if (LifeCount < playerinfo.HP)
        {
            for(int i = 0;i < playerinfo.HP;i++)
            {
                Lifelist[i].gameObject.SetActive(true);
            }

            LifeCount = playerinfo.HP;
        }
    }
}
