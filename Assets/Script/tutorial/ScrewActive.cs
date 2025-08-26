using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrewActive : MonoBehaviour
{
    [SerializeField] int MissionCount = 0;

    TutorialTextManager tutorial;

    // Start is called before the first frame update
    void Start()
    {
        //チュートリアル用
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = GameObject.Find("Tutorial").GetComponent<TutorialTextManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MissionCount <= tutorial.GetNowCount())
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
