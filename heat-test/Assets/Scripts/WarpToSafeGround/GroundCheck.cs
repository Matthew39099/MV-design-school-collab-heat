using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float extraHeight = 0.25f;

    private RaycastHit groundHit;
    private CapsuleCollider coll;

    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
    }

    public bool isGrounded()
    {
        // Calculate the two sphere centres that define the capsule shape
        Vector3 point1 = coll.bounds.center + Vector3.up * (coll.height / 2 - coll.radius);
        Vector3 point2 = coll.bounds.center - Vector3.up * (coll.height / 2 - coll.radius);

        Physics.CapsuleCast(point1, point2, coll.radius, Vector3.down, out groundHit, extraHeight, whatIsGround);
        if (groundHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}