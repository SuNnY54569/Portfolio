using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }
 
    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.UnPause();
    }
 
    public void StopMusic()
    {
        _audioSource.Pause();
    }

    public void StartMusic()
    {
        _audioSource.Play();
    }

    public void EndMusic()
    {
        _audioSource.Stop();
    }
}
