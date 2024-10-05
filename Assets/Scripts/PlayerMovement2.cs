using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static PlayerMovement;
using static UnityEditor.VersionControl.Asset;

public class PlayerMovement2 : MonoBehaviour
{
    public float jumpVelocity;
    public float jumpMultiplier;
    public float fallMultiplier;
    public float moveSpeed;
    public float acceleration;
    public float decelaration;
    public float velPower;
    public float frictionAmount;

    Vector2 direction;
    Rigidbody2D rb;
    // Start is called before the first frame update
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
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }

        if (Input.GetKey(KeyCode.D)) { direction = Vector2.right; }
        else if (Input.GetKey(KeyCode.A)) { direction = Vector2.left; }
        else { direction = Vector2.zero; }
        ControlJumpSpeed();

        //CheckFriction();
        
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
