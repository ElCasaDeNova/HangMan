using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EventHandler : MonoBehaviour
{

    [SerializeField]
    private List<string> listOfWord;

    private string wordToGuess;
    private StringBuilder wordToShow;


    [SerializeField]
    private float lifePoint;

    // Start is called before the first frame update
    void Start()
    {
        wordToGuess = listOfWord[Random.Range(0, listOfWord.Count)];
        Debug.Log("the word to guess is " + wordToGuess);
        wordToShow = new StringBuilder(new string('_', wordToGuess.Length));
        Debug.Log("the word to show is " + wordToShow);
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
        Debug.Log("The positions are "+ positions);
        Debug.Log("The letter is " + letter);

        // Replace the _ of the Word Displayed with the Guessed letter
        foreach (int pos in positions)
        {
            wordToShow[pos] = letter;
            Debug.Log("The Word in position  " + pos +" was "+ wordToShow[pos]+" and become "+letter);
        }
        Debug.Log("The Word is " + wordToShow);

        if (wordToShow.Equals(wordToGuess)) {
            Debug.Log("You win");
        }
    }

    private void BadAnswer(char letter)
    {
        Debug.Log("Wrong Answer");
        lifePoint--;
        Debug.Log("You have lost a Life Point. You still have " + lifePoint);
        if (lifePoint <= 0)
        {
            Debug.Log("Game Over");
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
