using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUps : MonoBehaviour
{

    [SerializeField] public bool doublePoints;
    [SerializeField] public bool safeMode;

    [SerializeField] public float powerUpLength;
    [SerializeField] private AudioSource meatSound;
    [SerializeField] private AudioSource powerUpSound;

    private PowerUpManager ThePowerUpManager;

    [SerializeField] public Sprite[] powerUpSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        ThePowerUpManager = FindObjectOfType<PowerUpManager>();
        meatSound = GameObject.Find("EatMac").GetComponent<AudioSource>();
        powerUpSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
    }

    private void Awake()
    {
        int powerUpSelector = Random.Range(0, 2);
        
        switch (powerUpSelector)
        {
            case 0: 
                doublePoints = true;
                break;
            case 1:
                safeMode = true;
                break;
        }

        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelector];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            ThePowerUpManager.ActivatePowerUp(doublePoints, safeMode, powerUpLength);
            meatSound.Play();
            powerUpSound.Play();
        }
        gameObject.SetActive(false);
    }
}
