using UnityEngine;
using UnityEngine.UI;

public class PaintHUDBar : MonoBehaviour
{
    [Tooltip("Image component displaying player HP")]
    [SerializeField] Image PaintFillImage;

    void Update()
    {
        PaintFillImage.fillAmount = GameManager.GetInstance().paintCur / GameManager.GetInstance().paintMax;
    }
}
