using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_OpenPause_Erin : MonoBehaviour
{
    public GameObject pauseScreen;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
        }
    }
}
