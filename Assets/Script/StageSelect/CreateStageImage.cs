using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStageImage : MonoBehaviour
{
    //�X�e�[�W�f�[�^�̃��X�g
    [SerializeField] StageDataList�@StageList;


     [SerializeField] float moveStep = 5f;

    // �p�x�̃I�t�Z�b�g

    //�X�e�[�WUI�̃I�t�Z�b�g
    [SerializeField] float SelectUIHeight = 4.66f;
    [SerializeField] float CommonUIHeight = 2.88f;

    [Header("�I�����Ă��Ȃ�UI�̃X�P�[��")]
    [SerializeField] float CommonUIScale;

    [SerializeField] GameObject PaperUI;
    [SerializeField] GameObject StageTextUI;

    // Start is called before the first frame update
    void Awake()
    {
        // �I�u�W�F�N�g���~�`�ɔz�u����
        PlaceObjects();
    }

    // �I�u�W�F�N�g���~�`�ɔz�u����
    void PlaceObjects()
    {
        // �I�u�W�F�N�g�̐�
        int objectCount = StageList.List.Count;

        // �e�I�u�W�F�N�g���~�`�ɔz�u����
        for (int i = 0; i < objectCount; i++)
        {
            // �p�x���v�Z
            float pos = moveStep * i;


            // �I�u�W�F�N�g��z�u
            GameObject newObj = Instantiate(PaperUI);
            newObj.transform.SetParent(transform); // �v���n�u�̎q�ɐݒ�
            Vector3 posBuf = newObj.transform.position;
            posBuf.x += pos;
            newObj.transform.position = posBuf;


            //�X�e�[�W�̏�ɕ\������UI�쐬
            //�I�u�W�F�N�g��z�u
            GameObject UIStageName = Instantiate(StageTextUI);
            //�X�e�[�W�����Z�b�g
            UIStageName.transform.GetChild(0).GetChild(0).GetComponent<UIStageName>().SetStageName(StageList.List[i].stagename);
            //�M�~�b�N�摜�ύX
            for (int j = 0; j < StageList.List[i].Gimmick.Count; ++j)
            {
                UIStageName.transform.GetChild(0).GetChild(1).GetChild(j).GetComponent<Image>().sprite = StageList.gimmickSprite[ StageList.List[i].Gimmick[j]];
                UIStageName.transform.GetChild(0).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.white;

            }
            posBuf = UIStageName.transform.position;
            posBuf.x += pos;
            UIStageName.transform.position = posBuf;
            //�R�A���\���ύX
            for (int j = 0; j < StageList.List[i].CoreNum; ++j)
            {
                UIStageName.transform.GetChild(0).GetChild(2).GetChild(j).GetComponent<Image>().color = Color.white;
            }

            //�{�X�摜�ύX
            UIStageName.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = StageList.List[i].StageObj;

            UIStageName.transform.SetParent(newObj.transform); // �v���n�u�̎q�ɐݒ�


            if (i != 0)
            {
                newObj.transform.localScale = CommonUIScale * Vector3.one;
                Vector3 CommonPosBuf = newObj.transform.position;
                CommonPosBuf.y = CommonUIHeight;
                newObj.transform.position = CommonPosBuf;

            }

         

        }
    }


}
