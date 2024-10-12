using UnityEngine;

public class RefillPickup : MonoBehaviour
{
    [SerializeField] NotificationScriptable notification;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotificationManager.Instance.ToastNotification(notification);
            Destroy(gameObject);
        }
    }
}
