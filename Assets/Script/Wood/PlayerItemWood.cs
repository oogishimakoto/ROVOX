using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemWood : MonoBehaviour
{
    GameObject playerwood;

    // Start is called before the first frame update
    void Start()
    {
        playerwood = GameObject.Find("mori");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform.name == "Player")
        {
            playerwood.transform.GetChild(3).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
