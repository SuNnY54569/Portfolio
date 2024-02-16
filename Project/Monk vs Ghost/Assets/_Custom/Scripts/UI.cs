using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
   
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            gameUI.SetActive(true);
        }

        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            gameUI.SetActive(false);
        }
        
    }

    public void PauseGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClick); 
        pauseButton.SetActive(false);
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClick); 
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClick); 
        SceneManager.LoadScene(0);
        
    }
    
    public void Retry()
    {
        Time.timeScale = 1f;
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClick); 
        SceneManager.LoadScene(1);
    }
}
