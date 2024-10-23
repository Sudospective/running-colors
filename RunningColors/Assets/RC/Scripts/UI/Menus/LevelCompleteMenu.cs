using UnityEngine;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateWinScreen();
        }
    }

    void ActivateWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
