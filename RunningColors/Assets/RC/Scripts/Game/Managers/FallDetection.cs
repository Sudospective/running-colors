using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public ScreenFader screenFader;
    public float fallThreshold = -5f;

    private void Update()
    {
        if(transform.position.y < fallThreshold)
        {
            screenFader.FadeToBlack();

        }
    }
}
