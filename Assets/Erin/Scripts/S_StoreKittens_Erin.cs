using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: When player reaches gameObject with a kitten, 
 *              put kitten in gameObject
 *              TEMPORARY CONDITION: Kitten just gets destroyed
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_StoreKittens_Erin : MonoBehaviour
{
    void Start() { }

    /*
     * When player collides with gameObject
     * and player has a kitten, destroy kitten
     * 
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with this gameObject
        if(collision.gameObject.tag == "Player")
        {
            //and the player has a kitten
            if(GameObject.FindGameObjectWithTag("PickedupKitten"))
            {
                //destroy kitten to symbolize that kitten is put away
                Destroy(GameObject.FindGameObjectWithTag("PickedupKitten"));
            }
        }
    }
    void Update() { }
}
