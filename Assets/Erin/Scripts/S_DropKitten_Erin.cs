using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: allows the player to drop the kitten they previously picked up
 *              TEMPORARY CONDITION: to drop the kitten press Enter & the kitten is dropped
 *                                   to the right of the player
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_DropKitten : MonoBehaviour
{
    [Tooltip("The kitten to spawn when the picked up kitten is dropped")]
    public GameObject movingCounterpart;
    private Vector2 dropPosition; //where the kitten is to be dropped

   /*
    * Initialize private variables
    */
    void Start()
    {
       dropPosition = transform.position;
    }

    /*
     * If a certain key is pressed, drops the kitten
     */
    void Update()
    {
        //if return is pressed
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //update dropPosition to be the current kitten position
            dropPosition = transform.position;
            //set the offset so the kitten is dropped to the right of the player
            dropPosition.x += 1;
            dropPosition.y -= 1;
            //spawn the kitten that can move
            Instantiate(movingCounterpart, dropPosition, Quaternion.identity);
            //destroy the kitten that is on top of the player
            Destroy(gameObject);
        }
    }
}
