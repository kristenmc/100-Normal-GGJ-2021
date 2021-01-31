using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void switchScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
