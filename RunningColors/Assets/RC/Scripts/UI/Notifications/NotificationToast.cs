using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationToast : MonoBehaviour
{
    [Tooltip("Text content that will display the notification text")]
    [SerializeField] TMP_Text notificationText;

    [SerializeField] Animator notificationAnim;

    NotificationSO notifScriptable;

    bool isInitialized;
    Coroutine notifCoroutine;

    private void Update()
    {
        if (isInitialized)
        {
            // Traverse a list of all the objects with this script attached
            for (int i = 0; i < Object.FindObjectsOfType<NotificationToast>().Length; i++)
            {
                // If a new notification is initialized
                if (Object.FindObjectsOfType<NotificationToast>()[i] != this)
                {
                    if (notifCoroutine != null)
                    {
                        // Remove this notification
                        Destroy(gameObject);
                    }
                }
            }
            notifCoroutine = StartCoroutine(EnableNotification());           
        }
    }

    public void Initialize(NotificationSO notification)
    {
        notifScriptable = notification;
        isInitialized = true;
    }

    IEnumerator EnableNotification()
    {
        if (notificationAnim != null && notificationText != null)
        {
            notificationAnim.Play("NotificationFadeIn");
            notificationText.text = notifScriptable.notificationMessage;

            if (notifScriptable.disableAfterTimer)
            {
                yield return new WaitForSeconds(notifScriptable.disableTimer);
                RemoveNotification();
            }
            else if (notifScriptable.removeByKey)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(notifScriptable.removeKey));
                RemoveNotification();
            }
        }
    }

    void RemoveNotification()
    {
        notificationAnim.Play("NotificationFadeOut");
        isInitialized = false;
    }
}
