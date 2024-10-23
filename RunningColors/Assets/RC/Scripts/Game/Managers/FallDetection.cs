using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public Transform playerTransform;
    public ScreenFader screenFader;
    public float fallThreshold = -5f;

    public GameObject loseScreen;

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
        loseScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
