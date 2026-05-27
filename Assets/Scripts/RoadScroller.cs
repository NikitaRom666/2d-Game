using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    public float speed = 5f;
    public float height = 20f; // висота дороги

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -height)
        {
            transform.position += new Vector3(0, height * 2, 0);
        }
    }
}