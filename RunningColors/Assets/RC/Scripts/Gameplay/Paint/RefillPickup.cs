using UnityEngine;

public class RefillPickup : MonoBehaviour
{
    [Tooltip("Scriptable object with notification message for refill pickup")]
    [SerializeField] NotificationSO notification;

    [SerializeField] NotificationToastEventPublisher _notificationPublisher;

    private void Update()
    {
        transform.Rotate(0.0f, 360.0f * Time.deltaTime, 0.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.GetInstance().paintCur = GameManager.GetInstance().paintMax;

            if (notification != null)
                _notificationPublisher.RaiseEvent(notification);
            
            Destroy(gameObject);
        }
    }
}
