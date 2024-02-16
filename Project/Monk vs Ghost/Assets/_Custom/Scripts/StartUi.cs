using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUi : MonoBehaviour
{
    public void StartGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.StartGame);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClick);
        Application.Quit();
    }
}
