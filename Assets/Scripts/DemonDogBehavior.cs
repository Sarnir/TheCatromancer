using UnityEngine;
using System.Collections;
using System;
using UnityRandom = UnityEngine.Random;

public class DemonDogBehavior : MonoBehaviour
{
    public GameObject Player;
    public GameObject Portal;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float projectileOffset;

    [SerializeField]
    float shotIntervalBase;

    private DateTime ShotInterval { get; set; }

    GameObject currentProjectile;

    void Start ()
    {
        gameObject.transform.position = Portal.gameObject.transform.position;
        ShotInterval = DateTime.Now;
    }
	
	void Update ()
    {
        if (DateTime.Now - ShotInterval > TimeSpan.FromSeconds(shotIntervalBase + (UnityRandom.value * 2)))
        {
            currentProjectile = CreateProjectile(Input.mousePosition);
            ShotInterval = DateTime.Now;
        }
        
        gameObject.transform.position += new Vector3(UnityRandom.value - 0.5f, UnityRandom.value - 0.5f, 0);
        
    }

    GameObject CreateProjectile(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position;
        direction.Normalize();

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.GetComponent<Projectile>().direction = direction;

        // Get Angle in Radians
        float AngleRad =
            Mathf.Atan2(Player.transform.position.y - gameObject.transform.position.y, Player.transform.position.x - gameObject.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        projectile.GetComponent<Projectile>().transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        projectile.transform.position = transform.position + new Vector3(projectileOffset * direction.x, projectileOffset * direction.y);

        return projectile;
    }
}
