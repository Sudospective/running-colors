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

        PauseMenuManager.SetPlayMode(true);
    }

    void OpenLoseMenu()
    {
        PauseMenuManager.SetPlayMode(false);
        PauseMenuManager.Instance.StatePause();
        UICanvas.SetActive(PauseMenuManager.GetPauseState());
        losePanel.SetActive(PauseMenuManager.GetPauseState());
    }
}
