using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float climbSpeed;

    public float dashSpeed;
    public float dashSpeedChange;

    public float maxYSpeed; 

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("References")]
    public Climbing climbingScript;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Wall Check")]
    public LayerMask whatIsWall;
    public bool isWallrunning;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitSlope;

    public Transform orientation;

    [Header("Paint")]
    public List<PaintStats> availablePaints;
    public Transform shotPosition;
    public float shotCooldown;
    public TMP_Text paintCurrent;
    PaintType interactivePaintType;
    bool canShoot;

    private int currentlyUsedPaintIndex;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        wallrunning,
        climbing,
        crouching,
        dashing,
        sliding,
        air
    }

    public bool sliding;
    public bool crouching;
    public bool dashing;
    public bool wallrunning;
    public bool climbing;

    public bool freeze;
    public bool unlimited;

    public bool restricted;

    private Collider paintSurface;

    private float paintSpeedMult;
    private float paintJumpMult;

    private void Start()
    {
        GameManager.GetInstance().paintCur = GameManager.GetInstance().paintMax;

        if (paintCurrent != null)
            paintCurrent.text = GameManager.GetInstance().paintCur.ToString();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (availablePaints.Count > 0)
        {
            canShoot = true;
        }
        interactivePaintType = PaintType.None;

        startYScale = transform.localScale.y;

        paintSpeedMult = 1.0f;
        paintJumpMult = 1.0f;

        currentlyUsedPaintIndex = 0;
    }

    private void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (!PauseMenuManager.GetPauseState())
        {
            MyInput();
            SpeedControl();
            StateHandler();
        }

        if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        CheckWallrunning();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || canShoot)
        {
            return;
        }
        PaintBrush brush = other.GetComponent<PaintBrush>();
        if (brush != null)
        {
            canShoot = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        PaintType originalPaintType = interactivePaintType;
        IPaint paint = other.GetComponent<IPaint>();
        if (paint != null)
        {
            Vector3 pos = other.ClosestPoint(transform.position);
            interactivePaintType = paint.GetSurfacePaintType(pos);
        }
        else
        {
            interactivePaintType = PaintType.None;
        }
        if (originalPaintType != interactivePaintType)
        {
            ReactToPaint(interactivePaintType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || !isGrounded)
        {
            return;
        }

        PaintType originalPaintType = interactivePaintType;
        interactivePaintType = PaintType.None;
        if (originalPaintType != interactivePaintType)
        {
            ReactToPaint(interactivePaintType);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrounded)
        {
            PaintType originalPaintType = interactivePaintType;
            interactivePaintType = PaintType.None;
            if (originalPaintType != interactivePaintType)
            {
                ReactToPaint(interactivePaintType);
            }
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ReactToPaint(PaintType paintType)
    {
        switch (paintType)
        {
            case PaintType.Speed:
                // Increase player top speed
                paintSpeedMult = GameManager.GetInstance().speedPaintMult;
                break;
            case PaintType.Jump:
                // Increase player jump height
                paintJumpMult = GameManager.GetInstance().jumpPaintMult;
                break;
            case PaintType.Stick:
                // Decrease player top speed and jump height
                paintSpeedMult = 1.0f / GameManager.GetInstance().speedPaintMult;
                paintJumpMult = 1.0f / GameManager.GetInstance().jumpPaintMult;
                break;
            case PaintType.None:
                // Reset top speed and jump height if on ground
                paintSpeedMult = 1.0f;
                paintJumpMult = 1.0f;
                break;
        }
    }

    public void UpdatePaintStatus()
    {
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Time.timeScale == 0) return;

        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetButton("Fire1") && canShoot)
        {
            ShootPaint();
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);

        }
        if (Input.GetButtonDown("Fire2") && availablePaints.Count > 0)
        {
            currentlyUsedPaintIndex++;
            while (currentlyUsedPaintIndex >= availablePaints.Count)
            {
                currentlyUsedPaintIndex -= availablePaints.Count;
            }
        }
    }

    private void ShootPaint()
    {
        if (!canShoot)
            return;
        GameManager.GetInstance().paintCur--;
        if (GameManager.GetInstance().paintCur <= 0)
        {
            canShoot = false;
            return;
        }
        UpdatePaintUI();
        PaintGlob paint = Instantiate(GameManager.GetInstance().paintGlob, shotPosition.position, GameManager.GetInstance().mainCamera.transform.rotation).GetComponent<PaintGlob>();
        paint.paintType = availablePaints[currentlyUsedPaintIndex].type;
        paint.paintColor = availablePaints[currentlyUsedPaintIndex].color;
        ToggleShooting();
        Invoke("ToggleShooting", shotCooldown);
    }

    private void ToggleShooting()
    {
        canShoot = !canShoot;
    }

    private void UpdatePaintUI()
    {
        //paintCurrent.text = "Paint: " + currentPaint.ToString();
        Debug.Log("Paint Left: " + GameManager.GetInstance().paintCur.ToString());
        
        if (paintCurrent != null)
            paintCurrent.text = GameManager.GetInstance().paintCur.ToString();

        if (GameManager.GetInstance().paintCur <= 0)
        {
            canShoot = false;
        }
    }

    bool keepMomentum;
    private void StateHandler()
    {
        if (freeze)
        {
            state = MovementState.freeze;
            rb.velocity = Vector3.zero;
            desiredMoveSpeed = 0;
        }
        else if (unlimited)
        {
            state = MovementState.unlimited;
            desiredMoveSpeed = 999f;
            return;
        }
        else if(climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }

       else if(wallrunning)
       {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
       }

       else  if(sliding)
       {
            state = MovementState.sliding;

            if(OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
                keepMomentum = true;
            }

            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
       }
       else if (dashing)
       {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChange;
            GetComponent<PlayerAudio>().PlayDashSound();
       }
        else
        {
            if(lastState == MovementState.dashing)
            {
                GetComponent<PlayerAudio>().ResetDashSound();
            }
            if (Input.GetKey(crouchKey))
            {
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
            }
            else if (isGrounded && Input.GetKey(sprintKey) && IsMoving())
            {
                state = MovementState.sprinting;
                desiredMoveSpeed = sprintSpeed;
            }
            else if (isGrounded && IsMoving())
            {
                state = MovementState.walking;
                desiredMoveSpeed = walkSpeed;
            }
            else
            {
                state = MovementState.air;

                if (desiredMoveSpeed < sprintSpeed)
                    desiredMoveSpeed = walkSpeed;
                else
                    desiredMoveSpeed = sprintSpeed;
            }
            desiredMoveSpeed *= paintSpeedMult;
        }

        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        bool moveSpeedChange = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (moveSpeedChange)
        {
            if(keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }

        bool desieredMoveSpeedChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if(desieredMoveSpeedChanged)
        {
            if(keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpDashSpeed());

            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

        if(Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1) keepMomentum = false;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;
            
            yield return null;
        }
        moveSpeed = desiredMoveSpeed;
    }

    public float speedChangeFactor;
    private IEnumerator SmoothlyLerpDashSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }
        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void MovePlayer()
    {
        if(restricted) return;

        if (state == MovementState.dashing) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitSlope)
        {
            rb.AddForce(SlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        else if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if (!wallrunning) rb.useGravity = !OnSlope();
    }


    private void SpeedControl()
    {
        if (OnSlope() && !exitSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else 
        {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

            }
        }

        if(maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }

    }

    private void Jump()
    {
        exitSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce * paintJumpMult, ForceMode.Impulse);

        GetComponent<PlayerAudio>().PlayJumpSound();
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitSlope = false;
    }

    public bool IsMoving()
    {
        return horizontalInput != 0 || verticalInput != 0;
    }

    public bool IsWallLeft()
    {
        return Physics.Raycast(transform.position, -orientation.right, 1f, whatIsWall);
    }
    public bool IsWallRight()
    {
        return Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
    }

    private void CheckWallrunning()
    {
        if(IsWallLeft() || IsWallRight() && !isGrounded && IsMoving())
        {
            isWallrunning = true;
        }
        else
        {
            isWallrunning = false;
        }
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 SlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}