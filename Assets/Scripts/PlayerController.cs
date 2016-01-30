using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Sprite))]
public class PlayerController : MonoBehaviour
{
    public bool isImmortal = false;
    public int LifesCount = 5;


    public Color defaultColor;

    public float ImmortalTime = 1f;
    public float blinkFrequency = 0.1f;
    public Color blinkColor;
    public Color glowColor;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    Vector2 direction;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float projectileOffset;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    Seal seal;

    GameObject currentProjectile;

    void Init()
    {
        sprite = GetComponent<Sprite>();
        
        
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartBlinkSprite();
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
    }

    GameObject CreateProjectile(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;
        direction.Normalize();

        GameObject projectile = Instantiate(projectilePrefab);
        projectile.GetComponent<Projectile>().direction = direction;
        projectile.GetComponent<Projectile>().transform.Rotate(new Vector3(direction.x, direction.y, 1));
        projectile.transform.position = transform.position + new Vector3(projectileOffset * direction.x, projectileOffset * direction.y);

        return projectile;
    }

    void HitPlayer(int power = 1) {
        LifesCount--;

    }

    void SetMortal() {
        isImmortal = false;
    }

    void SetImmortalMortal() {
        isImmortal = false;
    }

    public void SetImmortalForTime(float time = 2) {
 
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
        spriteRenderer.color = defaultColor;
    }

    void SetSpriteHi() {
        spriteRenderer.color = glowColor;
    }

    void SetSpriteLow() {
        spriteRenderer.color = blinkColor;

    }
}
