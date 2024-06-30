using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelSelector : MonoBehaviour{
    public enum Scene
    {
        MainMenu = 0,
        Level = 1,
        Credits = 2,
    }

    [SerializeField] private AudioClip menuHoverSound;
    [SerializeField] private AudioClip menuClickSound;

    public void StartGame()
    {
        StartCoroutine(LoadScene(Scene.Level, 1f));
    }

    public void ShowCredits()
    {
        StartCoroutine(LoadScene(Scene.Credits, 1f));
    }

    public void ShowMainMenu()
    {
        StartCoroutine(LoadScene(Scene.MainMenu, 1f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScene(Scene scene, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadSceneAsync((int) scene);
    }

    public void PlayMenuHover()
    {
        S_SoundManager.instance.PlayClip(menuHoverSound, transform, 1f);
    }

    public void PlayMenuClick()
    {
        S_SoundManager.instance.PlayClip(menuClickSound, transform, 1f);
    }
}
