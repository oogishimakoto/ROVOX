using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] GameObject camera;
    [SerializeField] LineRenderer line;
    PlayerAction player;
    PlayerInfo playerinfo;
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject lockOnUIList;

    OptionCursor cursor;

    private void Start()
    {
        if(player == null)
        {
            player= GetComponent<PlayerAction>();
        }

        if (playerinfo == null)
        {
            playerinfo = GetComponent<PlayerInfo>();
        }

        cursor = GameObject.Find("OptionCanvas").transform.GetChild(0).GetComponent<OptionCursor>();

    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetPlayerState() == PlayerAction.State.CHARGE)
        {
            line.gameObject.SetActive(true);

            var positions = new Vector3[]{
                 new Vector3(0, 0, 0),               // �J�n�_
                 new Vector3(0, 0, 0), };               // �I���_

            float movespeed = 0;
            Vector3 endpos = new Vector3(0, 0, 0);
            if (player.GetChargeState() == PlayerAction.Charge.charge1)
            {
                movespeed = 20 * (1 - playerinfo.shotspeed1) + 30 * playerinfo.shotspeed1;
                endpos = startPos.position + camera.gameObject.transform.forward.normalized * movespeed;
                positions = new Vector3[]{
                 startPos.position,               // �J�n�_
                  endpos};               // �I���_

            }

            if (player.GetChargeState() == PlayerAction.Charge.charge2)
            {
                movespeed = 20 * (1 - playerinfo.shotspeed2) + 30 * playerinfo.shotspeed2;
                endpos = startPos.position + camera.gameObject.transform.forward.normalized * movespeed;
                positions = new Vector3[]{
                startPos.position,               // �J�n�_
                endpos};              // �I���_

            }

            if (player.GetChargeState() == PlayerAction.Charge.charge3)
            {
                movespeed = 20 * (1 - playerinfo.shotspeed3) + 30 * playerinfo.shotspeed3;
                endpos = startPos.position + camera.gameObject.transform.forward.normalized * movespeed;
                positions = new Vector3[]{
                 startPos.position,               // �J�n�_
                 endpos};               // �I���_
            }

            // ���������ꏊ���w�肷��
            line.SetPositions(positions);

            //������������擾
            Ray ray = new Ray(startPos.position, endpos - startPos.position);
            RaycastHit hit;

            //���O���郌�C���[���w��
            int layer7Mask = 1 << 7;
            int layer8Mask = 1 << 8;
            int layer11Mask = 1 << 11;
            int combinedMask = layer7Mask | layer8Mask | layer11Mask;
            int layerMask = ~combinedMask;

            if (Physics.Raycast(ray, out hit, (endpos - startPos.position).magnitude, layerMask) && !cursor.gameObject.activeSelf)
            {
                //�`���[�W�i�K��1�ȏ�̎�
                if(player.GetChargeState() != PlayerAction.Charge.charge0)
                    lockOnUIList.gameObject.SetActive(true);
                lockOnUIList.transform.position = hit.point;
                lockOnUIList.transform.position += hit.normal * 0.01f;
                lockOnUIList.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                // ���ʂ̌��̌������l�����āA�K�v�ɉ����Ēǉ��̉�]��������
                // �����ł́A���ʂ�90�x��]�����āA�@���ɑ΂��Đ����������ɂ��܂�
                lockOnUIList.transform.Rotate(Vector3.right, 90);
            }
            else
            {
                lockOnUIList.gameObject.SetActive(false);
            }
        }
        else
        {
            lockOnUIList.gameObject.SetActive(false);
            line.gameObject.SetActive(false);
        }
    }
}
