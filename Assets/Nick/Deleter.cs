using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public bool isDestroy = false;
    public float timer;
    public float TotalTime = 0.5f;

    private void OnEnable()
    {
        isDestroy = true;
    }

    private void Update()
    {
        if (isDestroy)
        {
            timer +=Time.deltaTime;

            if(timer >= TotalTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
