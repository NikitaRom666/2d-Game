using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float minOppositeSpeed = 12f;
    public float maxOppositeSpeed = 16f;

    public float minSameDirectionSpeed = 3f;
    public float maxSameDirectionSpeed = 6f;

    public Sprite[] carSprites;

    private SpriteRenderer sr;

    private float speed;

    public void Setup(bool oppositeLane)
    {
        if (oppositeLane)
        {
            speed = Random.Range(
                minOppositeSpeed,
                maxOppositeSpeed
            );

            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            speed = Random.Range(
                minSameDirectionSpeed,
                maxSameDirectionSpeed
            );

            transform.rotation = Quaternion.identity;
        }
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (carSprites.Length > 0)
        {
            sr.sprite =
                carSprites[Random.Range(0, carSprites.Length)];
        }
    }

    void Update()
    {
        transform.Translate(
            Vector3.down * speed * Time.deltaTime,
            Space.World
        );

        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }
}