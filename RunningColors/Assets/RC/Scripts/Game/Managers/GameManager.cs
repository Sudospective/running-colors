using UnityEngine;

public class GameManager : MonoBehaviour
{

    static private GameManager instance;

    public GameObject player { get; private set; }
    public Camera mainCamera;
    public GameObject paintGlob;
    [Tooltip("The size of a glob of paint (move to paintbrush later)")]
    [Range(4, 64)] public int globSize;
    [Tooltip("The resolution of the paintable surfaces")]
    [Range(0, 3)] public int paintResolution;
    [Tooltip("The multiplier of the speed paint")]
    [Range(2.0f, 5.0f)] public float speedPaintMult;
    [Tooltip("The multiplier of the jump paint")]
    [Range(2.0f, 5.0f)] public float jumpPaintMult;

    [Tooltip("This is the max amount of paint")]
    [Range(10, 200)] public int paintMax;

    public float paintCur {  get; set; }

    public static GameManager GetInstance() { return instance; }

    private Controller playerController;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;

        if(mainCamera == null)
        {
            Debug.Log("Main Camera not found.");
        }
        
        paintCur = paintMax;
    }

    public void SetPlayer(GameObject newPlayer)
    {
        if(newPlayer != null)
        {
            player = newPlayer;
            playerController = player.GetComponent<Controller>();

            if (playerController == null)
            {
                Debug.Log("Controller not found on player");
            }
            else
            {
                Debug.Log("Player is assigned to GameManager");
            }
        }
        
    }

}
