using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotGauge : MonoBehaviour
{
    [SerializeField] GameObject chargelist;
    List<GameObject> ChargeStep = new List<GameObject>();

    PlayerAction action;

    int step = 0;

    // Start is called before the first frame update
    void Start()
    { 

        action = GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < 3; i++)
        {

            chargelist.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (action != null)
        {
            if (action.GetChargeState() == PlayerAction.Charge.charge1)
            {
                step = 1;
            }

            if (action.GetChargeState() == PlayerAction.Charge.charge2)
            {
                step = 2;
            }

            if (action.GetChargeState() == PlayerAction.Charge.charge3)
            {
                step = 3;
            }

            if (action.GetPlayerState() != PlayerAction.State.CHARGE)
            {
                step = 0;
            }
        }

        if(action.GetPlayerState() == PlayerAction.State.CHARGE)
        {
            chargelist.gameObject.SetActive(true);


            for (int i = 0; i < step; i++)
            {
                chargelist.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
        else
        {
           chargelist.gameObject.SetActive(false);
        }
    }
}
