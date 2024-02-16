using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codeText;
    private string codeTextValue = "";
    [SerializeField] private AudioClip interactSound;

    private void Update()
    {
        codeText.text = codeTextValue;
        if (codeTextValue == "4444")
        {
            EnterPassword.isPasswordCorrect = true;
        }

        if (codeTextValue.Length >= 4)
        {
            codeTextValue = "";
        }
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }

    public void PlaySound()
    {
        GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(interactSound);
    }
}
