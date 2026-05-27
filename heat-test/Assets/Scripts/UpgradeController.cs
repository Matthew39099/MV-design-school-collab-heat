using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] public GameObject upgradeCanvas;  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MovementUpgrade()
    {
        if(playerController.saltNum < 50f)
        {
            Debug.Log("Not enough salt to purchase upgrade!");
            return;
        }
        else
        {
            playerController.saltNum -= 50f;
            playerController.speed += 5f;
        Debug.Log("Movement Upgrade Purchased! Current Speed: " + playerController.speed);
        }
    }
    public void closeUpgrade()
    {
        upgradeCanvas.SetActive(false);
    }
}
