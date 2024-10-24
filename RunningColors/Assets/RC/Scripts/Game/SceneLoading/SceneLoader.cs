using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scenes loading and unloading
/// </summary>
public static class SceneLoader
{
    public enum SceneName
    {
        //PersistentGameplay = 0,
        Loading = 0,
        MainMenu = 1,
        Level1 = 2,
        Level2 = 4,
        Level3 = 3,
        Level4 = 5
    }

    private static UnityAction OnSceneLoaderCallback;

    private static int _currentSceneIndex = 0;
    public static bool HasNextScene => SceneManager.sceneCountInBuildSettings > _currentSceneIndex + 1;

    /// <summary> This function loads the scene passed as SceneName enum </summary>
    public static void LoadScene(SceneName sceneToLoad)
    {
        OnSceneLoaderCallback = () => {
            SceneManager.LoadSceneAsync((int)sceneToLoad);
            
            _currentSceneIndex = (int)sceneToLoad;
            Debug.Log("Current Scene Index: " + _currentSceneIndex);
        };

        SceneManager.LoadScene(SceneName.Loading.ToString());
    }

    public static int GetCurrentSceneIndex()
    {
        return _currentSceneIndex;
    }

    public static void SceneLoaderCallback()
    {
        // Triggered after the first update which lets the screen refresh
        // Execute the scene loader callback action which will load the target scene
        OnSceneLoaderCallback?.Invoke();
        OnSceneLoaderCallback = null;
    }
}
