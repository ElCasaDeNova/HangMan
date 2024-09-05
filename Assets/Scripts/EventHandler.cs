using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{

    [SerializeField]
    private List<string> listOfWords;
    private List<string> currentlistOfWords;

    private string wordToGuess;

    private StringBuilder wordToShow;

    [SerializeField]
    private float maxLifePoint;
    private float currentLifePoint;

    [SerializeField]
    private TextMeshProUGUI wordDisplayText;

    [SerializeField]
    private TextMeshProUGUI currentLifePointText;

    [SerializeField]
    private float characterSpacing = 10f;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private float waitWinTime = 0.2f;
    [SerializeField]
    private float waitLoseTime = 1f;

    // Random Number to Select a Word from the List
    private int randomPick;
    private int nbRoundWon = 0;
    private int nbRoundToWin;

    // Start is called before the first frame update
    void Start()
    {
        // Adapt Spacing
        if (wordDisplayText != null)
        {
            wordDisplayText.characterSpacing = characterSpacing;
        }

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
            StartCoroutine(WinCoroutine());
        }
    }

    private void BadAnswer(char letter)
    {
        Debug.Log("Wrong Answer");
        currentLifePoint--;

        if (currentLifePoint <= 0)
        {
            StartCoroutine(LoseCoroutine());
        }
        else
        {
            Debug.Log("You have lost a Life Point. You still have " + currentLifePoint);
            UpdateScreen(wordToShow.ToString());
        }
    }

    private void SetRound()
    {
        // Set Current Life Poitns
        currentLifePoint = maxLifePoint;

        // Select a Word to Guess from List
        randomPick = Random.Range(0, currentlistOfWords.Count);
        wordToGuess = currentlistOfWords[randomPick];

        // Remove Word to Guess from List
        currentlistOfWords.RemoveAt(randomPick);

        // Generate Word to Show
        wordToShow = new StringBuilder(new string('_', wordToGuess.Length));

        Debug.Log("the word to guess is " + wordToGuess);

        UpdateScreen(wordToShow.ToString());
        ActivateButtons();
    }

    private IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(waitWinTime); // in seconds
        nbRoundWon++;
        Debug.Log("Round " + nbRoundWon + " on " + nbRoundToWin);
        SetRound();
    }

    private IEnumerator LoseCoroutine()
    {
        // Display Game Over
        UpdateScreen("Game Over");
        DisableButtons();
        yield return new WaitForSeconds(waitLoseTime); // in seconds

        currentlistOfWords = listOfWords;
        nbRoundWon = 0;
        SetRound();
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
        if (currentLifePointText != null)
        {
            currentLifePointText.text = currentLifePoint.ToString();
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

}
