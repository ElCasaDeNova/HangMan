using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Button guessButton;

    [SerializeField]
    private char letter;

    [SerializeField]
    private EventHandler eventHandler;

    void Start()
    {
        // Assure-toi que le bouton est assigné
        if (guessButton != null)
        {
            guessButton.onClick.AddListener(OnStartButtonClick);
        }

        void OnStartButtonClick()
        {
            eventHandler.Guess(letter);

            // TODO setActive false This Button
            // TODO setActive True NoneWorking button
        }
    }
}
