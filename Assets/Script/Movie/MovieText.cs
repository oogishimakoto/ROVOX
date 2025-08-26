using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieText : MonoBehaviour
{
    private Text text;

    [SerializeField] private List<string> newtext = new List<string>();

    private int clipcount = 0;

    StageDataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        clipcount = dataManager.videoclipnumber;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SceneTransition();

    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        SceneTransition();
    }

    public void SceneTransition()
    {
        if (clipcount == 0)
        {
            text.text = newtext[0];
        }

        if (clipcount == 1)
        {
            text.text = newtext[1];
        }

        if (clipcount == 2)
        {
            text.text = newtext[2];
        }

        if (clipcount == 3)
        {
            text.text = newtext[3];
        }
    }

}
