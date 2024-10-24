using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private void OnEnable()
    {
        PauseMenuManager.SetPlayMode(false);
    }

    private void OnDisable()
    {
        PauseMenuManager.SetPlayMode(true);
    }
}
