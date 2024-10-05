using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    [SerializeField] float jumpingMultiplier; // 15
    [SerializeField] float speedMultiplier; // 2
    [SerializeField] float maxHorizontalSpeed; // 5
    [SerializeField] float gravityScale; // 3
    [SerializeField] float fallGravityScale; // 3.5
    [SerializeField] float umbrellaGravityScale; // 2
    [SerializeField] float umbrellaFallGravityScale; // 2.5
    [SerializeField] GameObject umbrella;

    [Header("Check Values", order = 0)]
    public Vector2 myVelocity;
    public float myGravityScale;
    public string currentState;

    [Header("Variables", order = 1)]
    public bool isHolding;
    public bool isJumping;
    public bool isGrounded;
    public bool grounded;
    public float jumpVelocity;

    public enum playerStates
    {
        walking,
        jumping,
    }
    public playerStates states;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        isHolding = true;
        states = playerStates.walking;
        Time.timeScale = 1.0f;

    }

    void Update()
    {
        // update my variables for debugging / checking
        myVelocity = rb.velocity;
        myGravityScale = rb.gravityScale;
        currentState = states.ToString();

        // control horizonal speed
        if (rb.velocity.x < -maxHorizontalSpeed)
        {
            rb.velocity = new Vector3(-maxHorizontalSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x > maxHorizontalSpeed)
        {
            rb.velocity = new Vector3(maxHorizontalSpeed, rb.velocity.y);
        }

        GravityController(); // adjust gravity & acceleration of movement

        IsGrounded();
        MoveAndJump(); // on ground and platforms
    }
    private void MoveAndJump()
    {
        

        if (Input.GetKeyDown(KeyCode.W)) { PlayerJump(); }
        if (Input.GetKey(KeyCode.A)) { PlayerMove(Vector2.left); }
        if (Input.GetKey(KeyCode.D)) { PlayerMove(Vector2.right); }
        if (Input.GetKeyDown(KeyCode.E)) { Umbrella(); }
    }
    private void PlayerJump()
    {
        // check if on ground - if false, can't jump
        if (isGrounded == false) { return; } // will have to change

        states = playerStates.jumping;
        isGrounded = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
    }


    private void PlayerMove(Vector2 direction)
    {
        //rb.AddForce(direction * speedMultiplier, ForceMode2D.Impulse); // control movement direction
        rb.velocity = new Vector2(direction.x * speedMultiplier, rb.velocity.y);
        states = playerStates.walking;
    }

    

    private void Umbrella()
    {
        if (isHolding == false) { isHolding = true;  } else { isHolding = false; }
        //umbrella.GetComponent<Umbrella>().Position();
        return;
    }

    public void IsGrounded()
    {
        //states = playerStates.walking;
        Physics2D.SyncTransforms();

        foreach (var plat in GameObject.FindGameObjectsWithTag("GroundAndPlatforms"))
        {
            grounded = Physics2D.IsTouching(col, plat.GetComponent<Collider2D>());

            if (grounded == false)
            {
                states = playerStates.jumping;
            }

            // check previous state (isGrounded) and the fresh state (grounded) to conclude if the player is landing
            // isGrounded starts as false when jumping
            if (isGrounded == false && grounded == true) // landing sound
            {
                Debug.Log("landed");
                //AudioManager.instance.Play("Landing");
            }
            isGrounded = grounded;

            // if is currently on ground, return true
            if (grounded == true)
            {
                

                states = playerStates.walking;

                break;
            }
        }
    }

    private void GravityController()
    {
        //rb.gravityScale = fallGravityScale;

        // controls jump curves
        if (states == playerStates.jumping)
        {
            if (rb.velocity.y > 0) // jump up
            {
                Debug.Log("up");
                if (isHolding == false)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityScale) * Time.deltaTime;
                }
                else
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (umbrellaGravityScale) * Time.deltaTime;

                }
                /*
                if (isHolding == false)
                {
                    rb.gravityScale = gravityScale;
                } else
                {
                    rb.gravityScale = umbrellaGravityScale;
                }*/
            }
            else if (rb.velocity.y <= 0) // jump down
            {
                Debug.Log("down");

                if (isHolding == false)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravityScale) * Time.deltaTime;
                }
                else
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (umbrellaFallGravityScale) * Time.deltaTime;

                }

                /*if (isHolding == false)
                {
                    rb.gravityScale = fallGravityScale;
                } else
                {
                    rb.gravityScale = umbrellaFallGravityScale;
                }*/
            }
            //myGravityScale = rb.gravityScale;
        }

    }
}
