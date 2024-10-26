using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public Transform playerTransform;
    public ScreenFader screenFader;
    public float fallThreshold = -5f;

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
    }
}
