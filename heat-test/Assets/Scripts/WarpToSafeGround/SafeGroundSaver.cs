using UnityEngine;
using System.Collections;

public class SafeGroundSaver : MonoBehaviour
{
    [SerializeField] private float saveFrequency = 3f; // Time in seconds between saves
    public Vector3 SafeGroundLocation { get; private set; } = new Vector3(0, 0, 0); // Default safe location
    private Coroutine safeGroundCoroutine;
    private GroundCheck groundCheck;
    public void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        // Start the coroutine to save the ground location at regular intervals
        safeGroundCoroutine = StartCoroutine(SaveGroundLocation());
        SafeGroundLocation = transform.position; // Initialize with the current position
    }
    private IEnumerator SaveGroundLocation()
    {
        float elapsedTime = 0f;
        while (elapsedTime < saveFrequency)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        if (groundCheck.isGrounded())
        {
            SafeGroundLocation = transform.position; // Save the current position as the safe ground location
        }
        safeGroundCoroutine = StartCoroutine(SaveGroundLocation()); // Restart the coroutine to save again after the specified frequency
    }
    public void WarpPlayerToSafeGround()
    {
        transform.position = SafeGroundLocation; // Move the player to the last saved safe ground location
    }

}
