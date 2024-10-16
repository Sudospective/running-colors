using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu")]

    [SerializeField] GameObject mainMenu;

    [Header("Scenes to Load")]

    //[Tooltip("Name of the scene containing persistant objects")]
    //[SerializeField] string persistantGameplay = "PersistantGameplay";

    [Tooltip("Name of the first level to load")]
    [SerializeField] string levelScene = "Level1";

    [Header("Loading Bar")]

    [Tooltip("Parent object used to toggle the loading bar activation")]
    [SerializeField] GameObject loadingBarParent;

    [Tooltip("Image used to fill the loading bar")]
    [SerializeField] Image loadingBarFill;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    void Start()
    {
        loadingBarParent.SetActive(false);
    }

    public void StartGame()
    {
        HideMenu();
        ShowLoadingBar();

        //scenesToLoad.Add(SceneManager.LoadSceneAsync(persistantGameplay));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelScene));

        StartCoroutine(LoadingBarProgress());
    }

    void HideMenu()
    {
        mainMenu.SetActive(false);
    }

    void ShowLoadingBar()
    {
        loadingBarParent.SetActive(true);
    }

    IEnumerator LoadingBarProgress()
    {
        float loadProgress = 0f;

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                loadProgress += scenesToLoad[i].progress;
                loadingBarFill.fillAmount = loadProgress / scenesToLoad.Count;

                yield return null;
            }
        }
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
