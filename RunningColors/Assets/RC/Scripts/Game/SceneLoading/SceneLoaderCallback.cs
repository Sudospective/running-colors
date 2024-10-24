using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            SceneLoader.SceneLoaderCallback();
        }
    }
}
