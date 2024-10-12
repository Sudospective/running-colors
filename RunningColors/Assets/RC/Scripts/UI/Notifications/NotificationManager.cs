using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [Tooltip("UI panel containing the CanvasGroup for displaying notifications")]
    [SerializeField] RectTransform notificationPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ToastNotification(NotificationScriptable notification)
    {
        NotificationToast toast = notificationPanel.GetComponent<NotificationToast>();

        if (toast)
        {
            toast.Initialize(notification);
        }
    }
}
