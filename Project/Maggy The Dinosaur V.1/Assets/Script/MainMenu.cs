using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public string playGameLevel;
    [SerializeField] private GameObject ReHiscoreAlert;
    
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    private int bgRandom;
    public void Start()
    {
        bgRandom = Random.Range(0, 2);
        switch (bgRandom)
        {
            case 0:
                background1.SetActive(true);
                background2.SetActive(false);
                break;
            case 1:
                background1.SetActive(false);
                background2.SetActive(true);
                break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHiScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        StartCoroutine(Alert(1));
    }
    
    IEnumerator Alert (float delay) {
        ReHiscoreAlert.SetActive(true);
        yield return new WaitForSeconds(delay);
        ReHiscoreAlert.SetActive(false);
    }
        
}
