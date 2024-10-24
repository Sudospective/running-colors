using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    Image _loadingBar;

    private void Awake()
    {
        _loadingBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        PauseMenuManager.Instance.isInPlayMode = false;
    }

    private void OnDisable()
    {
        PauseMenuManager.Instance.isInPlayMode = true;
    }

    private void Update()
    {
        if (_loadingBar != null)
            _loadingBar.fillAmount = SceneLoader.GetLoadingProgress() * Time.deltaTime;
    }
}
