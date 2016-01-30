using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeKeeper : MonoBehaviour {



    private GameObject HeartPrefab;
    private PlayerController Player;

    // Use this for initialization
    void Start() {
        Player = GameObject.FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update() {
        NextFreePosition();

    }


    Transform NextFreePosition() {
        int lifes = Player.LifesCount;
        int count = 0;
        //Generet heart icon
        foreach (Transform child in transform) {
            count++;
            //print(child.name);
            if (count <= lifes) {
                child.GetComponent<SpriteRenderer>().enabled = true;

            }
            else {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
            

        }
        return null;
    }
}
