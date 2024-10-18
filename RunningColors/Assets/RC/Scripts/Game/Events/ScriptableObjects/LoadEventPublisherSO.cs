using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Events/Load Event Publisher")]
public class LoadEventPublisherSO : ScriptableObject
{
    public UnityAction<GameSceneSO[], bool> OnLoadingRequested;

    public void RaiseEvent(GameSceneSO[] scenesToLoad, bool showLoadingScreen)
    {
        // If there is a Scene Loader in the scene
        if (OnLoadingRequested != null)
        {
            OnLoadingRequested.Invoke(scenesToLoad, showLoadingScreen);
        }
        else
        {
            Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
                "Check why there is no SceneLoader already present, " +
                "and make sure it's listening for this Load Event Publisher.");
        }
    }
}
