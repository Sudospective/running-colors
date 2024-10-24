using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.Level1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
