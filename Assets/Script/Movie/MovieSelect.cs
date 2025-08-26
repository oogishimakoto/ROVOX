using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Video;
using static PlayerAction;

public class MovieSelect : MonoBehaviour
{
    [SerializeField] List<VideoClip> clip = new List<VideoClip>();

    VideoPlayer video;

    public int clipcount { get; set; } = 0;

    StageDataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();
        clipcount = dataManager.videoclipnumber;
        video = GetComponent<VideoPlayer>();

        video.clip = clip[clipcount];
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneTransition();
        }

    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        SceneTransition();
    }

    public void SceneTransition()
    {
        if (clipcount == 0)
        {
            FadeManager.Instance.LoadScene("Stage_3", 1);
        }

        if (clipcount == 1)
        {
            FadeManager.Instance.LoadScene("Stage_5", 1);
        }

        if (clipcount == 2)
        {
            FadeManager.Instance.LoadScene("Stage_7", 1);
        }

        if (clipcount == 3)
        {
            FadeManager.Instance.LoadScene("Stage_9", 1);
        }
    }
}
