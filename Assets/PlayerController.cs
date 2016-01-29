using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Sprite))]
public class PlayerController : MonoBehaviour
{
    enum MovingDirection
    {
        Left,
        LeftUp,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft
    }

    MovingDirection direction;
    
    Sprite sprite;

    [SerializeField]
    float movementSpeed;

    void Init()
    {
        sprite = GetComponent<Sprite>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        HandleMovement(h, v);
    }

    private void HandleMovement(float h, float v)
    {
        float SpeedX = h * movementSpeed;
        float SpeedY = v * movementSpeed;
        transform.position += new Vector3(SpeedX, SpeedY, 0);
    }
}
