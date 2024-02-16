using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    [SerializeField] private GameObject skipButton;
    [SerializeField] private AudioClip interactSound;

    private void Awake()
    {
        skipButton.SetActive(false);
    }

    public void ButtonAppear()
    {
        skipButton.SetActive(true);
    }

    public void Skip()
    {
        SceneManager.LoadScene(2);
    }

    public void EndStory()
    {
        SceneManager.LoadScene(2);
    }

    public void InteractSound()
    {
        GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(interactSound);
    }
}
