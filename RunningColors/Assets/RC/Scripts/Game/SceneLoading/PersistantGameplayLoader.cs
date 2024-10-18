using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script loads the Persistant Gameplay Scene, to allow to start the game from any gameplay Scene
/// </summary>
public class PersistantGameplayLoader : MonoBehaviour
{
#if UNITY_EDITOR
    public GameSceneSO persistantGameplayScene;

    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == persistantGameplayScene.sceneName)
            {
                return;
            }
        }
        SceneManager.LoadSceneAsync(persistantGameplayScene.sceneName, LoadSceneMode.Additive);
    }
#endif
}
