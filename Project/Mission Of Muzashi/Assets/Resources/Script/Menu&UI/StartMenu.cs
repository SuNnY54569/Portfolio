using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private BGMusic _bgMusic;
    [SerializeField] private AudioClip interactSound;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void StartSong()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().StartMusic();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void InteractSound()
    {
        GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(interactSound);
    }
}
