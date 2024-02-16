using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    
    public AudioClip background;
    public AudioClip throwBall;
    public AudioClip bonk;
    public AudioClip starSound;
    public AudioClip buttonPress;
    public AudioClip cheeringSound;
    public AudioClip failSound;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Disable the extra AudioListener component
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
        if (audioListeners.Length > 1)
        {
            Destroy(audioListeners[1].gameObject);
        }
        
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
