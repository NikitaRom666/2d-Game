using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction accelerateAction;
    private InputAction brakeAction;

    [Header("Car Sprites")]
    public Sprite normalCar;
    public Sprite frontDamageCar;
    public Sprite sideDamageCar;
    public Sprite rearDamageCar;

    private SpriteRenderer sr;

    [Header("Movement")]
    public float speed = 6f;

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
        sr = GetComponent<SpriteRenderer>();

        startY = transform.position.y;

        if (normalCar != null)
            sr.sprite = normalCar;
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

        float move = moveAction.ReadValue<float>();

        Vector3 pos = transform.position;

        pos.x += move * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        if (accelerateAction.IsPressed())
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        if (brakeAction.IsPressed())
        {
            currentSpeed -= brakePower * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(
            currentSpeed,
            minSpeed,
            maxSpeed
        );

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
        if (!other.CompareTag("EnemyCar"))
            return;

        ShowDamage(other.transform.position);

        isDead = true;

        Invoke(nameof(FinishGame), 1f);
    }

    void FinishGame()
    {
        gameOverPanel.SetActive(true);

        FindFirstObjectByType<DistanceManager>()
            .StopCounting();

        Time.timeScale = 0f;
    }

    void ShowDamage(Vector3 enemyPos)
    {
        Vector3 hitDir =
            enemyPos - transform.position;

        float absX = Mathf.Abs(hitDir.x);
        float absY = Mathf.Abs(hitDir.y);

        if (absY > absX)
        {
            if (hitDir.y > 0)
            {
                sr.sprite = frontDamageCar;
            }
            else
            {
                sr.sprite = rearDamageCar;
            }
        }
        else
        {
            sr.sprite = sideDamageCar;

            if (hitDir.x > 0)
            {
                sr.flipX = true;   // удар справа
            }
            else
            {
                sr.flipX = false;  // удар зліва
            }
        }
    }
}