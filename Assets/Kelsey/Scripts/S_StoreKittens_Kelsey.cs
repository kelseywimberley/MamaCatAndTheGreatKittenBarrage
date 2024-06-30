using System.Collections;
using System.Collections.Generic;
using TMPro;
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
public class S_StoreKittens_Kelsey : MonoBehaviour
{
    private int kittenCount;
    private GameObject kittenCountText;

    [Tooltip("The particles to spawn when a kitten is put into the basket")]
    public ParticleSystem depositParticles;
    [Tooltip("The particles to spawn when all the kittens are put into the basket")]
    public ParticleSystem celebrationParticles;
    bool spawnParticles;

    void Start()
    {
        kittenCount = 0;
        kittenCountText = GameObject.Find("KittenCounter");
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Kitten"))
        {
            //destroy kitten to symbolize that kitten is put away
            Destroy(collision.gameObject);
            kittenCount++;
            kittenCountText.GetComponent<TextMeshProUGUI>().SetText(kittenCount + "/3");
            //spawn particles
            Instantiate(depositParticles, transform.position, Quaternion.identity);
        }
    }
    void Update()
    {
        if (kittenCount >= 3 && spawnParticles)
        {
            spawnParticles = false;
            float x = Random.Range(transform.position.x - 2, transform.position.x + 2);
            float y = Random.Range(transform.position.y - 2, transform.position.y + 2);
            //spawn particles
            Instantiate(celebrationParticles, new Vector2(x, y), Quaternion.identity);
        }
    }
}
