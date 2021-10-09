using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    // walking
    public float step;
    //jump
    public bool on_ground;
    public float jump_height;
    // wall jump
    public int on_wall=0; // between -1 and 1
    // Start is called before the first frame update
    void Start()
    {
        // get a rigidbody component
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check inputs
        CheckInputs();
        // check if player is on the ground(7)
        CheckIfGrounded();
        // move player
    
    }

    void CheckInputs()
    {
        // magick
        float dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx*step, rb.velocity.y);
    } 

    void CheckIfGrounded()
    { 
        on_ground = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction: Vector3.down), 0.8f, LayerMask.GetMask("Ground"));
        if (hit)
        {
            on_ground = true;
        }
        
        Jump();
    }

    void Jump()
    {
        // normal jump
        if (Input.GetButton("Jump") && on_ground)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump_height);
        }
        // wall jump
        if (Input.GetButton("Jump") && on_wall != 0)
        {
            
        }
    }
}