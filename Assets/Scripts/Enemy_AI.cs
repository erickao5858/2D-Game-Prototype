﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    public float speed,jumpforce;
    private float leftx, rightx;
    private bool Faceleft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(Faceleft)
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpforce);
            }
            if (transform.position.x < leftx)//as it pass left point,turn right
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpforce);
            }
            if (transform.position.x > rightx)//as it pass right point,turn left
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if(Coll.IsTouchingLayers(Ground)&&Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }
}
