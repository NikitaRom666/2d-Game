using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 3f;  // помаленько щоб гравець міг обійти
    public Sprite[] carSprites;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // рандомна машина
        sr.sprite = carSprites[Random.Range(0, carSprites.Length)];

        // всі машинки мають їхати в одному напрямку - вперед (не назад!)
        sr.flipX = true;

        // рандомна швидкість але помаленько
        speed = Random.Range(2f, 3.5f);
    }

    void Update()
    {
        // машинка їде вниз, як дорога скролиться
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}