using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeatMod;
    [SerializeField] private float moveHeatMod;

    [Header("Mineables and Furnace")]

    [SerializeField] public TextMeshProUGUI coalCounterText;
    private float coalNum = 0f;
    [SerializeField] public float coalIncrement = 10f;

    [SerializeField] public TextMeshProUGUI saltCounterText;
    [SerializeField] public float saltIncrement = 10f;
    private float saltNum = 0f;

    [SerializeField] public GameObject furnace;
    [SerializeField] ProgressBarController progress;

    [SerializeField] public GameObject minePrompt;


    [Header("Misc")]
    [SerializeField] public bool hasDrill = false;
    [SerializeField] public GameObject drill;
    [SerializeField] public bool hasPick = false;
    [SerializeField] public GameObject pick;
    [SerializeField] public Light flame;
    bool isInRange = false;

    private Collider currentInteractable;

    private CharacterController controller;
    private Vector3 moveInput;
    private Vector3 velocity;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (!hasDrill)
        {
            drill.SetActive(false);
        }

        if (!hasPick)
        {
            pick.SetActive(false);
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //Debug.Log($"Move input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Debug.Log($"Jumping: {context.performed} - Is Grounded: {controller.isGrounded}");
        if(context.performed && controller.isGrounded)
        {
            AudioController.Instance.PlaySound("playerJump");
            Debug.Log("Jumped!");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Hit Interact Button");

        if (!context.performed) return;

        if (currentInteractable == null)
        {
            return;
        }

        //Coal
        if (currentInteractable.CompareTag("Coal") && hasPick && isInRange)
        {
            coalNum += coalIncrement;
            CoalController coalController = currentInteractable.GetComponent<CoalController>();
            coalController.totalCoal -= coalIncrement;

            if (coalController.totalCoal <= 0)
            {
                minePrompt.SetActive(false);
            }
        }

        //Salt
        else if (currentInteractable.CompareTag("Salt") && hasPick && isInRange)
        {
            saltNum += saltIncrement;
            SaltController saltController = currentInteractable.GetComponent<SaltController>();
            saltController.totalSalt -= coalIncrement; 

            if (saltController.totalSalt <= 0)
            {
                minePrompt.SetActive(false);
            }
        }

        //Furnace
        else if (currentInteractable.CompareTag("Furnace"))
        {
            if (coalNum > 0)
            {
                progress.Increase(coalNum);
                coalNum = 0f;
            }
        }
    }

    public void PickUpDrill()
    {
        hasDrill = true;
        drill.SetActive(true);
        Debug.Log("Drill is true");
    }

    public void PickUpPick()
    {
        hasPick = true;
        pick.SetActive(true);
    }

    void Update()
    {
        //Camera Angles to offset the player movement based on camera angles so that w still means go up
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        controller.Move(move * speed * Time.deltaTime);


        //Jump logic
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Coal number
        coalCounterText.SetText($"{coalNum.ToString()}");
        saltCounterText.SetText($"{saltNum.ToString()}");


        //Heat meter reduction
        if (velocity.y > 0.01f)
        {
            //Jump makes it decrease faster
            progress.Decrease(Time.deltaTime * jumpHeatMod);
            
        }
        else if (moveInput.sqrMagnitude > 0.01f)
        {
            // Decreases when player is moving
            progress.Decrease(Time.deltaTime * moveHeatMod);
        }

        updateLight();
        
    }


    public void OnTriggerEnter(Collider other)
    {
        //Pick up coal
        if (other.gameObject.CompareTag("Coal") && hasPick == true)
        {
            AudioController.Instance.PlaySound("oreMining");
            Debug.Log("Coal tag works");
            coalNum += coalIncrement;
            coalController.totalCoal -= coalIncrement;
        }

        //Pick up salt
        if (other.gameObject.CompareTag("Salt") && hasPick == true)
        {
            AudioController.Instance.PlaySound("oreMining");
            Debug.Log("Salt tag works");
            saltNum += saltIncrement;
            saltController.totalSalt -= saltIncrement;
        }


        //Deposit coal in furnace, refill heat meter and upgrade meter max by how much coal is deposited
        if (other.gameObject.CompareTag("Furnace"))
        {
            currentInteractable = other;

            if (coalNum > 0)
            {
                AudioController.Instance.PlaySound("furnaceEnter");
                Debug.Log("Player has coal");
                progress.maxValue += coalNum;
                progress.ResetBar();
                coalNum = 0f;
            }
        }
    }

        }

        // Furnace room box collider, turn on room ambience
        if (other.gameObject.CompareTag("FurnaceRoom"))
        {
            AudioController.Instance.PlaySound("furnaceRoom");
            Debug.Log("Furnace room collider works");
        }
        // Caverns Area box collider for ambience on
        if (other.gameObject.CompareTag("CavernsArea"))
        {
            AudioController.Instance.PlaySound("cavernsArea");
            Debug.Log("Caverns room collider works");
        }
        
    }

     public void OnTriggerExit(Collider other)
    {
        // Leave Furnace room 
        if (other.gameObject.CompareTag("FurnaceRoom"))
        {
            AudioController.Instance.StopSound("furnaceRoom");
        }

        if (other.gameObject.CompareTag("Salt"))
        {
            minePrompt.SetActive(false);
            isInRange = false;

        }
    }

    void Die()
    {
        PlayerController.Instance.gameObject.SetActive(false);
    }

    //Updates the light based on heat level. If gets to 0, dies.
    void updateLight()
    {
        flame.intensity = (progress.CurrentValue / progress.maxValue) * 50;
        flame.range = (progress.CurrentValue / progress.maxValue) * 50;

        if (flame.intensity <= 0)
        {
            Die();
        }
    }
}