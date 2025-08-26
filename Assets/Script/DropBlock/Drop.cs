using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Drop : MonoBehaviour
{
    private GameObject player;

    private GameObject field;

    private float lifetime = 10;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        field = GameObject.Find("Field");
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(gameObject,lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.transform.GetComponent<PlayerMove>().i_ItemCount++;

            Destroy(gameObject);
        }

        if (collision.gameObject.name == "BOSSFIeld")
        {
            field.GetComponent<DropItemCount>().SetDropItemCount();
            field.GetComponent<DropItemCount>().SetDropItem(true);
            Destroy(gameObject);
        }
    }
}
