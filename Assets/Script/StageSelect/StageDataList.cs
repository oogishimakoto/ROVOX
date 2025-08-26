using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataList : MonoBehaviour
{
    public List<Sprite> gimmickSprite;
    public List<StageData> List;
}

[Serializable]
public class StageData
{
    [Tooltip("�J�ڂ���V�[���̖��O")] public string LinkSceneName;
    [Tooltip("�X�e�[�W�C���[�W�̃I�u�W�F�N�g")] public Sprite StageObj;
    [Tooltip("�\������X�e�[�W�̖��O")] public string stagename;
    [Tooltip("�X�e�[�W�̃M�~�b�NUI�̔ԍ�")] public List<int> Gimmick;
    [Tooltip("�R�A��")] public int CoreNum;

}
