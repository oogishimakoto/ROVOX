using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionLimiter : MonoBehaviour
{
    public float maxFriction = 0.5f; // 最大摩擦値
    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
        if (col != null)
        {
            // 新しい物理マテリアルを作成し、設定する
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
            // 動的に摩擦を制限する（必要に応じて）
            col.material.dynamicFriction = Mathf.Min(col.material.dynamicFriction, maxFriction);
            col.material.staticFriction = Mathf.Min(col.material.staticFriction, maxFriction);
        }
    }

}
