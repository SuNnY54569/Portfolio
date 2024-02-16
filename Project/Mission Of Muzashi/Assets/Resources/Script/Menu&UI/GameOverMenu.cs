using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public string mainMenuLevel;

    public void RestartGame()
    {
        SceneManager.LoadScene("Lvl.1");
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("Start Menu");
    }

    [SerializeField] private GameObject gameOverScreen;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
