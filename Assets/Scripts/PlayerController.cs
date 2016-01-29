using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Sprite))]
public class PlayerController : MonoBehaviour
{    
    Sprite sprite;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    GameObject projectile;

    void Init()
    {
        sprite = GetComponent<Sprite>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool lc = Input.GetButtonDown("Fire1");
        bool rc = Input.GetButtonDown("Fire2");
        HandleMovement(h, v);
        HandleMouse(lc, rc);
    }

    private void HandleMouse(bool lc, bool rc)
    {
        if(lc)
            Debug.Log("Left CLick!");
        if(rc)
            Debug.Log("Right CLick!");
    }

    private void HandleMovement(float h, float v)
    {
        float SpeedX = h * movementSpeed;
        float SpeedY = v * movementSpeed;
        transform.position += new Vector3(SpeedX, SpeedY, 0);
    }
}
