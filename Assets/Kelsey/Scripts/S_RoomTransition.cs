using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RoomTransition : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    public PolygonCollider2D nextScreen;

    void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            confiner.m_BoundingShape2D = nextScreen;
        }
    }
}
