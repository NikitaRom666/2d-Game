using UnityEngine;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI recordText;

    private float distance;
    private bool counting = true;

    private int record;

    void Start()
    {
        record = PlayerPrefs.GetInt("Record", 0);

        recordText.text =
            "Рекорд: " + record + " м";
    }

    void Update()
    {
        if (!counting)
            return;

        CarController player =
            FindFirstObjectByType<CarController>();

        distance +=
            player.GetCurrentSpeed() *
            Time.deltaTime;

        distanceText.text =
            ((int)distance).ToString() + " м";
    }

    public void StopCounting()
    {
        counting = false;

        int currentDistance = (int)distance;

        if (currentDistance > record)
        {
            record = currentDistance;

            PlayerPrefs.SetInt(
                "Record",
                record
            );

            PlayerPrefs.Save();
        }
    }
}