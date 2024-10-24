using UnityEngine;

public class NextLevel : MonoBehaviour
{
    int nextSceneIndex;
    public void OnNextLevel()
    {
        PauseMenuManager.Instance.StateUnpause();
        nextSceneIndex = SceneLoader.GetCurrentSceneIndex() + 1;
        SceneLoader.LoadScene((SceneLoader.SceneName)nextSceneIndex);
    }
}
