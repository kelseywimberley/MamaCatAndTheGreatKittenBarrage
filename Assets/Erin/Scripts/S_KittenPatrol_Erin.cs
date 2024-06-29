using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: Kitten moves in a continuous loop
 *  T           EMPORARY CONDITION: Patrol route only has kitten move either left or right
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: S_OnGround_Erin()
 */
public class S_Patrol : MonoBehaviour
{
    [Tooltip("How fast the kitten can move")]
    public float movementSpeed;
    [Tooltip("Determins how many spaces in the X direction the kitten moves. " +
             "negative number = moving left" +
             "positive number = moving right")]
    public int movementSpace;

    private Vector2 newPosition; //the new place the kitten moves to
    private Vector2 fallPosition; //the place the kitten needs to fall to
    private Vector2 startingPosition; //the place the kitten starts at
   
    /*
     * Initialize private variables
     */
    void Start()
    {
        newPosition = new Vector2(transform.position.x + movementSpace, transform.position.y);
        startingPosition = transform.position;
        fallPosition = Vector2.zero;
    }
    
    /*
     * Checks to see if the kitten isn't on ground.
     * if kitten isn't on ground, has the kitten fall
     * until it lands on ground
     */
    void Fall()
    {
        //if the kitten is already on ground
        if (GetComponent<S_OnGround_Erin>().GetGroundStatus() == true)
        {
            //set fallPosition to 0
            fallPosition.y = 0; 
        }
        //if kitten isn't on ground
        else if (GetComponent<S_OnGround_Erin>().GetGroundStatus() == false)
        {
            //add gravity to fallPosition
            fallPosition.y += Physics2D.gravity.y * Time.deltaTime;
           
        }
        //have the kitten fall down
        transform.Translate(fallPosition * Time.deltaTime);
    }

    /*
     * The kitten moves based on the values inside of
     * patrolRoute
     */
    void Patrol()
    {
        //if the kitten has reached their destination
        if (Vector2.Distance(transform.position, newPosition) <= 0.01f)
        {
            newPosition = startingPosition;
            startingPosition = transform.position;
        }
        //we only care about left & right for now, so make sure 
        //newPosition.y = kitten's current y 
        newPosition.y = transform.position.y;
        //have kitten move to its new destination
        transform.position = Vector2.MoveTowards(transform.position, newPosition, movementSpeed * Time.deltaTime);
    }

    /*
     * Has the kitten move in a continuous loop
     */
    void Update()
    {
        //check to see if kitten needs to fall
        Fall();

        //if kitten is on ground
        if (GetComponent<S_OnGround_Erin>().GetGroundStatus() == true)
        {
            //have kitten do its patrol route
            Patrol();
        }
    }
}
