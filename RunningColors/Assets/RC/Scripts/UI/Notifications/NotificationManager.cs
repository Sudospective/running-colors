using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] Canvas HUDCanvas;

    [SerializeField] NotificationToastEventPublisher _notificationPublisher;

    GameObject _notificationInstance;

    private void Start()
    {
        _notificationPublisher.OnToastNotification += OnToastNotification;
    }

    public void OnToastNotification(NotificationSO notification)
    {
        if (HUDCanvas != null)
            _notificationInstance = Instantiate(notification.notificationPrefab, HUDCanvas.transform);

        if (_notificationInstance != null)
        {
            NotificationToast toast = _notificationInstance.GetComponent<NotificationToast>();

            if (toast)
            {
                toast.Initialize(notification);
            }
        }
    }
}
