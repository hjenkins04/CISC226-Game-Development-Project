using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    //Changeable variables
    public float speed;
    public float forwardJumpSpeed;
    public float jumpForce;
    public float downwardsRaycastDistance;

    private Rigidbody2D rb;

    //Visible private variable 
    [SerializeField]
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //Ged rigidbody component of the player game object
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
        }
        //Maybe want less mobility in the air
        /*else
        {
            rb.velocity = new Vector2(horizontalInput * (speed/4), rb.velocity.y);
        }*/
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
        }
    }

}
