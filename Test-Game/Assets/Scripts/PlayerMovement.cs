using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.Animations;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{

    public LayerMask ground;
    public Rigidbody2D rb;

    public Animator anim;
    // walking
    [Header("Horiznotal")]
    public float step;
    public float inAirStep;
    private bool isWalking;
    // jump
    [Header("Vertical")]
    private bool on_ground;
    public float jump_force;
    public Transform groundPoint;
    // wall jump
    [Header("Wall Jump")]
    public Transform wallGrabPoint;
    public float wallJumpTime=.3f, wallJumpForce;
    private float wallCounter=0f;
    private bool canGrab, isGrabbing;
    // dash
    [Header("Dash")]
    public float dashTime;
    public float dashForce;
    private float dashTimer;
    private bool dashCouldown=true;
    private float dashCouldownTimer=0f;
    public float dashCouldownTime;
    private float dashDir;
    void Start()
    {
        // get a rigidbody component
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        // check inputs if not wall jump
        if (wallCounter <= 0f)
        {
            CheckInputs();   
        }
        else if (wallCounter > 0f)
        {
            wallCounter -= Time.deltaTime;
        }
        // dash couldown
        // to fix                                                                                                   
        if (!dashCouldown && (on_ground||isGrabbing) && dashTimer<=0f && dashCouldownTimer<=0f)
        {
            dashCouldown = true;
        }
        if (dashCouldownTimer > 0f)
        {
            dashCouldownTimer -= Time.deltaTime;
        }
        // check if player is on the ground(Layer ID:8)
        CheckIfGrounded();
        // animation stuff
        animationStuff();
    }

    void CheckInputs()
    {
        // dont move while dash
        if (dashTimer <= 0)
        {
            dashTimer = 0f;
            float dirx = Input.GetAxisRaw("Horizontal");
            if (on_ground)
            {
                rb.velocity = new Vector2(dirx*step, rb.velocity.y);   
            }
            else
            {
                rb.velocity = new Vector2(dirx * inAirStep, rb.velocity.y);
            }
            if (Input.GetButton("Jump") && on_ground && !isGrabbing)
            {
                Jump();
            }
            else if (Input.GetButton("Dash") && dashCouldown)
            {
                dash();
            }
            // wall jump
            wallJump();
        }
        else
        {
            rb.velocity = new Vector2(dashDir * dashForce, rb.velocity.y);
            dashTimer -= Time.deltaTime;
        }
    } 

    void CheckIfGrounded()
    {
        on_ground = false;
        // to fix there is a more efficent way to do it
        on_ground = Physics2D.OverlapCircle(groundPoint.position, .1f, ground) || Physics2D.OverlapCircle(new Vector3(groundPoint.position.x+.2f, groundPoint.position.y, groundPoint.position.z ), .1f, ground) || Physics2D.OverlapCircle(new Vector3(groundPoint.position.x-.2f, groundPoint.position.y, groundPoint.position.z ), .1f, ground);
    }

    void Jump()
    {
        // normal jump
        rb.velocity = new Vector2(rb.velocity.x, jump_force);
    }
    
    // handle wall jump
    void wallJump()
    {
        canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, ground);
        
        isGrabbing = false;
        if (canGrab && !on_ground)
        {
            // don't ask
            if (transform.localScale.x/Mathf.Abs(transform.localScale.x)==Input.GetAxisRaw("Horizontal")/Mathf.Abs(Input.GetAxisRaw("Horizontal")))
            {
                isGrabbing = true;
            }
        }

        rb.gravityScale = 2f;
        if (isGrabbing)
        {
            if (rb.velocity.y <= 0f)
            {
                rb.gravityScale = 1f;   
            }
            if (Input.GetButton("Jump"))
            {
                wallCounter = wallJumpTime;
                rb.velocity = new Vector2(transform.localScale.x/Mathf.Abs(transform.localScale.x)*-1f*wallJumpForce, jump_force);
            }
        }
    }

    void animationStuff()
    {
        // unity is stupid
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0f && on_ground)
        {
            isWalking = true;
        }else
        {
            isWalking = false;  
        }
        anim.SetBool("isWalking", isWalking);
        anim.SetFloat("speed", step);
    }

    void dash()
    {
        if (dashCouldownTimer <= 0f)
        {
            // direction of dash is based on direction of player
            dashDir = transform.localScale.x/Mathf.Abs(transform.localScale.x);
            dashTimer = dashTime;
            dashCouldown = false;
            dashCouldownTimer = dashCouldownTime;
        }
    }
}