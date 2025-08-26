using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private float DeleteTime = 3;

    float StartTime = 0;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;

        text = transform.GetChild(0).GetComponent<Text>();  
        //Invoke("Delete" , DeleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - StartTime >= DeleteTime)
        {
            Color color= text.color;
            color.a -= 0.02f;
            text.color = color;
            if(text.color.a <= 0)
               Delete();
        }
    }

    private void Delete()
    {
        gameObject.SetActive(false);
    }
}
