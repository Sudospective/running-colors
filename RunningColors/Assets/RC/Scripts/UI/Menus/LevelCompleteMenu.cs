using UnityEngine;

public class LevelCompleteMenu : MonoBehaviour
{
    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] GameObject nextLevelButton;

    [SerializeField] WinEventPublisherSO _winEventPublisher;

    private void OnEnable()
    {
        if (_winEventPublisher != null)
            _winEventPublisher.OnLevelComplete += OpenLevelCompleteMenu;
    }

    private void OnDisable()
    {
        if (_winEventPublisher != null)
            _winEventPublisher.OnLevelComplete -= OpenLevelCompleteMenu;
        
        PauseMenuManager.Instance.isInPlayMode = true;
    }

    void OpenLevelCompleteMenu()
    {
        PauseMenuManager.Instance.isInPlayMode = false;
        PauseMenuManager.Instance.StatePause();
        UICanvas.SetActive(PauseMenuManager.Instance.isPaused);
        levelCompletePanel.SetActive(PauseMenuManager.Instance.isPaused);

        if (!SceneLoader.HasNextScene)
            nextLevelButton.SetActive(false);
    }
}
