using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    //Aerial Movement Variables
    private LineRenderer lr;
    private Vector2 grapplePoint;
    public LayerMask Grappleable;
    public Transform direction;
    private DistanceJoint2D joint;
    public float GrappleDistance;


    //Ground Movement variables
    public float speed;
    public float jumpForce;
    public float downwardsRaycastDistance;

    private Rigidbody2D rb;

    //Visible private variable 
    [SerializeField]
    private bool isGrounded;

    void Awake()
    {
        // get the line renderer component attached to player
        lr = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody component of the player game object
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input axis for horizontal movement.
        float horizontalInput = Input.GetAxis("Horizontal");

        //If on ground
        if (isGrounded)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            //jump when w is pressed
            if (Input.GetKeyDown(KeyCode.W))
            {
                //jump by adding an impulse force upwards
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (joint)
            {
                //Allowing the joint distance to decrease while the player is grounded so they don't get stuck
                joint.maxDistanceOnly = true;
            }
        }
        //Maybe want less mobility in the air
        /*else
        {
            rb.velocity = new Vector2(horizontalInput * (speed/4), rb.velocity.y);
        }*/

        //Grapple inputs
        //On left click down
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clicked");
            shoot();
        }
        //On left click release
        else if (Input.GetMouseButtonUp(0))
        {
            release();
        }
    }
  
    //check for when the player first touches an object
    void OnCollisionEnter2D(Collision2D collision)
    {
        //When the collision is on a platform
        if(collision.gameObject.CompareTag("Platform"))
        {
            //downwards raycast to check if the platform is below the player, we don't want to say the player is grounded when its their head touching the bottom of a platform
            RaycastHit2D rcHit = Physics2D.BoxCast(transform.position, new Vector2(1f, 1f), 0f, Vector2.down, downwardsRaycastDistance);
            
            //Check if there is a hit
            if (rcHit.collider != null)
            {
                //player is on the ground
                isGrounded = true;

                
            }          
        }
    }

    //Same thing as collision enter, but its exit.
    private void OnCollisionExit2D(Collision2D collision)
    {
        //If they exited a platform, the player is not on the ground anymore
        if(collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;

            if (joint)
            {
                //fixing the joint distance once the player leaves the ground
                joint.maxDistanceOnly = false;
            }
        }
    }

    private void LateUpdate()
    {
        //Draw rope every late update
        drawRope();
    }

    void shoot()
    {
        //Send a raycast in the direction of the cursor, only detecting the grappleable layer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.up, GrappleDistance, Grappleable);
        if (hit.collider != null)
        {
            //Debug.Log("Hit");

            //Variable to hold the information on the hit position 
            grapplePoint = hit.point;
            
            //Create new joint component
            joint = gameObject.AddComponent<DistanceJoint2D>();
            //Initialization stuff
            joint.autoConfigureConnectedAnchor = false;
            joint.maxDistanceOnly = false;
            joint.enableCollision = true;
            //Set an anchor point as the grapple point, the other one is automatically the player position
            joint.connectedAnchor = grapplePoint;

            //Set up line renderer points
            lr.positionCount = 2;
        }
    }

    void drawRope()
    {
        //Don't draw the rope if there is no joint object present
        if (!joint)
        {
            return;
        }
        //If there are joints, draw from the player pos to the point grappled
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, grapplePoint);
    }

    void release()
    {
        //Remove line renderer draw positions
        lr.positionCount = 0;
        //Destroy the joint component
        Destroy(joint);
    }
}
