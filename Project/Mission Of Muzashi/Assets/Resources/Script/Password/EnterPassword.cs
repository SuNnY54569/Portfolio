using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPassword : MonoBehaviour
{
    [SerializeField] private GameObject codePanel, panelBG;

    public static bool isPasswordCorrect = false;

    private void Start()
    {
        codePanel.SetActive(false);
        panelBG.SetActive(false);
    }

    private void Update()
    {
        if (isPasswordCorrect)
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().EndMusic();
            SceneManager.LoadScene(6);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player") && !isPasswordCorrect)
        {
            
            codePanel.SetActive(true);
            panelBG.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            codePanel.SetActive(false);
            panelBG.SetActive(false);
        }
    }
}
