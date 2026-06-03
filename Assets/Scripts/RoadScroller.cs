using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    public float height = 20f;

    private CarController player;

    void Start()
    {
        player = FindFirstObjectByType<CarController>();
    }

    void Update()
    {
        float currentSpeed = player.GetCurrentSpeed();

        transform.Translate(
            Vector3.down *
            currentSpeed *
            Time.deltaTime
        );

        if (transform.position.y <= -height)
        {
            transform.position += new Vector3(
                0,
                height * 2,
                0
            );
        }
    }
}