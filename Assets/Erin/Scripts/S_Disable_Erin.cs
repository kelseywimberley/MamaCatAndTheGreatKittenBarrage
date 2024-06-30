using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/30/2024
 * 
 * Description: When game starts up, disables the gameObject's functionality.
 *              Used primarely to hide menus at the start of the game
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_Disable_Erin : MonoBehaviour
{
    /*
     * At the start of the game deactivate the pause menu. 
     * This hides the pause menu at the start of the game
     */
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update() { }
}
