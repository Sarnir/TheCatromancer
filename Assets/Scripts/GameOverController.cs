using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.Return) ||
            Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}
