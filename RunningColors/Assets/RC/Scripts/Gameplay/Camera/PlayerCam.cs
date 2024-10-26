using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    public float walkBobbingSpeed = 14f;
    public float sprintBobbingSpeed = 18f;
    public float bobbingAmount = 0.05f;

    public float wallrunTiltAngle = 15f;
    public float tiltSpeed = 5f;

    public float defaultFOV = 60f;
    public float dashFOV = 80f;
    public float fovTransitionSpeed = 5f;

    private float defaultYPos;
    private float bobTimer;
    private float currentTilt = 0f;

    private Controller playerController;
    private Camera cam;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        if (UserSettings.Instance == null)
        {
            Debug.LogError("UserSettings Instance is not initialized.");
        }
        else
        {
            sensX = UserSettings.Instance.mouseSensitivityX;
            sensY = UserSettings.Instance.mouseSensitivityY;
        }
        defaultYPos = transform.localPosition.y;
        cam = GetComponent<Camera>();
        playerController = FindObjectOfType<Controller>();

        ApplyUserSettings();
    }

    private void Update()
    {
        if(playerController == null)
        {
            playerController = FindObjectOfType<Controller>();
            if(playerController == null )
            {
                return;
            }
        }
        
        
        HandleCameraRotation();
        HandleCameraBobbing();
        HandleWallrunTilt();
        HandleFOVChanges();
    }

    private void ApplyUserSettings()
    {
        if (UserSettings.Instance != null)
        {
            sensX = UserSettings.Instance.mouseSensitivityX;
            sensY = UserSettings.Instance.mouseSensitivityY;
        }
    }

    private void HandleFOVChanges()
    {
        if (playerController.state == Controller.MovementState.dashing)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, dashFOV, Time.deltaTime * fovTransitionSpeed);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, Time.deltaTime * fovTransitionSpeed);
        }
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;


        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void HandleCameraBobbing()
    {
        
        if (playerController.state == Controller.MovementState.walking || playerController.state == Controller.MovementState.sprinting)
        {
            //use sprint speed if sprinting
            float bobbingSpeed = playerController.state == Controller.MovementState.sprinting ? sprintBobbingSpeed : walkBobbingSpeed;

            //Calculate new y pos
            bobTimer += Time.deltaTime * bobbingSpeed;
            float newY = defaultYPos + Mathf.Sin(bobTimer) * bobbingAmount;

            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
        else
        {
            bobTimer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultYPos, transform.localPosition.z);
        }
    }

    private void HandleWallrunTilt()
    {

        if (playerController.wallrunning)
        {
            //find tilt direction
            float targetTilt = playerController.IsWallLeft() ? -wallrunTiltAngle : wallrunTiltAngle;
            //tilt the camera
            currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
        }
        else
        {
            //reset camera when not wallrunning
            currentTilt = Mathf.Lerp(currentTilt, 0, Time.deltaTime * tiltSpeed);
        }

        transform.rotation = Quaternion.Euler(xRotation, yRotation, currentTilt);
    }

    public void SetPlayerController(Controller controller)
    {
        playerController = controller;
    }

}
