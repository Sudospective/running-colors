    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Unity.VisualScripting;
    using UnityEngine;

    public class WallRunning : MonoBehaviour
    {
        [Header("WallRunning")]
        public LayerMask whatIsWall;
        public LayerMask whatIsGround;
        public float wallRunForce;
        public float wallJumpUpForce;
        public float wallJumpSideForce;
        public float wallClimbSpeed;
        public float maxWallRunTime;
        private float wallRunTimer;

        [Header("Input")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode upwardsRunKey = KeyCode.LeftShift;
        public KeyCode downwardsRunKey = KeyCode.LeftControl;
        private bool upwardsRunning;
        private bool downwardsRunning;
        private float horizontalInput;
        private float verticalInput;

        [Header("Detection")]
        public float wallCheckDistance;
        public float minJumpHeight;
        private RaycastHit leftWallhit;
        private RaycastHit rightWallhit;
        private bool wallLeft;
        private bool wallRight;

        [Header("Exit")]
        public bool exitingWall;
        public float exitWallTime;
        private float exitWallTimer;

        [Header("Gravity")]
        public bool useGravity;
        public float gravityCounterForce;

        [Header("References")]
        public Transform orientation;
        private Controller ct;
        private Rigidbody rb;
        private LedgeGrabbing grab;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            ct = GetComponent<Controller>();
            grab = GetComponent<LedgeGrabbing>();
        }

        private void Update()
        {
            CheckForWall();
            StateMachine();
        }

        private void FixedUpdate()
        {
            if (ct.wallrunning)
                WallRunningMovement();
        }

        private void CheckForWall()
        {
            wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
            wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        }

        private bool AboveGround()
        {
            return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
        }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        // State 1 - Wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            if (!ct.wallrunning)
                StartWallRun();

            // wall jump
            if (Input.GetKeyDown(jumpKey)) WallJump();
        }
        // State 2 - Exiting
        else if (exitingWall)
        {
            if (ct.wallrunning)
                StopWallRun();

            if (exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;

            if (exitWallTimer <= 0)
                exitingWall = false;
        }
        // State 3 - None
        else
        {
            if (ct.wallrunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
        {
            ct.wallrunning = true;

            wallRunTimer = maxWallRunTime;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }

        private void WallRunningMovement()
        {
            rb.useGravity = useGravity;

            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

            Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

            if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
                wallForward = -wallForward;

            rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

            if (upwardsRunning)
                rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
            if (downwardsRunning)
                rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

            if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
                rb.AddForce(-wallNormal * 100, ForceMode.Force);

            if (useGravity)
                rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }

        private void StopWallRun()
        {
            ct.wallrunning = false;
        }

    private void WallJump()
    {
        if (grab.holding || grab.exitingLedge) return;

        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
