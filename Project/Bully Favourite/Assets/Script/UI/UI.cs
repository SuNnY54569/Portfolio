using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;
    
    public Text ballText;
    public Text starText;

    public GameObject youWin;
    public GameObject youLose;

    [SerializeField] private Image starImage;
    [SerializeField] private Sprite star0;
    [SerializeField] private Sprite star1;
    [SerializeField] private Sprite star2;
    [SerializeField] private Sprite star3;

    private void Start()
    {
        instance = this;
    }

    public void UpdateText()
    {
        ballText.text = $"Ball Remaining : {Launcher.instance.remainingBall}";
        starText.text = $"Star Get : {Launcher.instance.starGet}";
    }

    public void Win()
    {
        youWin.SetActive(true);
        switch (Launcher.instance.starGet)
        {
            case 0:
                starImage.sprite = star0;
                break;
            case 1:
                starImage.sprite = star1;
                break;
            case 2:
                starImage.sprite = star2;
                break;
            case 3:
                starImage.sprite = star3;
                break;
            
        }
    }//Win panel appear

    public void RestartLevel()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToStart()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Menu");
    }

    public void ToResult()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("End");
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Launcher.instance.isPaused = true;
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }
}
