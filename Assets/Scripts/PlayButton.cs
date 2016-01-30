using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
	void OnMouseUp()
    {
        SceneManager.LoadScene(1);
	}
}
