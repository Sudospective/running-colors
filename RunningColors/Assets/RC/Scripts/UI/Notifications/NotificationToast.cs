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

    private void Update()
    {
        if (isInitialized)
        {
            StartCoroutine(EnableNotification());           
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
            notificationAnim.SetTrigger("isToasted");
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
