using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static PlayerMovement;
//using static UnityEditor.VersionControl.Asset;

public class PlayerMovement2 : MonoBehaviour
{
    public GameObject groundCheckPointObject;
    public GameObject umbrella;
    public GameObject fillBar;
    public bool holdsUmbrella;
    public enum colors { red, blue, none};
    public colors fillColour;
    public float waterVolume;

    public float moveSpeed;
    public float acceleration;
    public float decelaration;

    public float jumpForce;
    public float jumpMultiplier;
    public float fallMultiplier;
    public float umbrellaFallMultiplier;

    public float velPower;
    public float frictionAmount;

    Rigidbody2D rb;
    public Vector2 direction;
    Vector2 groundCheckPoint;
    Animator anim;
    public GameObject cam;

    public bool isPouring;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Time.timeScale = 1.0f;
        groundCheckPoint = groundCheckPointObject.transform.position;
        holdsUmbrella = true;
        isPouring = false;
        anim = gameObject.GetComponent<Animator>();
    }

    public void SetColour(string colour)
    {
        if (colour == colors.red.ToString())
        {
            fillColour = colors.red;
        }
        else if (colour == colors.blue.ToString())
        {
            fillColour = colors.blue;
        }
    }

    void Update()
    {
        MoveAndJump();        
        ControlJumpSpeed();
        ChangeAnimation();
    }

    private void MoveAndJump()
    {
        if (Input.GetKey(KeyCode.D)) { direction = Vector2.right; }
        else if (Input.GetKey(KeyCode.A)) { direction = Vector2.left; }
        
        else { direction = Vector2.zero; }

        if (Input.GetKeyDown(KeyCode.Space)) { Jump();  }
        if (Input.GetKeyDown(KeyCode.E)) { Umbrella(); }
        if (Input.GetKeyDown(KeyCode.Q)) { HeadBang(); }

        waterVolume = Mathf.Clamp(waterVolume, 0, 99);
    }

    private void ChangeAnimation()
    {
        // flip sprite

        if (direction.x < 0)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z));
        } else if (direction.x > 0)
        {
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z));
        }


        // walking

        if (direction.x != 0) { 
            anim.SetBool("walking", true); // goes to walking with umbrella
        } else
        {
            anim.SetBool("walking", false); // goes to idle with umbrella
        }

    }

    private void Umbrella()
    {
        if (holdsUmbrella == true) { // closing
            holdsUmbrella = false; 
            umbrella.GetComponent<Umbrella>().IsDown(); 
            anim.SetBool("closedUmbrella", true);
        }
        else 
        { // opening
            holdsUmbrella = true; 
            umbrella.GetComponent<Umbrella>().IsHeld();
            anim.SetBool("closedUmbrella", false);
        }
    }

    private void Jump()
    {
        if (CheckIfGrounded() == false) { return; }
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetBool("jumping", true);
        StartCoroutine(CountJumpTime());
    }

    IEnumerator CountJumpTime()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        anim.SetBool("jumping", false);
    }

    private void HeadBang()
    {
        isPouring = true;
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        cam.GetComponent<CameraController>().CameraShake();

        // will take 0.5f
        for (int i = 0; i < 50; i++)
        {
            gameObject.transform.Rotate(0, 0, -7.2f);
            fillBar.GetComponent<FillBarScript>().PourOutWater(2);
            waterVolume -= 2;
            yield return new WaitForSeconds(0.01f);
        }
        isPouring = false;
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
                //Debug.Log("landed");
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
        if (rb.velocity.y < 0) // falling
        {
            if (holdsUmbrella == false)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }else 
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (umbrellaFallMultiplier) * Time.deltaTime;

            }
        }
        else if ((rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.W)))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier) * Time.deltaTime;
            
        }
    }
}
