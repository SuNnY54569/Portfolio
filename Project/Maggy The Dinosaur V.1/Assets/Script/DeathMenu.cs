using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuLevel;
    
    public virtual void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
    }

    public virtual void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }
}
