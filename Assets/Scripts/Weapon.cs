using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponBody;
    public TrailRenderer tr;

    public float rotationSpeed = 800f; // Adjust the speed as needed
    private bool isRotating = false;

    private bool isRight;
    private Quaternion initialRotation;

    // Cooldown
    public float cooldownDuration = 2f;
    private float nextActivationTime = 0f;


    void Start()
    {
        tr.emitting = false;
        weaponBody.SetActive(false); 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextActivationTime)
        {
            nextActivationTime = Time.time + cooldownDuration;

            //Start rotation is pointed at mouse
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to world space
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));

            //Determine whether the mouse is to the right or left of the player
            if (mousePosition.x >= transform.position.x)
            {
                isRight = true;
            }
            else
            {
                isRight = false;
            }


            // Calculate the rotation angle
            Vector2 lookDirection = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

            if (isRight)
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0f, 0f, 60f);

                 
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0f, 0f, -60f);

                
            }

            initialRotation = transform.rotation;

            // Start rotating
            isRotating = true;

            //tr.emitting = true;
            weaponBody.SetActive(true);
        }

        /*
        // Check if currently rotating
        if (isRotating)
        {
            // Calculate the rotation angle based on speed and time
            float rotationAmount = rotationSpeed * Time.deltaTime;

            if (isRight)
            {
                // Rotate the GameObject around the Z-axis
                transform.Rotate(Vector3.back, rotationAmount);
            }
            else
            {
                // Rotate the GameObject around the Z-axis
                transform.Rotate(Vector3.forward, rotationAmount);
            }

            // Check if the desired rotation angle (120 degrees) is reached
            if (Mathf.Abs(transform.rotation.eulerAngles.z - initialRotation.eulerAngles.z) >= 120f)
            {
                // Stop rotating when the desired angle is reached
                isRotating = false;
                tr.emitting = false;  
            }
        }
        */


    }

    private void LateUpdate()
    {
        if (isRotating)
        {
            if(tr.emitting == false)
            {
                tr.emitting = true;
            }
            // Calculate the rotation angle based on speed and time
            float rotationAmount = rotationSpeed * Time.deltaTime;

            if (isRight)
            {
                // Rotate the GameObject around the Z-axis
                transform.Rotate(Vector3.back, rotationAmount);
            }
            else
            {
                // Rotate the GameObject around the Z-axis
                transform.Rotate(Vector3.forward, rotationAmount);
            }

            // Check if the desired rotation angle (120 degrees) is reached
            if (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, initialRotation.eulerAngles.z)) >= 120f)
            {
                // Stop rotating when the desired angle is reached
                isRotating = false;
                tr.emitting = false;
                weaponBody.SetActive(false);
            }
        }
    }

}
