using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowField : MonoBehaviour
{
    [SerializeField] GameObject Field;

    [SerializeField] int GrowDropCount  = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Field != null)
        {
            if(Field.GetComponent<DropItemCount>().GetDropItemCount() != 0 && Field.GetComponent<DropItemCount>().GetDropItem())
            {
                if (Field.GetComponent<DropItemCount>().GetDropItemCount() % GrowDropCount == 0)
                {
                    Vector3 position = this.transform.localScale;
                    position.y += 50;
                    transform.localScale = position;
                    Field.GetComponent<DropItemCount>().SetDropItem(false);
                }
            }
        }
    }
}
