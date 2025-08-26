using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    StageDataManager stagedatamanager;

    SceneChanger scene;

    // Start is called before the first frame update
    void Start()
    {
        stagedatamanager = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();

        scene = GetComponent<SceneChanger>();   


    }

    // Update is called once per frame
    void Update()
    {
        scene.SetSceneName(stagedatamanager.GetStageData().name);
    }
}
