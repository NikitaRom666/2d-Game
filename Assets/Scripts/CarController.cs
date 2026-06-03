using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private InputAction moveAction;

    [Header("Movement")]
    public float speed = 6f;

    // межі дороги
    public float minX = -3.5f;
    public float maxX = 3.5f;

    [Header("UI")]
    public GameObject gameOverPanel;

    private bool isDead = false;

    void Awake()
    {
        moveAction = new InputAction("Move");

        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        // якщо програли — не рухаємось
        if (isDead)
            return;

        float move = moveAction.ReadValue<float>();

        Vector3 pos = transform.position;

        pos.x += move * speed * Time.deltaTime;

        // обмеження дороги
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
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

        // показати меню програшу
        gameOverPanel.SetActive(true);

        FindFirstObjectByType<DistanceManager>()
            .StopCounting();

        // стоп гри
        Time.timeScale = 0f;
    }
}