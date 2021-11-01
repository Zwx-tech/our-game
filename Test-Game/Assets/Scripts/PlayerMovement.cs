using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{

    public LayerMask ground;
    public Rigidbody2D rb;
    // walking
    [Header("Horiznotal")]
    public float step;
    public float inAirStep;
    
    // jump
    [Header("Vertical")]
    private bool on_ground;
    public float jump_force;
    public Transform groundPoint;
    // wall jump
    [Header("Wall Jump")]
    public Transform wallGrabPoint;

    public float wallCounterTime=.3f, wallJumpForce;
    private float wallCounter;
    private bool canGrab, isGrabbing;

    void Start()
    {
        // get a rigidbody component
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        // check inputs
        if (wallCounter <= 0f)
        {
            CheckInputs();   
        }
        else
        {
            wallCounter -= Time.deltaTime;
        }
        // check if player is on the ground(Layer ID:7)
        CheckIfGrounded();
        // animation stuff
        animationStuff();
    }

    void CheckInputs()
    {
        float dirx = Input.GetAxisRaw("Horizontal");
        if (on_ground)
        {
            rb.velocity = new Vector2(dirx*step, rb.velocity.y);   
        }
        else
        {
            rb.velocity = new Vector2(dirx*inAirStep, rb.velocity.y);   
        }
        if (Input.GetButton("Jump") && on_ground && !isGrabbing)
        {
            Jump();
        }
        // wal jump
        wallJump();
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
                wallCounter = wallCounterTime;
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
    }
}