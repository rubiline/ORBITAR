using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTextBox
    : MonoBehaviour
{
    private int index;
    private string actualText = "";
    public string targetText;

    public TextMeshPro logTextBox;

    private Coroutine text;


    public void SetText(string tgt)
    {
        index = 0;
        actualText = "";
        targetText = tgt;
        ReproduceText();
    }

    public void ClearText()
    {
        StopCoroutine(text);
        actualText = "";
        logTextBox.text = "";
    }


    private void ReproduceText()
    {
        //if not readied all letters
        if (index < targetText.Length)
        {
            //get one letter
            char letter = targetText[index];

            //Actualize on screen
            logTextBox.text = Write(letter);

            //set to go to the next
            index += 1;
            text = StartCoroutine(PauseBetweenChars(letter));
        }
    }

    private string Write(char letter)
    {
        actualText += letter;
        return actualText;
    }

    private IEnumerator PauseBetweenChars(char letter)
    {
        switch (letter)
        {
            case '.':
                yield return new WaitForSeconds(0.2f);
                ReproduceText();
                yield break;
            case ',':
                yield return new WaitForSeconds(0.2f);
                ReproduceText();
                yield break;
            case ' ':
                yield return new WaitForSeconds(0.1f);
                ReproduceText();
                yield break;
            default:
                yield return new WaitForSeconds(0.05f);
                ReproduceText();
                yield break;
        }
    }
}
