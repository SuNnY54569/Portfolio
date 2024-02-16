using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public static StartSceneUI instance;
    
    [SerializeField] private GameObject normalPanel;
    [SerializeField] private GameObject clearAllPanel;

    public bool onSetting;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (LevelManager.instance.clearAll == true)
        {
            normalPanel.SetActive(false);
            clearAllPanel.SetActive(true);
        }
        else 
        {
            normalPanel.SetActive(true);
            clearAllPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level 1");
        
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void LevelSelect()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level Select");
    }
    
    public void PauseGame()
    {
        if (!onSetting)
        {
            onSetting = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
            SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
        }
        else
        {
            onSetting = false;
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
            SceneManager.UnloadSceneAsync("Setting");
        }
    }

    public void ResetHighScore()
    {
        LevelManager.instance.ResetHighestStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        PlayerPrefs.SetInt("HighestStars",0);
    }

    public void LoadCredit()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Credit");
    }
}
