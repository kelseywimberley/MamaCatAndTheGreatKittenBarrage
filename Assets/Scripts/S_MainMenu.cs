using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ShowCredits()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
