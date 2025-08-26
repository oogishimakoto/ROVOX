using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string SceneName;

    SoundActive option;

    StageDataManager stagedatalist;

    public void SetSceneName(string name) { SceneName = name; }

    private void Start()
    {
        stagedatalist = GameObject.Find("StageDataManager").GetComponent<StageDataManager>();

        option = transform.root.GetComponent<SoundActive>();
    }
    public void OnChage(InputAction.CallbackContext context)
    {
        //‰Ÿ‚µ‚½‚Æ‚«
        if (context.performed)
        {

            FadeManager.Instance.LoadScene(SceneName, 1);
        }
    }

    public void SceneChage()
    {

        FadeManager.Instance.LoadScene(SceneName, 1);
    }

    public void NextSceneChage()
    {
        if(stagedatalist != null)
        {

            if (stagedatalist.GetNextStageData().name != "Stage_3" ||
                stagedatalist.GetNextStageData().name != "Stage_5" ||
                stagedatalist.GetNextStageData().name != "Stage_7" ||
                stagedatalist.GetNextStageData().name != "Stage_9")
            {
                FadeManager.Instance.LoadScene("Movie", 1);

            }
            else
            {
                FadeManager.Instance.LoadScene(stagedatalist.GetNextStageData().name, 1);

            }

        }
    }

    public void OptionSceneChage()
    {
        option.ChangeOption();
        FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 1);
    }

    public void OptionSelectSceneChage()
    {
        option.ChangeOption();
        FadeManager.Instance.LoadScene(SceneName, 1);
    }
}
