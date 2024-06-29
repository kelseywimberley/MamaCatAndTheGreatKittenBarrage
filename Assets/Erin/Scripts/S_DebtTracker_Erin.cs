using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/* Author: Erin Scribner
 * 
 * Date: 6/26/2024
 * 
 * Description: Updates the total debt tracker UI. 
 *              TEMPORARY CONDITION: UI updates depending on how many kittens there are
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: debtTrackerUI needs TextMeshPro
 */
public class S_DebtTracker_Erin : MonoBehaviour
{
    [Tooltip("How much debt the gameObject is worth when it is damaged." +
             "Have the amount be negative")]
    public int debtAmount;
    [Tooltip("The UI that will display the total debt amount")]
    public GameObject debtTrackerUI;

    private float timer; //determins when the UI is able to be updated
    private GameObject[] kittens; //determins how many times the UI can be updated

    /* If the debtAmount is a positive number, 
     * change it to a negative number.
     * 
     * Initialize private variables
     */
    void Start()
    {
        //if the value of debtAmount is positive
        if(debtAmount > 0)
        {
            //change it to a negative number since
            //debt is negative
            debtAmount *= -1;
        }

        timer = 1.0f + Time.time;
        kittens = new GameObject[3];
    }

    /*
     * Updates the UI that displays the total amount of debt
     */
    void DebtTracker()
    {
        //have an int variable that will store the value of debtTrackerUI
        int debt;
        //if it is possible to parse the integer value from the UI
        if(int.TryParse(debtTrackerUI.GetComponent<TextMeshProUGUI>().text, out debt) == true)
        {
            //increment the current total debt amount by debtAmount
            debt += debtAmount;
            //Have debtTrackerUI now display the new total debt amount
            debtTrackerUI.GetComponent<TextMeshProUGUI>().SetText(debt.ToString());
        }
    }

    /*
     * FOR TEMP. PURPOSES
     * Have the debtcounter update depending on how many 
     * kittens are on the screen
     */
    void Update()
    {
        //find out how many kittens are currently in the scene
        kittens = GameObject.FindGameObjectsWithTag("Kitten");
        //if it is time for the UI to be updated
        if (Time.time > timer)
        {
            //go through the list of kittens
            for(int i = 0; i < kittens.Length; i++)
            {
                //update the debt counter
                DebtTracker();
            }
            //increment timer
            timer = 1.0f + Time.time;
        }
    }
}
