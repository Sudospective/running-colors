using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainEvent : MonoBehaviour
{
    public NotificationSO notifObject;
    public NotificationToastEventPublisher notifPublisher;

    private void OnTriggerEnter(Collider other)
    {
        if (notifObject != null)
        {
            notifPublisher.RaiseEvent(notifObject);
            Destroy(gameObject);
        }
    }
}
