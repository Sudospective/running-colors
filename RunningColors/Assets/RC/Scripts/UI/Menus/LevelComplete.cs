using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] Canvas UICanvas;
    [SerializeField] GameObject levelCompletePanel;

    [SerializeField] WinEventPublisherSO _winEventPublisher;

    private void OnEnable()
    {
        if (_winEventPublisher != null)
            _winEventPublisher.OnLevelComplete += OpenLevelCompleteMenu;
    }

    void OpenLevelCompleteMenu()
    {
        PauseMenuManager.Instance.StatePause();
        levelCompletePanel.SetActive(true);
    }
}
