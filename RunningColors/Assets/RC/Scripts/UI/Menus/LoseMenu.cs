using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject losePanel;

    [SerializeField] LoseEventPublisherSO _loseEventPublisher;

    private void OnEnable()
    {
        if (_loseEventPublisher != null)
            _loseEventPublisher.OnLose += OpenLoseMenu;
    }

    private void OnDisable()
    {
        if (_loseEventPublisher != null)
            _loseEventPublisher.OnLose -= OpenLoseMenu;

        PauseMenuManager.Instance.isInPlayMode = true;
    }

    void OpenLoseMenu()
    {
        PauseMenuManager.Instance.isInPlayMode = false;
        PauseMenuManager.Instance.StatePause();
        UICanvas.SetActive(PauseMenuManager.Instance.isPaused);
        losePanel.SetActive(PauseMenuManager.Instance.isPaused);
    }
}
