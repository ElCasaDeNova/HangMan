using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayHandler : MonoBehaviour
{

    [SerializeField]
    private List<string> listOfWords;
    private List<string> currentlistOfWords;

    private string wordToGuess;
    private List<int> spacePositions;

    private StringBuilder wordToShow;

    [SerializeField]
    private float maxChances;
    private float chancesLeft;

    [SerializeField]
    private float maxLifePoint;
    private float currentLifePoint;

    [SerializeField]
    private TextMeshProUGUI wordDisplayText;

    [SerializeField]
    private TextMeshProUGUI chancesLeftText;

    [SerializeField]
    private float characterSpacing = 10f;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private float waitWinTime = 0.2f;
    [SerializeField]
    private float waitLoseTime = 1f;

    [SerializeField]
    HealthBar HealthBar;

    [SerializeField]
    private Animator animator;

    // Random Number to Select a Word from the List
    private int randomPick;
    private int nbRoundWon = 0;
    private int nbRoundToWin;

    // For Animator
    [SerializeField] private Transform player;
    [SerializeField] private GameObject winRoundPrefab;
    [SerializeField] private GameObject loseRoundPrefab;
    [SerializeField] private GameObject loseGamePrefab;
    [SerializeField] private float spawnDistanceX = 6f;

    // For Sound
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip goodAnswerSound;
    [SerializeField]
    private AudioClip winRoundSound;
    [SerializeField]
    private AudioClip badAnswerSound;
    [SerializeField]
    private AudioClip loseRoundSound;

    // For Settings
    [SerializeField]
    private SettingsHandler settingsHandler;

    // For Lost game
    [SerializeField]
    private GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Adapt Spacing
        if (wordDisplayText != null)
        {
            wordDisplayText.characterSpacing = characterSpacing;
        }

        currentLifePoint = maxLifePoint;
        currentlistOfWords = listOfWords;
        nbRoundToWin = currentlistOfWords.Count;
        SetRound();
    }

    // Function Called by the Buttons
    public void Guess(char letter)
    {
        if (wordToGuess.Contains(letter))
        {
            GoodAnswer(letter);
        }
        else
        {
            BadAnswer(letter);
        }
    }

    private void GoodAnswer(char letter)
    {
        Debug.Log("Good Answer");

        PlaySound(goodAnswerSound);

        // Get A list of each position of the letter in the word
        List<int> positions = FindAllIndexes(wordToGuess, letter);

        // Replace the _ of the Word Displayed with the Guessed letter
        foreach (int pos in positions)
        {
            wordToShow[pos] = letter;
        }

        UpdateScreen(wordToShow.ToString());

        if (wordToShow.Equals(wordToGuess))
        {
            // Verify Winning Condition
            if (currentlistOfWords.Count <= 0)
            {
                WinGameCoroutine();
            }
            else
            {
                WinRound();
            }
        }
    }

    private void BadAnswer(char letter)
    {
        Debug.Log("Wrong Answer");
        chancesLeft--;

        PlaySound(badAnswerSound);

        // If too many errors
        if (chancesLeft <= 0)
        {
            // Lose a Life
            currentLifePoint--;
            HealthBar.LoseLife();

            Debug.Log("You have lost a life point. You still have " + currentLifePoint);

            if (currentLifePoint <= 0)
            {
                // If No more Life Points then Round is lost
                LoseGame();
            }
            else
            {
                // Verify Winning Condition
                if (currentlistOfWords.Count <= 0)
                {
                    WinGameCoroutine();
                }
                else
                {
                    StartCoroutine(LoseRoundCoroutine());
                }
            }
        }
        else
        {
            Debug.Log("You have lost a chance. You still have " + chancesLeft);
            UpdateScreen(wordToShow.ToString());
        }
    }

    private void SetRound()
    {
        // Set number of Chances
        chancesLeft = maxChances;

        // Select a Word to Guess from List
        randomPick = Random.Range(0, currentlistOfWords.Count);
        wordToGuess = currentlistOfWords[randomPick];

        // Remove Word to Guess from List
        currentlistOfWords.RemoveAt(randomPick);

        // Generate Word to Show
        wordToShow = new StringBuilder(new string('_', wordToGuess.Length));
        AddSpace();

        Debug.Log("the word to guess is " + wordToGuess);

        StartCoroutine(ShowRoundAndThenWord());
    }

    private void WinGameCoroutine()
    {
        DisableButtons();
        SceneLoader.nextScene = "VictoryScene";
        SceneManager.LoadScene("LoadingScene");
    }

    private void WinRound()
    {
        DisableButtons();

        PlaySound(goodAnswerSound, 1.1f);

        // Generate Win Round Animation Interaction
        SpawnObject(winRoundPrefab, "WinRound", spawnDistanceX);

        SetRound();
    }

    private void LoseGame()
    {
        DisableButtons();

        // Generate Lose Game Animation Interaction
        SpawnObject(loseGamePrefab, "LoseGame", spawnDistanceX + 3);

        // Hide Game UI
        gameUI.SetActive(false);
    }

    private IEnumerator LoseRoundCoroutine()
    {
        DisableButtons();

        // Generate Lose Round Animation Interaction
        SpawnObject(loseRoundPrefab, "LoseRound", spawnDistanceX);

        // Display Warning Message
        UpdateScreen("You've lost a Life");
        yield return new WaitForSeconds(waitLoseTime); // in seconds

        // Display Nothing for a pause
        UpdateScreen("");
        yield return new WaitForSeconds(waitLoseTime); // in seconds

        SetRound();
    }

    private IEnumerator ShowRoundAndThenWord()
    {
        DisableButtons();
        nbRoundWon++;
        UpdateScreen("Round " + nbRoundWon + " on " + nbRoundToWin);

        // Wait for a moment so the player can see the round info
        yield return new WaitForSeconds(waitWinTime);

        // Then show the word to guess
        UpdateScreen(wordToShow.ToString());
        ActivateButtons();
    }


    // Show Space characters in the Word to Show
    private void AddSpace()
    {
        if (wordToGuess.Contains(' '))
        {
            spacePositions = FindAllIndexes(wordToGuess, ' ');
            // Replace the _ of the Word Displayed with the Guessed letter
            foreach (int pos in spacePositions)
            {
                wordToShow[pos] = ' ';
            }
        }
    }

    private void DisableButtons()
    {
        foreach (Button button in buttons.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }
    }

    private void ActivateButtons()
    {
        foreach (Button button in buttons.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
    }

    private void UpdateScreen(string wordToDisplay)
    {
        // Display the Word
        if (wordDisplayText != null)
        {
            wordDisplayText.text = wordToDisplay;
        }

        // Display the Life Points
        if (chancesLeftText != null)
        {
            chancesLeftText.text = chancesLeft.ToString();
        }
    }

    // Return all indexes in which the letter is found in the word
    static List<int> FindAllIndexes(string word, char letter)
    {
        List<int> indexes = new List<int>();

        for (int i = 0; i < word.Length; i++)
        {
            if (char.ToUpper(word[i]) == char.ToUpper(letter))
            {
                indexes.Add(i);
            }
        }

        return indexes;
    }

    void SpawnObject(GameObject prefab, string triggerName, float spawnX)
    {
        if (prefab != null && player != null)
        {
            Vector3 spawnPosition = new Vector3(player.position.x + spawnX, player.position.y, player.position.z);
            GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

            CollisionDetector branchCollisionDetector = spawnedObject.GetComponent<CollisionDetector>();
            if (branchCollisionDetector != null && animator != null)
            {
                branchCollisionDetector.animator = animator;
                branchCollisionDetector.triggerName = triggerName;
            }
        }
        else
        {
            Debug.LogError("Prefab or Player are not assigned");
        }
    }

    void PlaySound(AudioClip audioClip, float newPitch = 1)
    {
        audioSource.PlayOneShot(audioClip);

        // Make different variation of the sound
        float randomVariant = Random.Range(0.8f, 1.2f);
        audioSource.pitch = newPitch * randomVariant;
    }
}
