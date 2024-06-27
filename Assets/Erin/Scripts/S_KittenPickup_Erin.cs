using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: Player picked up the moving kitten gameObject when they are
 *              close to the kitten & a key is pressed
 *              TEMPORARY CONDITION: The key needed to be pressed is Enter
 *                                   and the picked up kitten appears ontop of player
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_KittenPickup_Erin : MonoBehaviour
{
    [Tooltip("The kitten gameObject to spawn on top of the player")]
    public GameObject pickedUpKitten;
    private Vector3 pickedUpPosition; //the position that the kitten needs to appear
    private bool collidedFlag; //keeps track of if the player has collided with the kitten

   /*
    * Initialize private variables
    */
    void Start()
    {
        collidedFlag = false;
        pickedUpPosition = Vector3.zero;
    }

    /*
     * Checks to see if the kitten can be picked up by
     * the player
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the player has collided with the kitten gameObject
        if(collision.gameObject.name == "Player")
        {
            //set collidedflag = to true
            collidedFlag = true;
            //update the pickedUpPosition to be the player's currnt position
            pickedUpPosition = collision.gameObject.transform.position;
            
        }
    }

    /*
     * Checks to see if the kitten can no longer be picked
     * up by the player
     */
    void OnCollisionExit2D(Collision2D collision)
    {
        //if the player is no longer colliding with the kitten gameObject
        if (collision.gameObject.name == "Player")
        {
            //set collidedflag = to false
            collidedFlag = false;
        }
    }

    /*
     * Spawns the picked up kitten onto the player
     */
    void Update()
    {
        //if the kitten can be picked up & the return key is pressed
        if(collidedFlag == true && Input.GetKeyDown(KeyCode.Return))
        {
            //spawn the picked up kitten gameObject at the player's location
            Instantiate(pickedUpKitten,pickedUpPosition, Quaternion.identity);
            //destroy this gameObject since this kitten has now been picked up
            Destroy(gameObject);
        }
    }
}
