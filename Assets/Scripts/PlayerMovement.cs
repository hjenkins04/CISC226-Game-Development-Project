using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    //Grapple Variables
    private LineRenderer lr;
    private Vector2 grapplePoint;
    public LayerMask Grappleable;
    public Transform direction;
    private DistanceJoint2D joint;
    public float GrappleDistance;

    //Boost Variables
    private int yRelative;
    public float boostForce;
    public ParticleSystem boostEffect;


    //Ground Movement variables
    public float speed;
    public float jumpForce;
    public float downwardsRaycastDistance;

    private Rigidbody2D rb;

    //Visible private variable 
    [SerializeField]
    private bool isGrounded;

    //Animator
    private Animator anim;
    public bool facingLeft;
    public int side;

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
        //Get animator component of the player game object
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        //Input axis for horizontal movement.
        float horizontalInput = Input.GetAxis("Horizontal");

        //FIX - Player animation but breaks movement

        //float verticalInput = Input.GetAxis("Vertical");
        //Vector2 direction = new Vector2(horizontalInput, verticalInput);
        //Run(direction);

        //anim.SetFloat("HorizontalAxis", direction.x);

        ////Check player direction to flip run animation if necessary.
        ////If moving left and not already facing left, flip to face left.
        //if (horizontalInput < 0 && !facingLeft)
        //{
        //    FlipCharacter(); //Now character will face left.
        //}
        ////If moving right and currently facing left, flip to face right.
        //else if (horizontalInput > 0 && facingLeft)
        //{
        //    FlipCharacter(); //Now character will face right.
        //}

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
        if (Input.GetMouseButton(0) && !joint)
        {
            //Debug.Log("Clicked");
            shoot();
            if (Input.GetKey(KeyCode.Space))
            {
                boostEffect.Play();
            }
        }
        //On left click release
        else if (Input.GetMouseButtonUp(0))
        {
            release();
        }


        //Boost 
        if (joint)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yRelative = getRelativeYPos();
                //Debug.Log(yRelative);

                boostEffect.Play();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                boost();
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || !joint)
        {
            boostEffect.Stop();
        }
    }
    
    public void Run(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    void FlipCharacter()
    {
        //Toggle the state.
        facingLeft = !facingLeft;

        //Flip the character by multiplying the x scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //check for when the player first touches an object
    void OnCollisionEnter2D(Collision2D collision)
    {
        //When the collision is on a platform
        if(collision.gameObject.CompareTag("Platform"))
        {
            //downwards raycast to check if the platform is below the player, we don't want to say the player is grounded when its their head touching the bottom of a platform
            RaycastHit2D rcHit = Physics2D.BoxCast(transform.position, new Vector2(1.9f, 1f), 0f, Vector2.down, downwardsRaycastDistance, Grappleable);
            
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

    int getRelativeYPos()
    {
        if (transform.position.y <= grapplePoint.y)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    void boost()
    {

        Vector2 ropeVector = (grapplePoint - (Vector2)transform.position).normalized;

        //Input axis for horizontal movement.
        float rawHorizontalInput = Input.GetAxisRaw("Horizontal");

        // Create a quaternion representing the 90 degree rotation
        Quaternion rotation = Quaternion.Euler(0, 0, rawHorizontalInput*-90.0f*Mathf.Deg2Rad);

        Vector2 normalVector = rotation * ropeVector;


        float angle = Mathf.Atan2(ropeVector.y, ropeVector.x) * Mathf.Rad2Deg + 90f;

        

        if (yRelative == 0)
        {
            rb.AddForce(normalVector * boostForce, ForceMode2D.Force);

            boostEffect.transform.rotation = Quaternion.AngleAxis((angle + rawHorizontalInput*-90f), Vector3.forward);
        }
        else
        {
            rb.AddForce(-normalVector * boostForce, ForceMode2D.Force);

            boostEffect.transform.rotation = Quaternion.AngleAxis((angle + rawHorizontalInput * 90f), Vector3.forward);
        }
    }
}
