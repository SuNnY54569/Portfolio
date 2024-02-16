using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : MonoBehaviour
{
    [SerializeField] public int scoreToGive;

    [SerializeField] private ScoreManager theScoreManager;

    [SerializeField] private AudioSource meatSound;
    
    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        meatSound = GameObject.Find("EatMac").GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (meatSound.isPlaying)
            {
                meatSound.Stop();
            }
            
            meatSound.Play();
            theScoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
        }
    }
}
