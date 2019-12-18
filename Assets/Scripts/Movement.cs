using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator anim;
    public Collider2D coll;
    public float speed ;
    public float jumpforce;
    public LayerMask ground;
    public int Cherry;
    public Text Cherry_Num;
    private bool isHurt;//defaut value is flase
    // Start is called before the first frame update
    public Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
        SwitchAnimation();
    }

      void Move()
    {
        float horziontalmove = joystick.Horizontal;
        float facedirection = joystick.Horizontal;
        //character movement
        if(horziontalmove!=0f)
        {
            rb.velocity = new Vector2(horziontalmove * speed*Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
       
        if(facedirection>0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
                if(facedirection<0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //character jump
        if (joystick.Vertical>0.5f&& coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }

    void SwitchAnimation()
    {
        anim.SetBool("idle", false);
        if(rb.velocity.y<0.1f&&!coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(isHurt)
        {
            
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if(Mathf.Abs(rb.velocity.x)<0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    //Collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Collection")
        {
            Destroy(collision.gameObject);
            Cherry++;
            Cherry_Num.text = Cherry.ToString();
        }
        
    }


    //Kill Enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if(transform.position.x<collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                isHurt = true;
            }
        }
    }


}
