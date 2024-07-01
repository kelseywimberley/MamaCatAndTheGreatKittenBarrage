using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_WinConditionKelsey : MonoBehaviour
{
    private bool gameOver;

    private GameObject[] staticKittens;
    private GameObject[] movingKittens;
    private GameObject[] heldKittens;

    public GameObject winText;
    public GameObject playAgainButton;
    public GameObject mainMenuButton;

    void Start()
    {
        gameOver = false;

        staticKittens = new GameObject[3];
        movingKittens = new GameObject[3];
        heldKittens = new GameObject[3];
    }

    void Update()
    {
        if (!gameOver)
        {
            staticKittens = GameObject.FindGameObjectsWithTag("StaticKitten");
            movingKittens = GameObject.FindGameObjectsWithTag("Kitten");
            heldKittens = GameObject.FindGameObjectsWithTag("PickedupKitten");

            if (staticKittens.Length == 0 && movingKittens.Length == 0 && heldKittens.Length == 0)
            {
                gameOver = true;

                winText.SetActive(true);
                playAgainButton.SetActive(true);
                mainMenuButton.SetActive(true);
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level");
    }
}
