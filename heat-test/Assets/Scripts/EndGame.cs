using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            canvas.SetActive(true);
        }
    }

}
