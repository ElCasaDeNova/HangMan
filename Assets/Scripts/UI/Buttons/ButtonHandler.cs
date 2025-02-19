using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Button guessButton;

    [SerializeField]
    private char letter;

    [SerializeField]
    private GameplayHandler gameplayHandler;

    private TMP_Text buttonText;

    void Start()
    {
        buttonText = guessButton.GetComponentInChildren<TMP_Text>();

        if (guessButton != null)
        {
            guessButton.onClick.AddListener(OnStartButtonClick);
        }
    }

    void OnStartButtonClick()
    {
        // DisableButton
        guessButton.interactable = false;
        gameplayHandler.Guess(letter);
    }
}
