using UnityEngine;

public class SafeGroundSaver : MonoBehaviour
{
    [SerializeField] private float saveFrequency = 3f;
    public Vector3 SafeGroundLocation { get; private set; }
    private GroundCheck groundCheck;
    private CharacterController cc;

    public void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        cc = GetComponent<CharacterController>();
        SafeGroundLocation = transform.position;
        StartCoroutine(SaveGroundLocation());
    }

    private System.Collections.IEnumerator SaveGroundLocation()
    {
        while (true)
        {
            yield return new WaitForSeconds(saveFrequency);
            if (groundCheck.isGrounded())
            {
                SafeGroundLocation = transform.position;
                Debug.Log("Safe ground saved at: " + SafeGroundLocation);
            }
        }
    }

    public void WarpPlayerToSafeGround()
    {
        Debug.Log("Warping player to: " + SafeGroundLocation);
        // Must disable CharacterController before moving, otherwise it blocks teleport
        cc.enabled = false;
        transform.position = SafeGroundLocation;
        cc.enabled = true;
    }
}