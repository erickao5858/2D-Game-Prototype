using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    public Transform top,bottom;
    public float Speed;
    private float TopY, BottomY;
    private bool isUp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        TopY = top.position.y;
        BottomY =bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
       if(isUp)
       {
        rb.velocity= new Vector2(rb.velocity.x,Speed);
        if(transform.position.y>TopY)
        {
          isUp = false;
		}
	   }
       else
       {
        rb.velocity= new Vector2(rb.velocity.x,-Speed);
        if(transform.position.y<BottomY)
        {
          isUp = true;  
		}

	   }
	}
}
