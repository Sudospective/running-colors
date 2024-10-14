using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void Resume()
    {
        PauseMenuManager.Instance.StateUnpause();
    }

    public void OptionsMenu()
    {
        PauseMenuManager.Instance.OnOptionsClick();
    }

    public void SettingsMenu()
    {
        PauseMenuManager.Instance.OnSettingsClick();
    }

    public void ControlsMenu()
    {
        PauseMenuManager.Instance.OnControlsClick();
    }

    public void Back()
    {
        PauseMenuManager.Instance.OnBackClick();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
