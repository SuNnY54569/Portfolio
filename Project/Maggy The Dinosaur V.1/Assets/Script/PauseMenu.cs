using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : DeathMenu
{
    
    public GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public override void RestartGame()
    {
        ResumeGame();
        base.RestartGame();
    }
    
    public override void QuitToMain()
    {
        ResumeGame();
        base.QuitToMain();
    }

}
