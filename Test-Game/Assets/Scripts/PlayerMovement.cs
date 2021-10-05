using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float max_speed;

    public Vector2 veloctiy;
    
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
        
        // move player
        rb.MovePosition(rb.position + veloctiy * Time.fixedDeltaTime);
    }

    void CheckInputs()
    {
        // magick
    }
}