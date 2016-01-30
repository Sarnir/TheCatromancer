using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Sprite))]
public class PlayerController : MonoBehaviour
{    
    Sprite sprite;
    Vector2 direction;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float projectileOffset;

    [SerializeField]
    GameObject projectilePrefab;

    GameObject currentProjectile;

    void Init()
    {
        sprite = GetComponent<Sprite>();
    }

    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        bool lc = Input.GetButtonDown("Fire1");
        bool rc = Input.GetButtonDown("Fire2");
        HandleMovement(direction);
        HandleMouse(lc, rc);
    }

    private void HandleMouse(bool leftClick, bool rightClick)
    {
        if (leftClick && currentProjectile == null)
        {
            currentProjectile = CreateProjectile(Input.mousePosition);
        }
        if (rightClick)
        {

        }
    }

    private void HandleMovement(Vector2 direction)
    {
        float SpeedX = direction.x * movementSpeed;
        float SpeedY = direction.y * movementSpeed;
        transform.position += new Vector3(SpeedX, SpeedY, 0);
    }

    GameObject CreateProjectile(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;
        direction.Normalize();

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.GetComponent<Projectile>().direction = direction;

        // Get Angle in Radians
        float AngleRad =
            Mathf.Atan2(mousePosition.y - gameObject.transform.position.y, mousePosition.x - gameObject.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        projectile.GetComponent<Projectile>().transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        
        projectile.transform.position = transform.position + new Vector3(projectileOffset * direction.x, projectileOffset * direction.y);

        return projectile;
    }
}
