using TMPro;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;

    public float metersPerSecond = 20f;

    private float distance;
    private bool gameOver;

    void Update()
    {
        if (gameOver) return;

        distance += metersPerSecond * Time.deltaTime;

        distanceText.text = ((int)distance) + " m";
    }

    public void StopCounting()
    {
        gameOver = true;
    }

    public int GetDistance()
    {
        return (int)distance;
    }
}