﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    SpriteRenderer spriteRenderer;
    bool seen;

    public Vector2 direction { get; set; }
    public GameObject player { get; set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
            seen = true;

        if (seen && !spriteRenderer.isVisible)
            Destroy();

        transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != player.name)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
