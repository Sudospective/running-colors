using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationToast : MonoBehaviour
{
    [SerializeField] TMP_Text notificationText;

    [SerializeField] Animator notificationAnim;

    NotificationScriptable notifScriptable;

    bool initialized;

    public void Initialize(NotificationScriptable notification)
    {
        notifScriptable = notification;
        initialized = true;
    }

    void Update()
    {
        if (initialized)
        {
            StartCoroutine(EnableNotification());
        }
    }

    IEnumerator EnableNotification()
    {
        notificationAnim.Play("NotificationFadeIn");
        notificationText.text = notifScriptable.notificationMessage;

        if (notifScriptable.disableAfterTimer)
        {
            yield return new WaitForSeconds(notifScriptable.disableTimer);
            RemoveNotification();
        }
    }

    void RemoveNotification()
    {
        notificationAnim.Play("NotificationFadeOut");
        initialized = false;
    }
}
