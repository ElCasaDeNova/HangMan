using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private GameObject gameOverUI;

    // Show Death Menu to player
    void LoseGame()
    {
        // Show Game Over Menu
        gameOverUI.SetActive(true);
    }

}
