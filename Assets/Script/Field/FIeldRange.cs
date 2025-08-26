using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FIeldRange : MonoBehaviour
{
    [SerializeField] bool removeExistingColliders = true;
    [SerializeField] PhysicMaterial mat;
    private void Start()
    {
        CreateInvertedMeshCollider();
    }

    public void CreateInvertedMeshCollider()
    {
        if (removeExistingColliders)
            RemoveExistingColliders();

        InvertMesh();

        gameObject.AddComponent<MeshCollider>();
        if(mat != null)
        {
            gameObject.GetComponent<MeshCollider>().material = mat;
        }
    }

    private void RemoveExistingColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
            DestroyImmediate(colliders[i]);
    }

    private void InvertMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
}
