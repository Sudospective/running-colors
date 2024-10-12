using UnityEngine;

[CreateAssetMenu(fileName = "NotificationSC")]
public class NotificationScriptable : ScriptableObject
{
    [Header("Message Customization")]

    [TextArea] public string notificationMessage;

    [Header("Notification Removal")]

    public bool removeAfterExit;
    public bool disableAfterTimer;
    public float disableTimer;
}
