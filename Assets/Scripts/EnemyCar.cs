using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [Header("Relative Speeds")]
    public float minOppositeDifference = 6f;
    public float maxOppositeDifference = 10f;

    public float minSameDifference = 1f;
    public float maxSameDifference = 4f;

    [Header("Sprites")]
    public Sprite[] carSprites;

    private SpriteRenderer sr;
    private CarController player;

    private bool oppositeLane;
    private float relativeSpeed;

    public void Setup(bool isOppositeLane)
    {
        oppositeLane = isOppositeLane;

        player = FindFirstObjectByType<CarController>();

        if (oppositeLane)
        {
            // зустрічна смуга
            relativeSpeed = Random.Range(
                minOppositeDifference,
                maxOppositeDifference
            );

            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            // попутна смуга
            relativeSpeed = Random.Range(
                minSameDifference,
                maxSameDifference
            );

            transform.rotation = Quaternion.identity;
        }
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (carSprites.Length > 0)
        {
            sr.sprite = carSprites[
                Random.Range(0, carSprites.Length)
            ];
        }

        if (player == null)
        {
            player = FindFirstObjectByType<CarController>();
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float playerSpeed = player.GetCurrentSpeed();

        float finalSpeed;

        if (oppositeLane)
        {
            // зустрічка швидша за нас
            finalSpeed = playerSpeed + relativeSpeed;
        }
        else
        {
            // попутні трохи повільніші
            finalSpeed = playerSpeed - relativeSpeed;

            if (finalSpeed < 1f)
                finalSpeed = 1f;
        }

        transform.Translate(
            Vector3.down *
            finalSpeed *
            Time.deltaTime,
            Space.World
        );

        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }
}