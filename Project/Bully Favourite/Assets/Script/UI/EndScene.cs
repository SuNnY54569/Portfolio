using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField] private Text totalStar;
    [SerializeField] private Text highestStar;
    private void Update()
    {
        // Show the total number of stars obtained on play through
        int totalStars = LevelManager.instance.playThroughStars[0] + LevelManager.instance.playThroughStars[1] + LevelManager.instance.playThroughStars[2];
        totalStar.text = $"{totalStars}/9";
        // Show best Score
        int highestStars;
        if (PlayerPrefs.HasKey("HighestStars"))
        {
            highestStars = PlayerPrefs.GetInt("HighestStars");
        }
        else
        {
            highestStars = LevelManager.instance.highestStars[0] + LevelManager.instance.highestStars[1] + LevelManager.instance.highestStars[2];
        }

        if (totalStars > highestStars)
        {
            PlayerPrefs.SetInt("HighestStars", totalStars);
        }
        
        highestStar.text = $"Best Score : {highestStars}/9";
    }

    public void RestartGame()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level 1");
    }

    public void ToStart()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Menu");
    }
    
    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void LevelSelect()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level Select");
    }
}
