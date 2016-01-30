using UnityEngine;
using System.Collections;

public class BatterfuckerBehaviour : MonoBehaviour {


    public float Speed = 10f;
    public int Damage = 10;
    public float SinusAmpRatio = 2.0f;
    public float SinusFreqRatio = 2.0f;

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
        transform.position += new Vector3(speed_vector.x * dt + Mathf.Sin(Time.timeSinceLevelLoad * SinusFreqRatio) * SinusAmpRatio, speed_vector.y * dt + Mathf.Sin(Time.timeSinceLevelLoad * SinusFreqRatio) * SinusAmpRatio, 0);
        print(Mathf.Sin(Time.timeSinceLevelLoad*SinusFreqRatio)*SinusAmpRatio);
	
	}

    void Move () {

        

    }
}
