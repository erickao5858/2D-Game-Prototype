using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    public Collider2D coll;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int Cherry;
    public Text Cherry_Num;
    private bool isHurt;//defaut value is flase
    // Start is called before the first frame update
    public Joystick joystick;
    [SerializeField]
    private Slider _healthSlider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (_healthSlider != null) _healthSlider.value = GameManager.Instance.CurrentLife / GameManager.Instance.MaxLife * 100f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Jump();
        if (!isHurt)
        {
            Move();
        }
        SwitchAnimation();
        //Debug.Log(GameManager.Instance.CurrentLife);
    }
    private void Update()
    {
        
    }
    void Move()
    {
        float horziontalmove = joystick.Horizontal;
        float facedirection = joystick.Horizontal;
        //character movement
        if (horziontalmove != 0f)
        {
            rb.velocity = new Vector2(horziontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }

        if (facedirection > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void Jump()
    {

        if (joystick.Vertical > 0.5f && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }
    void SwitchAnimation()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {

            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    //Collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry++;
            Cherry_Num.text = Cherry.ToString();
        }

        if (collision.tag == "DeadLine")
        {
            Invoke("Restart", 2f);
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
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                isHurt = true;
                ApplyDamage(1);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                isHurt = true;
                ApplyDamage(1);
            }
        }
    }
    private void ApplyDamage(int damage)
    {
        GameManager.Instance.CurrentLife -= damage;
        _healthSlider.value = GameManager.Instance.CurrentLife / GameManager.Instance.MaxLife * 100f;
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
