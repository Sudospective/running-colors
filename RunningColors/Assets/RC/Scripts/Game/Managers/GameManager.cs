using UnityEngine;

public class GameManager : MonoBehaviour
{

    static private GameManager instance;

    public GameObject player;
    public Camera mainCamera;
    public GameObject paintGlob;

    public static GameManager GetInstance() { return instance; }

    private GameObject playerObject;
    private Controller playerController;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        playerController = player.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}