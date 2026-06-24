using UnityEngine;

public class DeathPitController : MonoBehaviour
{
    private SafeGroundSaver safeGroundSaver;

    private void Start()
    {
        safeGroundSaver = GameObject.FindGameObjectWithTag("Player").GetComponent<SafeGroundSaver>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            safeGroundSaver.WarpPlayerToSafeGround();
            Debug.Log("Player respawned");
        }
    }
}