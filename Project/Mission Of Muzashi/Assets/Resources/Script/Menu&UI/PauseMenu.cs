using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private BGMusic _bgMusic;
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
                
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().PlayMusic();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().StopMusic();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().EndMusic();
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    { 
        Debug.Log("Quit Game...");
        Application.Quit();
    }
}
