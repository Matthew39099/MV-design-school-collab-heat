using UnityEngine;
using UnityEngine.UI;

public class WaterController : MonoBehaviour
{
    Image waterWarning;

    void Awake()
    {
        waterWarning = GameObject.FindWithTag("DamageWarning").GetComponent<Image>();
        if (waterWarning == null)
            Debug.LogError("No Image component found on WaterWarning object!");
        waterWarning.color = new Color(0.86f, 0.08f, 0.24f, 0f); // start fully transparent
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit water");
            ProgressBarController.Instance.Decrease(Time.deltaTime * 2f);
            waterWarning.color = new Color(0.86f, 0.08f, 0.24f, 0.5f); // fully visible
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waterWarning.color = new Color(0.86f, 0.08f, 0.24f, 0f); // fully transparent
        }
    }
}