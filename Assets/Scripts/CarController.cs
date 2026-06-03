    using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction accelerateAction;
    private InputAction brakeAction;

    [Header("Movement")]
    public float speed = 6f;

    // межі дороги
    public float minX = -3.5f;
    public float maxX = 3.5f;

    [Header("Car Speed")]
    public float currentSpeed = 8f;
    public float minSpeed = 3f;
    public float maxSpeed = 20f;

    public float acceleration = 10f;
    public float brakePower = 15f;

    [Header("Visual Effect")]
    public float visualMoveAmount = 1f;

    [Header("UI")]
    public GameObject gameOverPanel;

    private bool isDead = false;
    private float startY;

    void Awake()
    {
        moveAction = new InputAction("Move");

        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");

        accelerateAction =
            new InputAction("Accelerate",
            binding: "<Keyboard>/w");

        brakeAction =
            new InputAction("Brake",
            binding: "<Keyboard>/space");
    }

    void Start()
    {
        startY = transform.position.y;
    }

    void OnEnable()
    {
        moveAction.Enable();
        accelerateAction.Enable();
        brakeAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        accelerateAction.Disable();
        brakeAction.Disable();
    }

    void Update()
    {
        if (isDead)
            return;

        // A / D
        float move = moveAction.ReadValue<float>();

        Vector3 pos = transform.position;

        pos.x += move * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        // W
        if (accelerateAction.IsPressed())
        {
            currentSpeed +=
                acceleration * Time.deltaTime;
        }

        // SPACE
        if (brakeAction.IsPressed())
        {
            currentSpeed -=
                brakePower * Time.deltaTime;
        }

        currentSpeed =
            Mathf.Clamp(
                currentSpeed,
                minSpeed,
                maxSpeed
            );

        // Візуальне зміщення машини
        float normalized =
            Mathf.InverseLerp(
                minSpeed,
                maxSpeed,
                currentSpeed
            );

        pos.y =
            startY +
            normalized * visualMoveAmount;

        transform.position = pos;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyCar"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isDead = true;

        Debug.Log("GAME OVER");

        gameOverPanel.SetActive(true);

        FindFirstObjectByType<DistanceManager>()
            .StopCounting();

        Time.timeScale = 0f;
    }
}