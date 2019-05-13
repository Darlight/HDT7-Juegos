using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Characteristics for the player controller
    private Rigidbody2D rb2d;
    private bool hasPowerUP;
    private Animator animator;
    private float movement = 10f;
    private float jump = 600f;
    private bool facingRight = true;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hasPowerUP = false;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d)
            if (Input.GetButtonDown("Jump"))
                if(Mathf.Abs(rb2d.velocity.y) < 0.05f)
                    //Just adds the jumping force to able to change a the Y-axis.
                    rb2d.AddForce(new Vector2(0, jump));
    }

    //Update is called once per frame
    private void FixedUpdate()
    {
        // Gets the the x-axis, and multiplies it with the movement
        float movementx = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(movementx * movement, rb2d.velocity.y);
        // Grabs the same speed with movementx to get the running animation
        animator.SetFloat("Speed", movementx* movement);
        
        //Just checks what is facing the character.
        if (movementx > 0f && !facingRight)
            Flip();
        else if (movementx < 0f && facingRight)
            Flip();

    }
    private void Flip()
    {
        //Always check if it's facing right or left, making the character flip.
        facingRight = !facingRight;
        sprite.flipX = !sprite.flipX;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If it encounter any object with the tag "Coin", it destroys it and gains a coin to get a superpower
        if (collision.gameObject.name == "Coin")
        {
            Destroy(collision.gameObject);
            hasPowerUP = true;
            //Just changes the color
            this.sprite.color = Color.blue;
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detects an enemt and the power up variable, compares it and decides if it's destroyed.
        if (collision.gameObject.name == "Enemy" && hasPowerUP != true)
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.name == "Enemy" && hasPowerUP == true)
        {
            Destroy(collision.gameObject);
        }
    }
}

