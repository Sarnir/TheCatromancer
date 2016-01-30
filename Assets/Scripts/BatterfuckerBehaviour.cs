using UnityEngine;
using System.Collections;

public class BatterfuckerBehaviour : MonoBehaviour {


    public float Speed = 10f;
    public int Damage = 10;
    public float SinusAmpRatio = 2.0f;
    public float SinusFreqRatio = 2.0f;
    public float Y_sprite_padding = 5;
    public float X_sprite_padding = 5;


    private Vector3 speed_vector;
    private float x_max, x_min, y_max, y_min;
    private PlayerController Cat;



    // Use this for initialization
    void Start () {

        Cat = GameObject.FindObjectOfType<PlayerController>();

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

     
    
        float x_speed = Random.Range(0, Speed);
        float y_speed = Speed - x_speed;
        speed_vector = new Vector3(x_speed, y_speed,0);







    }


    

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("BatfackerCollision");

    }



    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("BatfuckerTrigger");


        speed_vector = Cat.transform.position - transform.position;
        



        
    }

    // Update is called once per frame
    void Update () {

        float dt = Time.deltaTime;
        transform.position += new Vector3(speed_vector.x * dt + Mathf.Sin(Time.timeSinceLevelLoad * SinusFreqRatio) * SinusAmpRatio, speed_vector.y * dt + Mathf.Sin(Time.timeSinceLevelLoad * SinusFreqRatio) * SinusAmpRatio, 0);
        //print(Mathf.Sin(Time.timeSinceLevelLoad*SinusFreqRatio)*SinusAmpRatio);
        //Clam boundaries

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x_min, x_max), Mathf.Clamp(transform.position.y, y_min, y_max), 0);


    }

    void Move () {

        

    }
}
