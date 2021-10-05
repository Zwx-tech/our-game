using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    
    public float max_speed;
    // Start is called before the first frame update
    void Start()
    {
        // get a rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
