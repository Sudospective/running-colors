using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;

    [Tooltip("This the acive menu")]
    public GameObject menuActive;

    [Tooltip("Root canvas used to toggle Menu activation")]
    [SerializeField] GameObject menuRoot;

    [Tooltip("Panel used to toggle Pause Menu activation")]
    [SerializeField] GameObject menuPause;

    [Tooltip("Panel used to toggle Options Menu activation")]
    [SerializeField] GameObject menuOptions;

    [Tooltip("Panel used to toggle Settings Menu activation")]
    [SerializeField] GameObject menuSettings;

    [Tooltip("Panel used to toggle Controls Menu activation")]
    [SerializeField] GameObject menuControls;

    Stack<GameObject> openedMenus = new Stack<GameObject>();

    public bool isPaused { get; private set; }
    public bool isInPlayMode { get; set; } = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isInPlayMode)
        {
            if (menuActive == null)
            {
                StatePause();

                if (menuRoot != null && menuPause != null)
                {
                    menuRoot.SetActive(isPaused);
                    menuActive = menuPause;
                    menuActive.SetActive(isPaused);
                    openedMenus.Push(menuPause);
                }
            }
            else
                StateUnpause();
        }
    }

    public void StatePause()
    {
        isPaused = !isPaused;

        Time.timeScale = 0;
        if (Timer.Instance != null)
            Timer.Instance.StopTimer();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StateUnpause()
    {
        isPaused = !isPaused;

        Time.timeScale = 1;
        if (Timer.Instance != null)
            Timer.Instance.StartTimer();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (menuActive != null && menuRoot != null)
        {
            menuRoot.SetActive(isPaused);
            menuActive.SetActive(isPaused);
        }
        menuActive = null;
    }

    public void OnOptionsClick()
    {
        if (menuOptions != null)
        {
            menuActive.SetActive(false);
            menuActive = menuOptions;
            menuActive.SetActive(isPaused);
            openedMenus.Push(menuOptions);
        }
    }

    public void OnSettingsClick()
    {
        if (menuSettings != null)
        {
            menuActive.SetActive(false);
            menuActive = menuSettings;
            menuActive.SetActive(isPaused);
            openedMenus.Push(menuSettings);
        }
    }

    public void OnControlsClick()
    {
        if (menuControls != null)
        {
            menuActive.SetActive(false);
            menuActive = menuControls;
            menuActive.SetActive(isPaused);
            openedMenus.Push(menuControls);
        }
    }

    public void OnBackClick()
    {
        openedMenus.Pop();
        menuActive.SetActive(false);

        if (openedMenus.Count > 0)
        {
            menuActive = openedMenus.Peek();
            menuActive.SetActive(isPaused);
        }
        else
            StateUnpause();
    }
}
