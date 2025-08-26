using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerDeath : MonoBehaviour
{
    PlayerInfo info;
    PlayerAction action;

    SceneChanger sceneChanger;

    private bool changescene = false;

    // Start is called before the first frame update
    void Start()
    {
        info = transform.root.GetComponent<PlayerInfo>();
        action = transform.root.GetComponent<PlayerAction>();

        sceneChanger = transform.root.GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (info.HP <= 0)
        {
            action.SetPlayerState(PlayerAction.State.DEATH);
            info.HP = 0;
        }
    }

    public void PlayerStateDeath()
    {
        if (!changescene)
        {
            sceneChanger.SceneChage();
            changescene = true;
        }
    }
}
