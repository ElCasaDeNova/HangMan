using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingBar;

    private void Start()
    {
        StartCoroutine(LoadGameAsync("WalkingScene"));
    }

    private IEnumerator LoadGameAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;

            if (operation.progress >= 0.9f)
            {
                loadingBar.fillAmount = 1f;
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
