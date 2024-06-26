using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerControls_Kelsey : MonoBehaviour
{
    private Vector3 tempTransform;
    // Start is called before the first frame update
    void Start()
    {
        tempTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            tempTransform.x -= 1;
           
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            tempTransform.x += 1;
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {

        }

        transform.position = Vector3.MoveTowards(transform.position, tempTransform, Time.deltaTime);
    }
}
