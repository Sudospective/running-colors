using System.Collections;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public Transform playerTransform;
    public ScreenFader screenFader;
    public float fallThreshold = -5f;

    [SerializeField] LoseEventPublisherSO loseEventPublisher;

    private void Update()
    {
        if(playerTransform == null)
        {
            return;
        }
        
        if(playerTransform.position.y < fallThreshold)
        {
            screenFader.FadeToBlack();
        }

        if(playerTransform.position.y < (fallThreshold * 10))
        {

            StartCoroutine(ShowLoseScreen());
            
        }
    }

    private IEnumerator ShowLoseScreen()
    {
        screenFader.FadeFromBlack();
        yield return new WaitForSeconds(screenFader.fadeDuration);
        loseEventPublisher.RaiseEvent();
    }
}
