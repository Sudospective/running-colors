using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Tooltip("X sensitivity value text")]
    [SerializeField] TMP_Text SensXValueText;

    [Tooltip("Y sensitivity value text")]
    [SerializeField] TMP_Text SensYValueText;

    [Tooltip("Slider component for look sensitivity on the x-axis")]
    [SerializeField] Slider lookSensXSlider;

    [Tooltip("Slider component for look sensitivity on the y-axis")]
    [SerializeField] Slider lookSensYSlider;

    PlayerCam cameraController;

    void Start()
    {
        cameraController = GameManager.GetInstance().mainCamera.GetComponent<PlayerCam>();

        if (cameraController != null)
        {
            lookSensXSlider.value = cameraController.sensX;
            lookSensXSlider.onValueChanged.AddListener(OnXSensitivityChanged);

            lookSensYSlider.value = cameraController.sensY;
            lookSensYSlider.onValueChanged.AddListener(OnYSensitivityChanged);
        }
    }

    void Update()
    {
        SensXValueText.text = ((int)lookSensXSlider.value).ToString();
        SensYValueText.text = ((int)lookSensYSlider.value).ToString();
    }

    void OnXSensitivityChanged(float newValue)
    {
        cameraController.sensX = newValue;
    }

    void OnYSensitivityChanged(float newValue)
    {
        cameraController.sensY = newValue;
    }
}
