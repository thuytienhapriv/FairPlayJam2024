using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump2 : MonoBehaviour
{
    public float jumpVelocity;
    public float jumpMultiplier;
    public float fallMultiplier;

    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            Debug.Log("jump");
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity ;
        }


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        } else if ((rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.W)) )
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier) * Time.deltaTime;
        }
    }
}
