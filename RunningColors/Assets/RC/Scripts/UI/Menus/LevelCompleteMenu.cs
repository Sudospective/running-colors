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
        
        PauseMenuManager.SetPlayMode(true);
    }

    void OpenLevelCompleteMenu()
    {
        PauseMenuManager.SetPlayMode(false);
        PauseMenuManager.Instance.StatePause();
        UICanvas.SetActive(PauseMenuManager.GetPauseState());
        levelCompletePanel.SetActive(PauseMenuManager.GetPauseState());

        if (!SceneLoader.HasNextScene)
            nextLevelButton.SetActive(false);
    }
}
