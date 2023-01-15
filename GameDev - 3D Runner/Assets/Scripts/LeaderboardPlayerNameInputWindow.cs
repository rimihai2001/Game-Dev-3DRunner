using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LeaderboardPlayerNameInputWindow : MonoBehaviour
{
    // Variable used to make sure this class only has one instance
    private static LeaderboardPlayerNameInputWindow instance;
    // Variable used for the action of subbmiting the name in the NameInput object
    private Action<string> onNameSubmitted;
    // Variable used to link the script to the NameText object
    private Text nameText;
    // Variable used to link the script to the YearReachedText object
    private Text yearReachedText;
    // Variable used to link the script to the PlayerNameField object
    private InputField playerNameField;
    
    void Awake()
    {
        // only one instance
        instance = this;
        // retrieve the text input of the object
        yearReachedText = transform.Find("YearReached").GetComponent<Text>();
        // retrieve the text input of the object
        playerNameField = transform.Find("NameInputText").GetComponent<InputField>();
        // validates the input of the PlayerNameField to be sure it s a string, character or integer
        playerNameField.onValidateInput = (string text, int charIndex, char addedChar) => addedChar.ToString().ToUpper()[0];
        // disables the window
        gameObject.SetActive(false);

    }
    /*
     * GET INPUT FUNCTION
     * used to retrieve the score and the name that is submitted in the InputTextField
     */
    public static void GetInput(int yearReached, Action<string> onNameSubmitted)
    {
        instance.gameObject.SetActive(true);
        instance.yearReachedText.text = " " + yearReached;
        instance.onNameSubmitted = onNameSubmitted;
        instance.playerNameField.text = "";
        instance.playerNameField.Select();
        instance.playerNameField.ActivateInputField();

    }

    /*
     * CHECK INPUT FUNCTION
     * used to check if the input has reached the max length or if the RETURN button was pressed
     * in order to submit the name introduced
     */
    public void checkInput()
    {
        if (playerNameField.text.Length <= 5 && Input.GetKeyDown(KeyCode.Return))
        {
            onNameSubmitted(playerNameField.text);
            gameObject.SetActive(false);
        }
    }
}
