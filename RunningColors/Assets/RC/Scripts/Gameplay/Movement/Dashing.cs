using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform cam;
    private Rigidbody rb;
    private Controller ct;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = true;
    public bool restVel = true;

    [Header("Cooldown")]
    public float dashCD;
    private float dashCDTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.R;

    private void Start()
    {
        ct = GetComponent<Controller>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey)) 
        {
            Dash();
        }

        if(dashCDTimer > 0)
            dashCDTimer -= Time.deltaTime;

    }

    private void Dash()
    {
        if (dashCDTimer > 0) return;
        else dashCDTimer = dashCD;
        
        ct.dashing = true;
        ct.maxYSpeed = maxDashYSpeed;

        Transform forwardT;

        if (useCameraForward && cam != null)
            forwardT = cam;
        else
            forwardT = orientation;

        Vector3 direction = GetDirection(forwardT);

        ct.dashing = true;

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if(disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration); 
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        if(restVel)
            rb.velocity = Vector3.zero; 

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);

    }

    private void ResetDash()
    {
        ct.dashing = false;
        ct.maxYSpeed = 0;

        if(disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        if(useCameraForward)
        {
            return forwardT.forward.normalized;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (horizontalInput == 0 && verticalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }

}
