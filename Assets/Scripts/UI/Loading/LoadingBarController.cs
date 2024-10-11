using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingBarController : MonoBehaviour
{
    public Image loadingBar;
    public TMP_Text loadingText;

    public void UpdateLoadingBar(float progress)
    {
        loadingBar.fillAmount = progress;
        loadingText.text = (progress * 100).ToString("F0") + "%";
    }
}
