using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTree : MonoBehaviour
{

    [SerializeField] GameObject Field;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.y = Field.transform.localScale.y / 65;

        transform.position = position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.P))           
        {
            //âüÇµÇΩÇ∆Ç´ÉJÉEÉìÉgèâä˙âª
            if (other != null && other.transform.name == "Player")
            {
                this.gameObject.transform.GetChild(1).gameObject.SetActive(!this.gameObject.transform.GetChild(1).gameObject.activeSelf);


            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }

}
