using UnityEngine;

public class GameManager : MonoBehaviour
{

    static private GameManager instance;

    public GameObject player;
    public Camera mainCamera;
    public GameObject paintGlob;
    [Tooltip("The size of a glob of paint (move to paintbrush later)")]
    [Range(4, 64)] public int globSize;
    [Tooltip("The resolution of the paintable surfaces")]
    [Range(0, 3)] public int paintResolution;

    [Tooltip("This is the max amount of paint")]
    [Range(10, 200)] public int paintMax;

    public float PaintCur {  get; set; }

    public static GameManager GetInstance() { return instance; }

    private GameObject playerObject;
    private Controller playerController;

    void Awake()
    {
        instance = this;
        playerController = player.GetComponent<Controller>();
    }

    void Start()
    {
        PaintCur = paintMax;
    }
}
