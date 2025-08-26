using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;

public class RoketReaction : MonoBehaviour
{
    Roket roketComp;
    [SerializeField] float MaxScale;
    [SerializeField] float lerpSpeed;

    [SerializeField] float moveSpeed = 5.0f;

    Vector3 moveVec;
     public bool isShot;
    // Start is called before the first frame update
    void Start()
    {
        roketComp = transform.parent.GetComponent<Roket>();
        roketComp.SetPlayerObj(transform.root.transform.GetComponent<Enemy_PlayerManager>().GetPlayerObj());
        roketComp.enabled = false;
        isShot = false;
    }

    private void Update()
    {
        if (isShot)
        {
            transform.parent.transform.localScale = Vector3.Lerp(this.transform.parent.transform.localScale, Vector3.one * MaxScale, lerpSpeed * Time.deltaTime);
            transform.parent.transform.position += moveVec * moveSpeed * Time.deltaTime;
        }
    }

    public void ShotBullet(Vector3 _moveVec)
    {

        ParentConstraint constraint = transform.parent.transform.GetComponent<ParentConstraint>();

        if (constraint != null && constraint.constraintActive)
        {
            constraint.constraintActive = false;

            isShot = true;
            moveVec = _moveVec;
            transform.parent.GetComponent<Collider>().isTrigger = true;
            roketComp.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ParentConstraint constraint = transform.parent.transform.GetComponent<ParentConstraint>();

            if (constraint != null && !constraint.constraintActive)
            {

                if (isShot)
                {
                    roketComp.enabled = true;
                    roketComp.SetMoveDirection();
                    transform.parent.GetComponent<Collider>().isTrigger = false;
                    isShot = false;
                }
            }
        }
    }
}
