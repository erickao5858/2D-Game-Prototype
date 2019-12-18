using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moverate;
    private float startpointX,startpointY;
    public bool locky;//defaut false
    void Start()
    {
        startpointX = transform.position.x;
        startpointY = transform.position.y;
    }


    void Update()
    {
        if (locky)
        {
            transform.position = new Vector2(startpointX + Cam.position.x * moverate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startpointX + Cam.position.x * moverate, startpointY+Cam.position.y*moverate);
        }
    }
}
