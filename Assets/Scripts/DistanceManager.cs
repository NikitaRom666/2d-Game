using TMPro;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;

    private float distance;
    private bool gameOver;

    private CarController player;

    void Start()
    {
        player = FindFirstObjectByType<CarController>();
    }

    void Update()
    {
        if (gameOver)
            return;

        if (player == null)
            return;

        distance += player.GetCurrentSpeed() * Time.deltaTime;

        distanceText.text =
            Mathf.FloorToInt(distance) + " m";
    }

    public void StopCounting()
    {
        gameOver = true;
    }

    public int GetDistance()
    {
        return Mathf.FloorToInt(distance);
    }
}