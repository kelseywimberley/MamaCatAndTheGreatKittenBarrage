using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_ExitGame_Erin : MonoBehaviour
{
    /*
     * Adds the exit game function to the button's onClick listener
     */
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ExitGame);
    }

    /*
     * Exit out of the game
     */
    void ExitGame()
    {
        //goes to the main menu
        SceneManager.LoadScene("MainMenu");
    }

    void Update() { }
}
