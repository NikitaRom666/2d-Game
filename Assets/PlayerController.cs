using UnityEngine; // Обов'язково цей рядок!

public class PlayerController : MonoBehaviour // Назва має бути PlayerController
{
    public float forwardSpeed = 8f;
    public float sideSpeed = 10f;

    void Update()
    {
        // Рух вперед
        transform.Translate(Vector2.right * forwardSpeed * Time.deltaTime);

        // Керування стрілками (Вгору/Вниз — це вліво/вправо для твоєї траси)
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector2.up * sideSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector2.down * sideSpeed * Time.deltaTime);
        }
    }
}