using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    private LineRenderer lr;
    private Vector2 grapplePoint;
    public LayerMask Grappleable;
    public Transform direction, player;

    private DistanceJoint2D joint;

    public float GrappleDistance;
    // Start is called before the first frame update


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicked");
            shoot();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            release();
        }
    }

    private void LateUpdate()
    {
        drawRope();
    }

    void shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction.up, GrappleDistance, Grappleable);
        if (hit.collider != null)
        {
            Debug.Log("Hit");
            
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<DistanceJoint2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.maxDistanceOnly = false;
            joint.enableCollision = true;

            lr.positionCount = 2;
        }
    }

    void drawRope()
    {
        if (!joint)
        {
            return;
        }
        lr.SetPosition(0, player.transform.position);
        lr.SetPosition(1, grapplePoint);
    }

    void release()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
