using UnityEngine;

/// <summary>
/// This class goes on a trigger which, when entered, loads the Level Complete scene
/// </summary>
public class LevelComplete : MonoBehaviour
{
    [Header("Loading Settings")]

    [Tooltip("Include the Level Complete scene")]
    [SerializeField] GameSceneSO[] _scenesToLoad = default;

    [Tooltip("This will toggle the loading screen")]
    [SerializeField] bool _showLoadScreen = default;

    [Header("Publisher")]

    [SerializeField] LoadEventPublisherSO _levelCompletePublisher = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _levelCompletePublisher.RaiseEvent(_scenesToLoad, _showLoadScreen);
        }
    }
}
