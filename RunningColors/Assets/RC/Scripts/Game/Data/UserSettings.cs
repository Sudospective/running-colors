using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
    private static UserSettings userSettingsInstance;

    public static UserSettings Instance
    {
        get
        {
            if(userSettingsInstance == null)
            {
                GameObject settingsObject = new GameObject("UserSettings");
                userSettingsInstance = settingsObject.AddComponent<UserSettings>();
                DontDestroyOnLoad(settingsObject);
            }
            return userSettingsInstance;
        }
    }

    [Header("Player Settings")]
    public float mouseSensitivityX = 400.0f;
    public float mouseSensitivityY = 400.0f;

    private void Awake()
    {
        if (userSettingsInstance != null && userSettingsInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            userSettingsInstance = this;
            DontDestroyOnLoad(gameObject);
            ApplySettingsToCamera();
        }
    }

    public void SetSensitivity(float x, float y)
    {
        mouseSensitivityX = x;
        mouseSensitivityY = y;
        ApplySettingsToCamera();
    }

    public void ApplySettingsToCamera()
    {
        PlayerCam playerCam = Camera.main?.GetComponent<PlayerCam>();
        if (playerCam != null)
        {
            playerCam.sensX = mouseSensitivityX;
            playerCam.sensY = mouseSensitivityY;
        }
        else
        {
            Debug.LogWarning("PlayerCam not found when applying settings");
        }
    }
}
