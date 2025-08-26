using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_ActionSelect : MonoBehaviour, IActionSelect
{

    AttackDataList dataList;
    [SerializeField] float SpecialAttackTime = 180;       //����U�������鎞��
    [SerializeField] float SpecialAttackCount = 0;       //����U���p�^�C�}�[

    [field :SerializeField] public int AttackPatternNum { get; private set; }      //�U���p�^�[���i�[�p


     PlayerAction hierarchyInfo;


    private void Start()
    {
        hierarchyInfo = transform.root.GetComponent<Enemy_PlayerManager>().GetPlayerObj().GetComponent<PlayerAction>();
        dataList = transform.root.transform.GetComponent<Enemy_PlayerManager>().GetAttackData();
    }

    private void Update()
    {
        SpecialAttackCount += Time.deltaTime;

    }

    //�U���p�^�[������
    public void ActionSelect()
    {

        //�v���C���[�̂���t���A����s���\�ȍU���������_���Œ��I
        AttackPatternNum = dataList.Process[hierarchyInfo.StageHierarchical].paternNum[Random.Range(0, dataList.Process[hierarchyInfo.StageHierarchical].paternNum.Length)];

#if UNITY_EDITOR

        Debug.Log(dataList.data[AttackPatternNum].C_AttackName +  "�U��");

#endif

    }
}
