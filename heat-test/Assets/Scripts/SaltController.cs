using TMPro;
using UnityEngine;

public class SaltController : MonoBehaviour
{
    [SerializeField] public float totalSalt;

    public static SaltController Instance;

    void Update()
    {
        if (totalSalt <= 0)
        {
            Debug.Log("0 Salt Left");
            Destroy(gameObject);
        }
    }
}
