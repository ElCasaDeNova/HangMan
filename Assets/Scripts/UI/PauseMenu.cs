using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject GameMenu;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;
    private float randomVariant;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);  // Hide the pause menu initially
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;  // Stop the game time (pause the game)
        pauseMenu.SetActive(true);  // Show the pause menu
        isPaused = true;

        PlaySound();
        GameMenu.SetActive(false);  // Activate the game menu UI
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Resume the game time
        pauseMenu.SetActive(false);  // Hide the pause menu
        isPaused = false;

        PlaySound();

        // if Settings are Opened
        if (settingsPanel.activeSelf)
        {
            OpenSettings(); // Close Settings
        }

        GameMenu.SetActive(true);  // Reactivate the game menu UI
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        PlaySound();
        Application.Quit();
    }

    public void OpenSettings()
    {
        Debug.Log("Settings Opened!");
        PlaySound();
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    private void PlaySound()
    {
        // Give variety to sound
        randomVariant = Random.Range(0.5f, 2f);
        audioSource.pitch = randomVariant;

        Debug.Log("randomVariant is " + randomVariant);
        Debug.Log("audioSource pitch is " + audioSource.pitch);

        audioSource.PlayOneShot(audioClip);

        // Set to default
        audioSource.pitch = 1;
    }
}
