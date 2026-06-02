using UnityEngine;
using TMPro;

public class SmartScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        // 1. АВТОМАТИЧНО шукаємо текст на екрані
        GameObject textObj = GameObject.Find("Score: 0");
        if (textObj != null) 
        {
            scoreText = textObj.GetComponent<TextMeshProUGUI>();
        }

        // 2. АВТОМАТИЧНО шукаємо гравця
        GameObject player = GameObject.Find("CarPlayer");
        if (player != null)
        {
            // 3. АВТОМАТИЧНО створюємо невидиму лінію позаду машинки
            GameObject triggerObj = new GameObject("AutoPassTrigger");
            triggerObj.transform.parent = player.transform;
            triggerObj.transform.localPosition = new Vector3(0, -3.5f, 0); // ставимо позаду

            // Додаємо колайдер
            BoxCollider2D box = triggerObj.AddComponent<BoxCollider2D>();
            box.isTrigger = true;
            box.size = new Vector2(10f, 0.5f); // розтягуємо на всю дорогу

            // Вішаємо логіку зіткнень
            AutoTriggerLogic logic = triggerObj.AddComponent<AutoTriggerLogic>();
            logic.manager = this;
        }
    }

    public void AddPoint()
    {
        score++;
        if (scoreText != null) 
        {
            scoreText.text = "Score: " + score;
        }
    }
}

// Це внутрішній скрипт, він працюватиме непомітно
public class AutoTriggerLogic : MonoBehaviour
{
    public SmartScore manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        // АВТОМАТИЧНО перевіряємо, чи це ворожа машина (без тегів, просто по назві)
        if (other.gameObject.name.Contains("car 1"))
        {
            manager.AddPoint();
        }
    }
}