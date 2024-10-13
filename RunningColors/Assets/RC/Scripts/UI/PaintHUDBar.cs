using UnityEngine;
using UnityEngine.UI;

public class PaintHUDBar : MonoBehaviour
{
    [Tooltip("Image component displaying player HP")]
    [SerializeField] Image PaintFillImage;

    // Update is called once per frame
    void Update()
    {
        PaintFillImage.fillAmount = GameManager.GetInstance().PaintCur / GameManager.GetInstance().paintMax;
    }
}
