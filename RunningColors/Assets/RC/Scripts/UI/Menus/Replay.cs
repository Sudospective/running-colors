using UnityEngine;

public class Replay : MonoBehaviour
{
    int currentSceneIndex;

    public void OnReplay()
    {
        PauseMenuManager.Instance.StateUnpause();
        currentSceneIndex = SceneLoader.GetCurrentSceneIndex();
        Debug.Log("Current Scene Index: " + currentSceneIndex);
        SceneLoader.LoadScene((SceneLoader.SceneName)currentSceneIndex);
    }
}
