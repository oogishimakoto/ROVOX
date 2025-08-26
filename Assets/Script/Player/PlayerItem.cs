using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public GameObject player;
    public Text TextFrame;
    public Text TextKill;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player"))
        {
            player = GameObject.Find("Player");

        }
        else
        {
            Debug.Log("ÉvÉåÉCÉÑÅ[Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TextFrame.text = string.Format("{0}", player.transform.GetComponent<PlayerAction>().i_ItemCount);
        //TextKill.text = string.Format("{0}", player.transform.GetComponent<PlayerAction>().GetKillenemyCount());
    }
}
