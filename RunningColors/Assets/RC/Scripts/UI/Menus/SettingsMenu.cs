using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Tooltip("X sensitivity value text")]
    [SerializeField] TMP_Text SensXValueText;

    [Tooltip("X sensitivity value shadow")]
    [SerializeField] TMP_Text SensXValueShadow;

    [Tooltip("Y sensitivity value text")]
    [SerializeField] TMP_Text SensYValueText;

    [Tooltip("Y sensitivity value shadow")]
    [SerializeField] TMP_Text SensYValueShadow;

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
            lookSensXSlider.value = UserSettings.Instance.mouseSensitivityX;
            lookSensYSlider.value = UserSettings.Instance.mouseSensitivityY;

            OnXSensitivityChanged(UserSettings.Instance.mouseSensitivityX);
            OnYSensitivityChanged(UserSettings.Instance.mouseSensitivityY);

            lookSensXSlider.onValueChanged.AddListener(OnXSensitivityChanged);
            lookSensYSlider.onValueChanged.AddListener(OnYSensitivityChanged);
        }
    }

    void Update()
    {
        SensXValueText.text = ((int)lookSensXSlider.value).ToString();
        SensXValueShadow.text = ((int)lookSensXSlider.value).ToString();

        SensYValueText.text = ((int)lookSensYSlider.value).ToString();
        SensYValueShadow.text = ((int)lookSensYSlider.value).ToString();
    }

    void OnXSensitivityChanged(float newValue)
    {
        cameraController.sensX = newValue;
        UserSettings.Instance.mouseSensitivityX = newValue;
    }

    void OnYSensitivityChanged(float newValue)
    {
        cameraController.sensY = newValue;
        UserSettings.Instance.mouseSensitivityY = newValue;
    }
}
