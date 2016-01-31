using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransientScreenController : MonoBehaviour
{
    public GameObject ContinueSign;
    public int SceneNr;

    private DateTime AppearanceTime;
    private bool CanBeClosed;

    void Start()
    {
        CanBeClosed = false;
        ContinueSign.GetComponent<Renderer>().enabled = false;
        AppearanceTime = DateTime.Now;
    }

    void Update()
    {
        if (DateTime.Now - AppearanceTime > TimeSpan.FromSeconds(2))
        {
            ContinueSign.GetComponent<Renderer>().enabled = true;
            CanBeClosed = true;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.Return) ||
            Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
        {
            if (CanBeClosed)
            {
                if (SceneNr == 1) // dla przejścia do kolejnego poziomu
                {
                    if (PlayerPrefs.HasKey("CurrentLevel"))
                    {
                        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("CurrentLevel", 1);
                    }
                }

                SceneManager.LoadScene(SceneNr);
            }
        }
    }
}
