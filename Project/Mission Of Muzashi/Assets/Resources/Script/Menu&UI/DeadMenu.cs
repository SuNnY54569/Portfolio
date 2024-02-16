using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().StopMusic();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().PlayMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().StopMusic();
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
