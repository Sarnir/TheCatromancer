using UnityEngine;
using System.Collections;

public class PortalCommander : MonoBehaviour {

    public bool playerInBounds = false;

    public Sprite PortalOpen;
    public Sprite PortalClose;
    public float AutoOpenTime = 6;

    private SpriteRenderer spriteRenderer;
    private SpawnerController spawner;


    private const string HeroNAME = "Catomancer";
    // Use this for initialization
    void Start() {
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        spawner = gameObject.GetComponentInParent<SpawnerController>();
        ClosePortal();
        Invoke("OpenPortal", AutoOpenTime);


    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && playerInBounds) {
            ClosePortal();
            Invoke("OpenPortal", AutoOpenTime);



        }




    }

    void ClosePortal() {
        spriteRenderer.sprite = PortalClose;
        spawner.Active = false;
    }

    void OpenPortal() {
        spriteRenderer.sprite = PortalOpen;
        spawner.Active = true;
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