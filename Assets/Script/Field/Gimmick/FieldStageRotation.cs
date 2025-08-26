using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldStageRotation : MonoBehaviour
{
    // ��]���x
    public float rotationSpeed = 10.0f;

    // ��]���鎞��
    public float rotateDuration = 10.0f;

    // ��~���鎞��
    public float stopDuration = 5.0f;

    // �ڐG���̃v���C���[���X�g
    private List<GameObject> touchingPlayers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // �R���[�`�����J�n����
        StartCoroutine(RotateAndStop());
    }

    IEnumerator RotateAndStop()
    {
        while (true)
        {
            // ��]���J�n
            float rotateEndTime = Time.time + rotateDuration;
            while (Time.time < rotateEndTime)
            {
                // �X�e�[�W����]������
                float rotationThisFrame = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationThisFrame);

                // �v���C���[�̈ʒu�Ɖ�]���X�V
                foreach (GameObject player in touchingPlayers)
                {
                    if (player != null)
                    {
                        player.transform.RotateAround(transform.position, Vector3.up, rotationThisFrame);
                    }
                }

                yield return null;
            }

            // ��]���~
            yield return new WaitForSeconds(stopDuration);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            touchingPlayers.Add(collision.gameObject);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayers.Remove(collision.gameObject);
        }
    }
}
