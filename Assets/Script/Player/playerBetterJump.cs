using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBetterJump : MonoBehaviour
{
    private Rigidbody rb;
    public float fallMultiplier = 2.5f;
    public float shortJumpMultiplier = 2.0f;

    private KeyCode jumpKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(jumpKey))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (shortJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
