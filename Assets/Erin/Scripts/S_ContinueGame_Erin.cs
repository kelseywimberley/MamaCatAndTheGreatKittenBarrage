using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Erin Scribner
 * 
 * Date: 6/30/2024
 * 
 * Description: Closes the pause menu and starts the game back up again
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_ContinueGame_Erin : MonoBehaviour
{
    /*
     * Adds the continue function to the button's onClick listener
     */
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Continue);
    }

    /*
     * Closes the pause menu and continues the game where
     * the player last left off.
     */
    void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    void Update() { }
}
