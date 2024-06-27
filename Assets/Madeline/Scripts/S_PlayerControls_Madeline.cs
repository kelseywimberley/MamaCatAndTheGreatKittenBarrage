using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerControls_Madeline : MonoBehaviour
{

    public LineRenderer jumpLine;

    private Vector3 tempTransform;
    private Vector3 jumpDirection;
    // Start is called before the first frame update
    void Start()
    {
        tempTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateJumpDirection();

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

    void UpdateJumpDirection(){
        Vector3 playerPos = gameObject.transform.position;
        playerPos.z = -1;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float magnitude = ((Vector2)(mousePos - playerPos)).magnitude;
        //mousePos /= magnitude;
        mousePos.z = -1;

        jumpLine.SetPosition(0, playerPos);
        jumpLine.SetPosition(1, mousePos);
    }
}
