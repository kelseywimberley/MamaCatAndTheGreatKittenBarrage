using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/* Author: Erin Scribner
 * 
 * Date: 6/26/2024
 * 
 * Description: Updates the total debt tracker UI. 
 *              TEMPORARY CONDITION: UI updates whenever M is pressed
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: debtTrackerUI needs TextMeshPro
 */
public class S_DebtTracker_Kelsey : MonoBehaviour
{
    [Tooltip("How much debt the gameObject is worth when it is damaged." +
             "Have the amount be negative")]
    public int debtAmount;
    [Tooltip("The UI that will display the total debt amount")]
    public GameObject debtTrackerUI;

    /* If the debtAmount is a positive number, 
     * change it to a negative number
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
     * Have the debtcounter update every time M is pressed
     */
    void Update()
    {
        //if M is pressed
        if(Input.GetKeyDown(KeyCode.M))
        {
            //update the debt counter
            DebtTracker();
        }
    }
}
