using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string LevelName)
    {
        
        Debug.Log("Level load request" + LevelName);
        SceneManager.LoadScene(LevelName);

    }

    public static int LevelCount() {
        return SceneManager.sceneCountInBuildSettings;
       

    }

    public void LoadNextLevel()
    {
       
        //Debug.Log(SceneManager.GetActiveScene().buildIndex.ToString() + ";" + SceneManager.sceneCountInBuildSettings);
        int ActualScene = SceneManager.GetActiveScene().buildIndex;
        if (ActualScene >= SceneManager.sceneCountInBuildSettings - 1) SceneManager.LoadScene("Win");
        SceneManager.LoadScene(ActualScene + 1);

        

    }


    public void QuitGame()
    {
        Debug.Log("QuitGame request");
        Application.Quit();
    }

}
