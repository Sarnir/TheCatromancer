using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    SpriteRenderer renderer;
    bool seen;

    public Vector2 direction { get; set; }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (renderer.isVisible)
            seen = true;

        if (seen && !renderer.isVisible)
            Destroy();

        transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
