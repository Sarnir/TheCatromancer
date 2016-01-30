using UnityEngine;
using System.Collections;
using System;

public class DemonDogBehavior : MonoBehaviour
{
    public GameObject Player;
    public GameObject Portal;

    private DateTime ShotInterval;

	void Start ()
    {
        this.gameObject.transform.position = Portal.gameObject.transform.position;
        ShotInterval = DateTime.Now;
    }
	
	void Update ()
    {

        // jakieś chodzenie tego psa, żeby losowo łaził, ale strzelał w Playera co 3 sekundy + losowy timer między 0-3 sekund
        //this.gameObject.transform.position = Player.gameObject.transform.position.Normalize();
	}
}
