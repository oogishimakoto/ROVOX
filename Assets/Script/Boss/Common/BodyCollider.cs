using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    [SerializeField] private List<BoxCollider> body;

    public List<BoxCollider> GetBodyCollider()
    {
        return body;
    }
}
