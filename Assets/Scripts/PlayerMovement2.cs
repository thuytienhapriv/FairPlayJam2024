using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static PlayerMovement;
using static UnityEditor.VersionControl.Asset;

public class PlayerMovement2 : MonoBehaviour
{
    Vector2 direction;
    Vector2 groundCheckPoint;
    public GameObject groundCheckPointObject;

    public float moveSpeed;
    public float acceleration;
    public float decelaration;

    public float jumpForce;
    public float jumpMultiplier;
    public float fallMultiplier;
        
    public float velPower;
    public float frictionAmount;

    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Time.timeScale = 1.0f;
        groundCheckPoint = groundCheckPointObject.transform.position;
    }

    void Update()
    {
        MoveAndJump();        
        ControlJumpSpeed();        
    }

    private void MoveAndJump()
    {
        if (Input.GetKey(KeyCode.D)) { direction = Vector2.right; }
        else if (Input.GetKey(KeyCode.A)) { direction = Vector2.left; }
        
        else { direction = Vector2.zero; }

        if (Input.GetKeyDown(KeyCode.W)) { Jump();  }
    }

    private void Jump()
    {
        if (CheckIfGrounded() == false) { return; }
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("jump");
    }

    private bool CheckIfGrounded()
    {
        Physics2D.SyncTransforms();

        foreach (var plat in GameObject.FindGameObjectsWithTag("GroundAndPlatforms"))
        {
            if (Physics2D.IsTouching(
                groundCheckPointObject.GetComponent<Collider2D>(), 
                plat.GetComponent<Collider2D>())) //if is touching
            {
                return true;
            }
            /*if (Physics2D.OverlapBox(groundCheckPoint, plat.GetComponent<Collider2D>().bounds.center - plat.GetComponent<Collider2D>().bounds.min, 0))
            {
                return true;
            }*/
        }

        return false;
    }

    private void FixedUpdate()
    {
        float targetSpeed = direction.x * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decelaration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private void CheckFriction()
    {
        float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
        rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
    }

    private void ControlJumpSpeed()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
        else if ((rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.W)))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier) * Time.deltaTime;
        }
    }
}
