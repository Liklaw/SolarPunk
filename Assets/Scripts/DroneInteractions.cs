using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneInteractions : MonoBehaviour
{
    // Input for drone movement
    public InputActionReference moveActions;

    // Drone movement speed
    public float DroneSpeed = 5.0f;

    // Position where seeds are spawned
    public Transform spawnPoint;

    // Available seed prefabs
    public GameObject[] seedPrefabs;

    // Input for dropping seeds
    public InputActionReference throwSeeds;

    // Input for watering
    public InputActionReference wateringAction;

    // Current watering state
    public bool isWatering = false;

    // Watering particle effect
    public ParticleSystem Watering;

    // Reference to the score manager
    public ScoreManager scoreManager;

    // Input action used to open and close the menu
    public InputActionReference escapeAction;
    // Mini menu panel
    public GameObject menuPanel;

    // Indicates whether the menu is currently open
    private bool isMenuOpen = false;

    // Reinitialisation of players score
    private void Awake()
    {
        ScoreManager.ResetScore();
        ResumeGame();
    }
    
    private void Start()
    { 
        menuPanel.SetActive(false);
    }
    // Controls drone movement
    private void Update()
    {
        Vector2 stickDirection = moveActions.action.ReadValue<Vector2>();

        Vector3 OvniDirection = new Vector3(-stickDirection.x, 0, -stickDirection.y);

        transform.Translate(OvniDirection * (Time.deltaTime * DroneSpeed));
    }

    // Spawns a random seed
    public void DropSeed(InputAction.CallbackContext context)
    {
        if (isWatering) return;

        int randomIndex = Random.Range(0, seedPrefabs.Length);

        GameObject seed = Instantiate(seedPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);

        SeedType seedType = seed.GetComponent<SeedType>();

        // Passes drone and score manager references to the seed
        seedType.drone = this;
        seedType.scoreManager = this.scoreManager;
    }

    // Enables or disables watering
    private void ToggleWatering(InputAction.CallbackContext context)
    {
        isWatering = !isWatering;

        if (isWatering)
        {
            GetComponent<AudioSource>().Play();
            Watering.Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            Watering.Stop();
        }

    }

    // Subscribes to input actions
    private void OnEnable()
    {
        throwSeeds.action.performed += DropSeed;
        throwSeeds.action.Enable();

        wateringAction.action.performed += ToggleWatering;
        wateringAction.action.Enable();

        escapeAction.action.performed += ToggleMenu;
        escapeAction.action.Enable();
    }

    // Unsubscribes from input actions
    private void OnDisable()
    {
        throwSeeds.action.performed -= DropSeed;
        throwSeeds.action.Disable();

        wateringAction.action.performed -= ToggleWatering;
        wateringAction.action.Disable();

       
        escapeAction.action.performed -= ToggleMenu;
        escapeAction.action.Disable();
    }

    // Opens or closes the mini menu
    private void ToggleMenu(InputAction.CallbackContext context)
    {
        isMenuOpen = !isMenuOpen;

        // Show or hide the menu panel
        menuPanel.SetActive(isMenuOpen);

        // Pause or resume the game
        if(isMenuOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }

        // Disable gameplay actions when the menu is open
        if (isMenuOpen)
        {
            moveActions.action.Disable();
            throwSeeds.action.Disable();
            wateringAction.action.Disable();
        }
        // Enable gameplay actions when the menu is closed
        else
        {
            moveActions.action.Enable();
            throwSeeds.action.Enable();
            wateringAction.action.Enable();
        }
    }

    // Closes the menu and resumes the game
    public void ResumeGame()
    {
       
        isMenuOpen = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1f;

        moveActions.action.Enable();
        throwSeeds.action.Enable();
        wateringAction.action.Enable();
        
    }
}