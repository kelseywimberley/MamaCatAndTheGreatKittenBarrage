using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerControls_Kelsey : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(-0.005f, 0, 0);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0.005f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector3(0, 15.0f, 0);
        }

        // Go through GoThroughPlatform layer objects when going up
        if (rb.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
        }
        // Don't go through GoThroughPlatform layer objects when falling down
        else
        {
            Physics2D.IgnoreLayerCollision(3, 7, false);
        }
    }
}
