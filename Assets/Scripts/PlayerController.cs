using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Sprite))]
public class PlayerController : MonoBehaviour
{
    public bool isImmortal = false;
    public int LifesCount = 5;

    private const int EnemyLAYER = 9;
    private const int ProjectileEnemyLAYER = 11;


    public Color defaultColor;

    public float ImmortalTime = 2f;
    public float blinkFrequency = 0.1f;
    public Color blinkColor;
    public Color glowColor;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    Vector2 direction;
    PolygonCollider2D polygonCollider;

    private float Y_sprite_padding = 1;
    private float X_sprite_padding = 1;

    private float x_max, x_min, y_max, y_min;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float projectileOffset;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    Seal seal;

    GameObject currentProjectile;
    Animator animator;
    Vector3 originalScale;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMostCamera = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0, distance));
        Vector3 rightMostCamera = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, distance));
        Vector3 topMostCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 botMostCamera = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        print(rightMostCamera.x);
        x_min = leftMostCamera.x + X_sprite_padding;
        x_max = rightMostCamera.x - X_sprite_padding;
        y_min = topMostCamera.y + Y_sprite_padding;
        y_max = botMostCamera.y - Y_sprite_padding;
    }

    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        bool lc = Input.GetButtonDown("Fire1");
        bool rc = Input.GetButton("Fire2");
        HandleMovement(direction);
        HandleMouse(lc, rc);

    }


    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("CatCollision " + collision.collider);
        int collision_layer = collision.collider.gameObject.layer;
        if (collision_layer == ProjectileEnemyLAYER || collision_layer == EnemyLAYER) {
            HandleHit();
        }
        

        
    }



    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("CatTrigger " + collider.name );
        int collision_layer = collider.gameObject.layer;
        if (collision_layer == ProjectileEnemyLAYER || collision_layer == EnemyLAYER) {
            HandleHit();
        }


    }

    private void HandleMouse(bool leftClick, bool rightClick)
    {
        if (leftClick && currentProjectile == null)
        {
            currentProjectile = CreateProjectile(Input.mousePosition);
        }
        if (rightClick)
        {
            seal.PaintSealAtPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void HandleMovement(Vector2 direction)
    {
        float SpeedX = direction.x * movementSpeed;
        float SpeedY = direction.y * movementSpeed;
        transform.position += new Vector3(SpeedX, SpeedY, 0);

        animator.SetBool("MoveLeftRight", direction.x != 0);

        if (direction.x < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        else
            transform.localScale = originalScale;

        // Limit 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x_min, x_max), Mathf.Clamp(transform.position.y, y_min, y_max), 0);
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


    void SetMortal() {
        isImmortal = false;
    }

    void SetImmortal() {
        isImmortal = true;
        Invoke("SetMortal", ImmortalTime);
        polygonCollider.enabled = false;
    }



    void HandleHit() {
        if (!isImmortal) {
            SetImmortal();
            StartBlinkSprite();
            LifesCount--;
        }
    }

    void StartBlinkSprite() {
        InvokeRepeating("SetSpriteHi",0, blinkFrequency);
        //WaitForSeconds(0.25f);
        InvokeRepeating("SetSpriteLow",blinkFrequency/2, blinkFrequency);
        Invoke("StopBlinkSprie", ImmortalTime);

    }

    void StopBlinkSprie() {
        CancelInvoke("SetSpriteHi");
        CancelInvoke("SetSpriteLow");
        polygonCollider.enabled = true;
        spriteRenderer.color = defaultColor;
    }

    void SetSpriteHi() {
        spriteRenderer.color = glowColor;
    }

    void SetSpriteLow() {
        spriteRenderer.color = blinkColor;

    }
}
