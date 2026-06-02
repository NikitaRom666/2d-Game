using UnityEngine;
using TMPro; // Підключаємо бібліотеку для роботи з красивим текстом

public class HUDManager : MonoBehaviour
{
    // Робимо скрипт Singleton, щоб до нього було легко звертатися з інших скриптів
    public static HUDManager instance;

    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("Speedometer Settings")]
    public TextMeshProUGUI speedText;
    public float baseSpeed = 80f; // Базова швидкість машинки

    void Awake()
    {
        // Налаштування Singleton
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Оновлюємо текст балів на старті
        UpdateScoreText();
    }

    void Update()
    {
        // Симуляція "живого" спідометра: додаємо невелике коливання швидкості для краси
        float currentSpeed = baseSpeed + Mathf.PingPong(Time.time * 3f, 5f);
        
        // Виводимо швидкість цілим числом
        speedText.text = Mathf.RoundToInt(currentSpeed).ToString() + " km/h";
    }

    // Цей метод викликатиметься, коли ти об'їжджаєш машину
    public void AddScore()
    {
        score += 1;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}