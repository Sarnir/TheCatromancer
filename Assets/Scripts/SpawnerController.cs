using UnityEngine;
using System.Collections;
using System;
using UnityRandom = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    public GameObject Monster;
    public bool TimeToActivate;
    public bool Active;
    public bool PlayerTriggeredClosing;
    public int SpawnIntervalBase;

    private DateTime LastTimeSpawned;
	
	void Update ()
    {
        if (TimeToActivate)
        {
            // pokaż portal
            Active = true;
        }

	    if (Active)
        {
            if (DateTime.Now - LastTimeSpawned > TimeSpan.FromSeconds(SpawnIntervalBase + (UnityRandom.value * 2)))
            {
                SpawnMonster(gameObject.transform.position);
                LastTimeSpawned = DateTime.Now;
            }

            if (PlayerTriggeredClosing)
            {
                Active = false;
            }
        }
    }

    GameObject SpawnMonster(Vector2 spawnerPosition)
    {
        GameObject monster = Instantiate(Monster);
        monster.transform.position = transform.position + new Vector3(spawnerPosition.x, spawnerPosition.y);
        return monster;
    }
}
