using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenu : MonoBehaviour
{
    public enum Scene
    {
        Level = 0,
        MainMenu = 1,
        Credits = 2,
    }

    public void StartGame()
    {
        LoadScene(Scene.Level);
    }

    public void ShowCredits()
    {
        LoadScene(Scene.Credits);
    }

    public void ShowMainMenu()
    {
        LoadScene(Scene.MainMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadSceneAsync((int) scene);
    }
}
