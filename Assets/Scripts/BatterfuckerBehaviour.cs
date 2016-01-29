using UnityEngine;
using System.Collections;

public class BatterfuckerBehaviour : MonoBehaviour {


    public float Speed = 10f;
    public int Damage = 10;

    private Vector3 speed_vector;

	// Use this for initialization
	void Start () {

        float x_speed = Random.Range(0, Speed);
        float y_speed = Speed - x_speed;
        speed_vector = new Vector3(x_speed, y_speed,0);

        
	
	}

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("BatfackerCollision");

    }



    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("BatfuckerTrigger");
        speed_vector = new Vector3(-speed_vector.x, -speed_vector.y, 0);

        
    }

    // Update is called once per frame
    void Update () {

        float dt = Time.deltaTime;
        transform.position += new Vector3(speed_vector.x * dt, speed_vector.y * dt, 0); 
	
	}

    void Move () {

        

    }
}
