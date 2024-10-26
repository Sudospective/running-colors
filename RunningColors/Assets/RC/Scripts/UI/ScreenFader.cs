using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] LoseEventPublisherSO loseEventPublisher;

    public void FadeToBlack()
    {
        if (anim != null)
            anim.SetTrigger("Death");
    }

    public void ShowLoseScreen()
    {
        if (loseEventPublisher != null)
            loseEventPublisher.RaiseEvent();
    }
}
