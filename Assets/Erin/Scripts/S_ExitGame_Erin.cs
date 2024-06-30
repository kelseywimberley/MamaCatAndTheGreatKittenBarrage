using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author: Erin Scribner
 * 
 * Date: 6/30/2024
 * 
 * Description: Exits out of the game when the button is clicked
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
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
        Application.Quit();
    }

    void Update() { }
}
