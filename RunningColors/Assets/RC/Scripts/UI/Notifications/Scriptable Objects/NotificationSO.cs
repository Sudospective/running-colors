using UnityEngine;

[CreateAssetMenu(fileName = "NotificationSO", menuName = "Notifications/NotificationSO")]
public class NotificationSO: ScriptableObject
{
    [Header("Message Customization")]

    [TextArea] public string notificationMessage;

    [Header("Notification")]

    [Tooltip("Prefab for the notifications")]
    public GameObject notificationPrefab;

    [Header("Notification Removal")]

    [Tooltip("The notification will disable after a period of time")]
    public bool disableAfterTimer;

    [Tooltip("The amount of time the notification will be displayed")]
    public float disableTimer;

    [Tooltip("The notification will disable after pressing a key")]
    public bool removeByKey;

    [Tooltip("The key to press to remove the notification")]
    public KeyCode removeKey = KeyCode.F;
}
