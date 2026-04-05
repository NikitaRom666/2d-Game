using UnityEngine;

public class SuperFixer : MonoBehaviour
{
    void Start()
    {
        // 1. Вимикаємо Static (щоб машина могла рухатися)
        gameObject.isStatic = false;

        // 2. Ставимо на дорогу (Y = -1.5) та розвертаємо вправо
        transform.position = new Vector3(transform.position.x, -1.5f, 0);
        transform.rotation = Quaternion.Euler(0, 0, -90);

        // 3. Налаштовуємо Rigidbody, щоб не падала
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.bodyType = RigidbodyType2D.Kinematic; // Ніякої гравітації
            rb.simulated = true;
        }

        // 4. Приклеюємо камеру, якщо вона ще не там
        Camera cam = Camera.main;
        if (cam != null) {
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0, 0, -10); // Центруємо камеру на авто
        }
        
        Debug.Log("Volvo відремонтована! Тисни стрілки.");
    }
}