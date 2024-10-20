using UnityEngine;

public class RefillPickup : MonoBehaviour
{
    [Tooltip("Scriptable object with notification message for refill pickup")]
    [SerializeField] NotificationScriptable notification;

    private void Update()
    {
        transform.Rotate(0.0f, 360.0f * Time.deltaTime, 0.0f);
    }
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
