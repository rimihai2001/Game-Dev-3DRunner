using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LeaderboardPlayerNameInputWindow : MonoBehaviour
{
    private static LeaderboardPlayerNameInputWindow instance;

    private Action<string> onNameSubmitted;
    private Text nameText;
    private Text yearReachedText;
    private InputField playerNameField;
    
    void Awake()
    {
        instance = this;

        nameText = transform.Find("NameInputText").GetComponent<Text>();
        yearReachedText = transform.Find("YearReached").GetComponent<Text>();
        playerNameField = transform.Find("NameInputText").GetComponent<InputField>();
        playerNameField.onValidateInput = (string text, int charIndex, char addedChar) => addedChar.ToString().ToUpper()[0];
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(nameText.text.Length >= 3) {
            onNameSubmitted(nameText.text);
            gameObject.SetActive(false);
        }
    }
     
    public static void Show(int yearReached, Action<string> onNameSubmitted)
    {
        instance.gameObject.SetActive(true);
        instance.yearReachedText.text = " " + yearReached;
        instance.onNameSubmitted = onNameSubmitted;
        instance.playerNameField.text = "";
        instance.playerNameField.Select();
        instance.playerNameField.ActivateInputField();
    }
}
