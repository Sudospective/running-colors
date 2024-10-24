using UnityEngine;

public class NextLevel : MonoBehaviour
{
    int nextSceneIndex;
    public void OnNextLevel()
    {
        PauseMenuManager.Instance.StateUnpause();
        nextSceneIndex = SceneLoader.GetCurrentSceneIndex() + 1;
        Debug.Log("Next Scene Index: " + nextSceneIndex);
        SceneLoader.LoadScene((SceneLoader.SceneName)nextSceneIndex);
    }
}
