using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
        readyToJump = true;
    }

    private void Update()
    {
        // Perform the raycast to check if the player is grounded
        isGrounded = Physics.Raycast(transform.position + Vector3.down * 0.1f, Vector3.down, 0.5f, whatIsGround);
        Debug.Log("Grounded Raycast Result: " + isGrounded);

        // Visualize the raycast in the scene view
        Vector3 rayStart = transform.position + Vector3.down * 0.1f;
        Debug.DrawRay(rayStart, Vector3.down * 0.5f, isGrounded ? Color.green : Color.red);

        // Log additional information
        Debug.Log("Ray starting point: " + rayStart);
        Debug.Log("Ray direction: " + (Vector3.down * 0.5f));
        Debug.Log("Current Position: " + transform.position);

        MyInput();
        SpeedControl();

        // Set drag based on ground state
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer(); 
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Debug.Log("isGrounded: " + isGrounded);

        if (Input.GetKeyDown(jumpKey))
        {
            Debug.Log("Jump key pressed");
        }

        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            Debug.Log("Jump key pressed, grounded, and ready to jump");

            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
       moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new  Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        Debug.Log("Jump triggered with force: " + jumpForce);
        Debug.Log("Jump force applied: " + jumpForce);
    }

    private void ResetJump()
    {
        readyToJump = true;

        Debug.Log("Reset Jump Called");
    }
}
