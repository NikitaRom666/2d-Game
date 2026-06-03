using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject carPrefab;

    [Header("Road Settings")]
    public float leftX = -3f;
    public float rightX = 3f;
    public int laneCount = 3;

    [Header("Spawn Settings")]
    public float spawnY = 10f;
    public float minSpawnDelay = 1.5f;
    public float maxSpawnDelay = 3f;
    public float minDistanceBetweenCars = 4f;
    public int maxCars = 8;
    public int maxCarsPerLane = 2;

    [Header("Detection")]
    public LayerMask carLayer;

    private float[] nextSpawnTime;
    private float[] lanePositions;

    void Start()
    {
        // створюємо смуги
        lanePositions = new float[laneCount];
        float width = (rightX - leftX) / (laneCount - 1);

        for (int i = 0; i < laneCount; i++)
        {
            lanePositions[i] = leftX + width * i;
        }

        // таймер для кожної смуги
        nextSpawnTime = new float[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            nextSpawnTime[i] = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }

    void Update()
    {
        for (int i = 0; i < laneCount; i++)
        {
            if (Time.time >= nextSpawnTime[i])
            {
                TrySpawn(i);

                // новий рандомний інтервал
                nextSpawnTime[i] = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
    }

    void TrySpawn(int laneIndex)
{
    float x = lanePositions[laneIndex];

    int carsInLane = 0;

    GameObject[] allCars =
        GameObject.FindGameObjectsWithTag("EnemyCar");

    foreach (GameObject car in allCars)
    {
        if (Mathf.Abs(car.transform.position.x - x) < 0.5f)
        {
            carsInLane++;
        }
    }

    if (carsInLane >= maxCarsPerLane)
        return;

    Vector2 spawnPos = new Vector2(x, spawnY);

    Vector2 boxSize =
        new Vector2(0.8f, minDistanceBetweenCars);

    Collider2D hit = Physics2D.OverlapBox(
        spawnPos + Vector2.down * (minDistanceBetweenCars / 2),
        boxSize,
        0f,
        carLayer
    );

    if (hit == null)
    {
        GameObject carObj =
            Instantiate(carPrefab, spawnPos, Quaternion.identity);

        EnemyCar enemyCar =
            carObj.GetComponent<EnemyCar>();

        bool oppositeLane =
            laneIndex < laneCount / 2;

        enemyCar.Setup(oppositeLane);
    }
}

    // візуалізація зон
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (laneCount <= 1) return;

        float width = (rightX - leftX) / (laneCount - 1);

        for (int i = 0; i < laneCount; i++)
        {
            float x = leftX + width * i;

            Vector3 pos = new Vector3(x, spawnY - (minDistanceBetweenCars / 2), 0);
            Vector3 size = new Vector3(0.8f, minDistanceBetweenCars, 0);

            Gizmos.DrawWireCube(pos, size);
        }
    }
}