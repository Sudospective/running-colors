using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

class LoadingMonoBehaviour : MonoBehaviour { }
/// <summary>
/// This class manages the scenes loading and unloading
/// </summary>
public static class SceneLoader
{
    public enum SceneName
    {
        PersistentGameplay = 0,
        Loading = 1,
        MainMenu = 2,
        Level1 = 3,
        Level2 = 4,
        Level3 = 5,
        Level4 = 6
    }

    private static UnityAction OnSceneLoaderCallback;
    private static AsyncOperation _loadingAsyncOps;
    private static List<Scene> _scenesToUnload;
    private static int _currentSceneIndex = 0;
    public static bool HasNextScene => SceneManager.sceneCountInBuildSettings > _currentSceneIndex + 1;

    static SceneLoader()
    {
        _loadingAsyncOps = new AsyncOperation();
        _scenesToUnload = new List<Scene>();
    }

    /// <summary> This function loads the scene passed as SceneName enum </summary>
    public static void LoadScene(SceneName sceneToLoad)
    {
        if (!CheckLoadState(SceneName.PersistentGameplay))
            SceneManager.LoadSceneAsync(SceneName.PersistentGameplay.ToString());

        if (!CheckLoadState(sceneToLoad))
        {
            // Add all current open scenes to unload list
            AddScenesToUnload();

            // Set the scene loader callback action to load the target scene
            OnSceneLoaderCallback = () => {
                GameObject loadingGameObject = new GameObject("Loading Game Object");
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(sceneToLoad));
                _currentSceneIndex = (int)sceneToLoad;
                Debug.Log("Current Scene Index: " +  _currentSceneIndex);
            };

            if (sceneToLoad != SceneName.PersistentGameplay)
            {
                // Load the loading scene
                SceneManager.LoadSceneAsync(SceneName.Loading.ToString(), LoadSceneMode.Additive);
            }
            // Unload the scenes
            UnloadScenes();
        }
    }

    static IEnumerator LoadSceneAsync(SceneName sceneToLoad)
    {
        yield return null;

        _loadingAsyncOps = SceneManager.LoadSceneAsync((int)sceneToLoad, LoadSceneMode.Additive);

        while (!_loadingAsyncOps.isDone) 
        {
            yield return null;
            Debug.Log("Loading progress: " + _loadingAsyncOps.progress);
        }
        
        if (_loadingAsyncOps.isDone)
            SceneManager.UnloadSceneAsync(SceneName.Loading.ToString());
    }

    static void AddScenesToUnload()
    {
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != SceneName.PersistentGameplay.ToString())
                {
                    Debug.Log("Added scene to unload = " + scene.name);
                    //Add the scene to the list of the scenes to unload
                    if (_scenesToUnload != null)
                        _scenesToUnload.Add(scene);
                }
            }
        }
    }

    static void UnloadScenes()
    {
        if (_scenesToUnload != null)
        {
            for (int i = 0; i < _scenesToUnload.Count; ++i)
            {
                Debug.Log("Unloaded scene = " + _scenesToUnload[i].name);
                //Unload the scene asynchronously in the background
                SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
            }
            _scenesToUnload.Clear();
        }
    }

    static bool CheckLoadState(SceneName sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name == sceneName.ToString())
            {
                return true;
            }
        }
        return false;
    }
    public static float GetLoadingProgress()
    {
        if (_loadingAsyncOps != null)
        {
            return _loadingAsyncOps.progress;
        }
        return 1f;
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
