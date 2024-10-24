using UnityEngine;

/// <summary>
/// This class is a used for scene loading events.
/// </summary>
[CreateAssetMenu(menuName = "Events/Load Event Publisher")]
public class LoadEventPublisherSO : ScriptableObject
{
    public void RaiseEvent(SceneLoader.SceneName sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }
}
