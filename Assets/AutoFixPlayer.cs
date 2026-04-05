using UnityEngine;

[ExecuteInEditMode] // Працює навіть коли гра вимкнена
public class AutoFixPlayer : MonoBehaviour
{
    void Update()
    {
        // 1. Ставимо машину в лівий бік на дорогу
        transform.position = new Vector3(-7f, -1.7f, 0f);
        
        // 2. Розвертаємо носом направо
        transform.rotation = Quaternion.Euler(0, 0, -90f);
        
        // 3. Налаштовуємо фізику, щоб не крутилася
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 3f; // Щоб машина швидше падала (аркадний стиль)
        }
    }
}