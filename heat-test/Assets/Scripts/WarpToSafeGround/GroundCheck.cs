using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;

    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public bool isGrounded()
    {
        // Account for lossy scale since CharacterController values are unscaled
        float scaledRadius = cc.radius * transform.lossyScale.x;
        float scaledHeight = cc.height * transform.lossyScale.y;

        Vector3 feetPos = transform.position + cc.center - Vector3.up * (scaledHeight / 2 - scaledRadius);

        bool hit = Physics.SphereCast(
            feetPos,
            scaledRadius * 0.9f,
            Vector3.down,
            out RaycastHit groundHit,
            extraHeight,
            whatIsGround
        );

        Debug.DrawRay(feetPos, Vector3.down * (extraHeight + scaledRadius), hit ? Color.green : Color.red, 0.1f);
        Debug.Log($"Feet pos: {feetPos} | Scaled radius: {scaledRadius} | Hit: {hit}");
        return hit;
    }
}