using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;

    [Tooltip("Root canvas used to toggle Pause Menu activation")]
    [SerializeField] GameObject menuRoot;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        menuRoot.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuRoot.activeSelf)
            {
                StatePause();
                menuRoot.SetActive(IsPaused);
            }
            else
                StateUnpause();
        }
    }

    public void StatePause()
    {
        IsPaused = !IsPaused;

        Time.timeScale = 0;
        Timer.Instance.StopTimer();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StateUnpause()
    {
        IsPaused = !IsPaused;

        Time.timeScale = 1;
        Timer.Instance.StartTimer();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        menuRoot.SetActive(IsPaused);
    }
}
