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
public class S_DebtTracker_Kelsey : MonoBehaviour
{
    [Tooltip("How much debt the static kitten is worth")]
    public int debtAmountStaticKitten = 3;
    [Tooltip("How much debt the moving kitten is worth")]
    public int debtAmountMovingKitten = 1;
    [Tooltip("The UI that will display the total debt amount")]
    public GameObject debtTrackerUI;

    public GameObject NickUITextONe;
    public GameObject NickUITextTWo;

    private int debtAmount;

    private float timer; //determins when the UI is able to be updated
    private GameObject[] staticKittens; //determins how many times the UI can be updated
    private GameObject[] movingKittens; //determins how many times the UI can be updated

    private int currentLost;

    /* If the debtAmount is a positive number, 
     * change it to a negative number.
     * 
     * Initialize private variables
     */
    void Start()
    {
        //if the value of debtAmount is negative
        if (debtAmountStaticKitten < 0)
        {
            //change it to a negative number since
            //debt is negative
            debtAmountStaticKitten *= -1;
        }
        if (debtAmountMovingKitten < 0)
        {
            //change it to a positive number since negative sign will be added later
            debtAmountMovingKitten *= -1;
        }

        debtAmount = 0;
        timer = 1.0f + Time.time;
        staticKittens = new GameObject[3];
        movingKittens = new GameObject[3];

        currentLost = 0;
    }

    /*
     * Updates the UI that displays the total amount of debt
     */
    void DebtTrackerStaticKitten()
    {
        
        //Have debtTrackerUI now display the new total debt amount
        debtTrackerUI.GetComponent<TextMeshProUGUI>().SetText("-$" + debtAmount.ToString());
    }

    /*
     * Updates the UI that displays the total amount of debt
     */
    void DebtTrackerMovingKitten()
    {
       
        //Have debtTrackerUI now display the new total debt amount
        debtTrackerUI.GetComponent<TextMeshProUGUI>().SetText("-$" + debtAmount.ToString());
    }

    /*
     * FOR TEMP. PURPOSES
     * Have the debtcounter update depending on how many 
     * kittens are on the screen
     */
    void Update()
    {
        //find out how many kittens are currently in the scene
        staticKittens = GameObject.FindGameObjectsWithTag("StaticKitten");
        movingKittens = GameObject.FindGameObjectsWithTag("Kitten");

        //if it is time for the UI to be updated
        if (Time.time > timer)
        {
            debtAmount += currentLost;
            currentLost = 0;
            //go through the list of kittens
            for (int i = 0; i < staticKittens.Length; i++)
            {
                currentLost += debtAmountStaticKitten;
              
                //update the debt counter
                DebtTrackerStaticKitten();
            }

            for (int i = 0; i < movingKittens.Length; i++)
            {
                //increment the current total debt amount by debtAmount
               // debtAmount += debtAmountMovingKitten;

                currentLost += debtAmountMovingKitten;
                //update the debt counter
                DebtTrackerMovingKitten();
            }

            if (currentLost > 0)
            {
                NickUITextONe.GetComponent<TextMeshProUGUI>().SetText("-$" + currentLost);
                NickUITextTWo.GetComponent<TextMeshProUGUI>().SetText("-$" + currentLost);
            }
            else
            {
                NickUITextONe.GetComponent<TextMeshProUGUI>().SetText("");
                NickUITextTWo.GetComponent<TextMeshProUGUI>().SetText("");
            }
          
            //increment timer
            timer = 1.0f + Time.time;
        }
    }
}
