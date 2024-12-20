using TMPro;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public ScreenFader screenFader;
    public TMP_Text paintUI;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        GameManager.GetInstance().SetPlayer(player);

        Controller playerController = player.GetComponent<Controller>();
        if (playerController != null && paintUI != null)
        {
            playerController.paintCurrent = paintUI;
        }


        PlayerCam playerCam = Camera.main.GetComponent<PlayerCam>();
        if (playerCam != null)
        {
            Transform orientation = player.transform.Find("Orientation");
            if (orientation != null)
            {
                playerCam.orientation = orientation;
            }

            MoveCamera moveCamera = Camera.main.GetComponentInParent<MoveCamera>();
            if (moveCamera != null)
            {
                Transform cameraPos = player.transform.Find("CameraPos");
                if (cameraPos != null)
                {
                    moveCamera.cameraPosition = cameraPos;
                }
            }
        }

        FallDetection fallDetector = FindObjectOfType<FallDetection>();
        if (fallDetector != null)
        {
            fallDetector.playerTransform = player.transform;
            fallDetector.screenFader = screenFader;
        }

        UserSettings.Instance.ApplySettingsToCamera();
    }
}
