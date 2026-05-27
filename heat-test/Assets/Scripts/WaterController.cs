using UnityEngine;

public class WaterController : MonoBehaviour
{
    GameObject waterWarning;
    void Awake()
    {
        waterWarning = GameObject.FindWithTag("DamageWarning");
        if (waterWarning == null)
            Debug.LogError("No GameObject with tag 'WaterWarning' found!");
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with water");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit water");
            ProgressBarController.Instance.Decrease(Time.deltaTime * 2f);
            waterWarning.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waterWarning.SetActive(false);
        }
    }
    
}
