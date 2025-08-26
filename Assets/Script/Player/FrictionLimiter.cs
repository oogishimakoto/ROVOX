using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionLimiter : MonoBehaviour
{
    public float maxFriction = 0.5f; // �ő喀�C�l
    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
        if (col != null)
        {
            // �V���������}�e���A�����쐬���A�ݒ肷��
            PhysicMaterial limitedFrictionMaterial = new PhysicMaterial();
            limitedFrictionMaterial.dynamicFriction = maxFriction;
            limitedFrictionMaterial.staticFriction = maxFriction;
            limitedFrictionMaterial.frictionCombine = PhysicMaterialCombine.Maximum;

            col.material = limitedFrictionMaterial;
        }
    }

    void Update()
    {
        if (col != null && col.material != null)
        {
            // ���I�ɖ��C�𐧌�����i�K�v�ɉ����āj
            col.material.dynamicFriction = Mathf.Min(col.material.dynamicFriction, maxFriction);
            col.material.staticFriction = Mathf.Min(col.material.staticFriction, maxFriction);
        }
    }

}
