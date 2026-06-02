using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private InputAction moveAction;

    [Header("Movement")]
    public float speed = 6f;

    // межі дороги по горизонталі (вліво-вправо)
    public float minX = -3.5f;
    public float maxX = 3.5f;

    // межі дороги по вертикалі (вперед-назад), щоб не виїхати за екран
    
    public float minY = -4.0f; 
    public float maxY = 4.0f;

    [Header("UI")]
    public GameObject gameOverPanel;

    private bool isDead = false;

    void Awake()
    {
        // Створюємо дію для руху 
        moveAction = new InputAction("Move");

        // Тепер машинка може їхати не тільки вліво/вправо (A/D), а й вперед/назад (W/S)
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")    // Газ
            .With("Down", "<Keyboard>/s")  // Гальмо (рух назад по екрану)
            .With("Left", "<Keyboard>/a")  // Вліво
            .With("Right", "<Keyboard>/d"); // Вправо
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

        // Зчитуємо рух відразу по двох осях (X та Y)
        Vector2 move = moveAction.ReadValue<Vector2>();

        Vector3 pos = transform.position;

        // Додаємо рух як по горизонталі (move.x), так і по вертикалі (move.y)
        pos.x += move.x * speed * Time.deltaTime;
        pos.y += move.y * speed * Time.deltaTime;

        // обмеження дороги: не даємо вилетіти ні по боках, ні зверху/знизу
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

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

        // стоп гри
        Time.timeScale = 0f;
    }
}