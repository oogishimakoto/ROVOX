using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldStageRotation : MonoBehaviour
{
    // 回転速度
    public float rotationSpeed = 10.0f;

    // 回転する時間
    public float rotateDuration = 10.0f;

    // 停止する時間
    public float stopDuration = 5.0f;

    // 接触中のプレイヤーリスト
    private List<GameObject> touchingPlayers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // コルーチンを開始する
        StartCoroutine(RotateAndStop());
    }

    IEnumerator RotateAndStop()
    {
        while (true)
        {
            // 回転を開始
            float rotateEndTime = Time.time + rotateDuration;
            while (Time.time < rotateEndTime)
            {
                // ステージを回転させる
                float rotationThisFrame = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationThisFrame);

                // プレイヤーの位置と回転を更新
                foreach (GameObject player in touchingPlayers)
                {
                    if (player != null)
                    {
                        player.transform.RotateAround(transform.position, Vector3.up, rotationThisFrame);
                    }
                }

                yield return null;
            }

            // 回転を停止
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
