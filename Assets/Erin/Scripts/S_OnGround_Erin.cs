using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: Determins if the gameObject is on the ground or not
 * 
 * Public Functions: GetGroundStatus()
 * 
 * Other Scripts Needed: None
 */
public class S_OnGround_Erin : MonoBehaviour
{
    private bool onGround; //keeps track of if gameObject is on ground
  
    /*
     * Initialize private variables
     */
    void Start()
    {
        onGround = false;
    }

    /*
     * Returns the value of onGround
     */
    public bool GetGroundStatus()
    {
        return onGround;
    }

    /*
     * If the gameObject collides with
     * any gameObject labeled "Floor"
     * then sets onGround to true
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the gameObject collided with another gameObject
        //labeled "Floor"
        if(collision.gameObject.tag == "Floor")
        {
            //gameObject is grounded, so set onGround to true
            onGround = true;
        }
    }

    /*
     * If the gameObject is no longer colliding with
     * any gameObject labeled "Floor"
     * then sets onGround to false
     */
    void OnCollisionExit2D(Collision2D collision)
    {
        //if the gameObject is no longer colliding with another
        //gameObject labeled "Floor"
        if(collision.gameObject.tag == "Floor")
        {
            //gameObject is no longer grounded, so set onGround to false
            onGround = false;
        }
    }
    void Update() { }
}
