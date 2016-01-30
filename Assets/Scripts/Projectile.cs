using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    public Vector2 direction { get; set; }

    void FixedUpdate()
    {
        transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
