using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform playerObject;
    private Rigidbody rb;
    private Controller ct;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slopeMultiplier;
    public float slideYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode slideKey = KeyCode.C;
    private float horizontalInput;
    private float verticalInput;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ct = GetComponent<Controller>();

        startYScale = playerObject.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && !ct.wallrunning)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && ct.sliding)
        {
            StopSlide();
        }
    }

    private void FixedUpdate()
    {
        
        if(ct.sliding)
        {
            SlidingMovement();
        }

    }

    private void StartSlide()
    {
        ct.sliding = true;

        playerObject.localScale = new Vector3(playerObject.localScale.x, slideYScale, playerObject.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!ct.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }
        else
        {
            
            rb.AddForce(ct.SlopeMoveDirection(inputDirection) * slideForce * slopeMultiplier, ForceMode.Force);
            rb.AddForce(Vector3.down * slideForce * 0.5f, ForceMode.Force);
        }

        if (slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        ct.sliding = false;

        playerObject.localScale = new Vector3(playerObject.localScale.x, startYScale, playerObject.localScale.z);
    }

}
