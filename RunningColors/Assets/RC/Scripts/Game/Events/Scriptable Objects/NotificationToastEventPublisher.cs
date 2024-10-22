using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NotificationToastEventPublisher", menuName = "Notifications/Notification Toast Event Publisher")]
public class NotificationToastEventPublisher : ScriptableObject
{
    public UnityAction<NotificationSO> OnToastNotification;

    public void RaiseEvent(NotificationSO notification)
    {
        OnToastNotification?.Invoke(notification);
    }
}
