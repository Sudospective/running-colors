using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] LoseEventPublisherSO loseEventPublisher;

    public static bool isFading;

    public void FadeToBlack()
    {
        if (anim != null)
        {
            anim.SetTrigger("Death");
            isFading = true;
        }
    }

    public void ShowLoseScreen()
    {
        if (loseEventPublisher != null)
            loseEventPublisher.RaiseEvent();
    }
}
