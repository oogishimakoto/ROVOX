using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [SerializeField] GameObject flg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            flg.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        flg.gameObject.SetActive(false);

    }
}
