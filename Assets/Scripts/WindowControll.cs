using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindowControll : MonoBehaviour {
    public bool playerInBounds = false;
    
    public Sprite WindowOpen;
    public Sprite WindowClose;
    public float AutoOpenTime = 5;

    private SpriteRenderer spriteRenderer;
    private SpawnerController spawner;

    public bool isOpen = false;


    private const string HeroNAME = "Catomancer";
    // Use this for initialization
    void Start() {
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        spawner = gameObject.GetComponentInParent<SpawnerController>();
        CloseWindow();
        Invoke("OpenWindow", AutoOpenTime);


    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && playerInBounds && isOpen) {
            CloseWindow();
            Invoke("OpenWindow", AutoOpenTime);
            


        }




    }

    void CloseWindow() {
        spriteRenderer.sprite = WindowClose;
        spawner.Active = false;
        isOpen = false;
    }

    void OpenWindow() {
        spriteRenderer.sprite = WindowOpen;
        spawner.Active = true;
        isOpen = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ///Debug.Log("WindowCollision " + collision.collider);

    }



    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == HeroNAME) {
            playerInBounds = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.name == HeroNAME) {
            playerInBounds = false;
        }
    }





}