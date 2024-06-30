using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/30/2024
 * 
 * Description: Pauses the game
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_PauseGame_Erin : MonoBehaviour
{
    void Start() { }

   /*
    * Sets timescale to 0 so game is paused
    */
    void Update()
    {
        Time.timeScale = 0;
    }
}
