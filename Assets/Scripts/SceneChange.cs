using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame(){
        // 1 is index of gamescene
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("QuitGame");
    }
}
