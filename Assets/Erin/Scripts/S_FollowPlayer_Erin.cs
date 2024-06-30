using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: Gameobjects will follow another gameObject
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_FollowPlayer_Erin : MonoBehaviour
{
    [Tooltip("The name of the gameObject this gameObject should follow")]
    public string parentName;
    [Tooltip("Where should the gameObject's position be relative the the parent's position")]
    public Vector2 positionOffset;
    private GameObject parent; //stores the data of the parent gameobject

    /*
     * Initialize private variables
     */
    void Start()
    {
        //if the parent can be found in the scene, store the data into parent
        parent = GameObject.Find(parentName);
    }

    /*
     * The gameobject's position updates based on the parent's position
     */
    void Update()
    {
        //if the parent is in the scene
        if(parent)
        {
            //if the player is moving right
            if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                //update this gameObject's position to be to the right of the player
                transform.position = new Vector2(parent.transform.position.x + positionOffset.x, parent.transform.position.y + positionOffset.y);
            }
            //if the player is moving left
            else if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                //update this gameObject's position to be to the left of the player
                transform.position = new Vector2(parent.transform.position.x - positionOffset.x, parent.transform.position.y + positionOffset.y);
            }
            //if the player is standing still
            else if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                //have the position be the same
                transform.position = parent.transform.position;
            }
            
        }
    }
}
