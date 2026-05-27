using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 5f;
    public Sprite[] carSprites;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // рандомна машина
        sr.sprite = carSprites[Random.Range(0, carSprites.Length)];

        // рандомна швидкість
        speed = Random.Range(4f, 8f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}