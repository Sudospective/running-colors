using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Tooltip("This the acive menu")]
    public GameObject menuActive;

    [Tooltip("Panel used to toggle Main Menu activation")]
    [SerializeField] GameObject menuMain;

    [Tooltip("Panel used to toggle Credits activation")]
    [SerializeField] GameObject menuCredits;

    Stack<GameObject> openedMenus = new Stack<GameObject>();

    private void Start()
    {
        if (menuMain != null)
        {
            menuActive = menuMain;
            openedMenus.Push(menuMain);
        }
    }

    public void OnStartGame()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.Level1);
    }

    public void OnCredits()
    {
        if (menuCredits != null && menuActive != null)
        {
            menuActive.SetActive(false);
            menuActive = menuCredits;
            menuActive.SetActive(true);
            openedMenus.Push(menuCredits);
        }
    }

    public void OnBack()
    {
        if (menuActive != null && openedMenus != null)
        {
            openedMenus.Pop();
            menuActive.SetActive(false);

            if (openedMenus.Count > 0)
            {
                menuActive = openedMenus.Peek();
                menuActive.SetActive(true);
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
