using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Erin Scribner
 * 
 * Date: 6/27/2024
 * 
 * Description: When gameObject on Kitten layer collides with this gameObject,
 *              the Kitten layered gameObject gets put away
 *              TEMPORARY CONDITION: Kitten just gets destroyed
 * 
 * Public Functions: None
 * 
 * Other Scripts Needed: None
 */
public class S_StoreKittens_Erin : MonoBehaviour
{
    [Tooltip("The particles to spawn when a kitten is put into the basket")]
    public ParticleSystem depositParticles;
    [Tooltip("The particles to spawn when all the kittens are put into the basket")]
    public ParticleSystem celebrationParticles;

    private int numOfKittens; //keeps track of how many kittens are put into the basket
    bool spawnParticles;

    /*
     * Initialize pirvate variables
     */
    void Start()
    {
        numOfKittens = 0;
        spawnParticles = true;
    }

    /*
     * When player collides with gameObject
     * and player has a kitten, destroy kitten
     * 
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if a gameobject on the kitten layer collides wiht this gameobject
        if(collision.gameObject.layer == LayerMask.NameToLayer("Kitten"))
        {
            numOfKittens++;
            //spawn particles
            Instantiate(depositParticles, transform.position, Quaternion.identity);
           //destroy kitten to symbolize that kitten is put away
           Destroy(collision.gameObject);
        }
    }

    void Update() 
    { 
        if(numOfKittens >= 3 && spawnParticles)
        {
            spawnParticles = false;
            float x = Random.Range(transform.position.x-2, transform.position.x+2);
            float y = Random.Range(transform.position.y - 2, transform.position.y + 2);
            //spawn particles
            Instantiate(celebrationParticles, new Vector2(x,y), Quaternion.identity);
        } 
    }
}
