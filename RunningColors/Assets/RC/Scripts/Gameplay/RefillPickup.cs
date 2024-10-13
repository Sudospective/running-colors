using UnityEngine;

public class RefillPickup : MonoBehaviour
{
    [Tooltip("Scriptable object with notification message for refill pickup")]
    [SerializeField] NotificationScriptable notification;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.GetInstance().PaintCur = GameManager.GetInstance().paintMax;

            NotificationManager.Instance.ToastNotification(notification);
            
            Destroy(gameObject);
        }
    }
}
