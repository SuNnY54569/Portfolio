using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField] public Transform platformGenerator;
    [SerializeField] private Vector3 platformStartPoint;

    [SerializeField] public PlayerController thePlayer;
    [SerializeField] private Vector3 playerStartPoint;

    [SerializeField] private PlatformDestroyer[] platformList;

    [SerializeField] private ScoreManager theScoreManager;
    [SerializeField] private DeathMenu theDeathScreen;

    [SerializeField] private PowerUpManager thePowerUpManager;

    public int bgRandom;
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        BgRandom();
    }
    
    public void RestartGame()
        {
            theScoreManager.HiScoreAlert.enabled = false;
            theScoreManager.scoreIncreasing = false;
            thePlayer.gameObject.SetActive(false);
            theDeathScreen.gameObject.SetActive(true);
            
        }
    public void Reset()
    {
        
        thePowerUpManager.DeactivatePowerUp();
        theDeathScreen.gameObject.SetActive(false);
        BgRandom();
        
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
        thePlayer.playerLife = thePlayer.playerDefaultLife;
        thePlayer.liveText.text = "Life: " + thePlayer.playerLife;

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
        theScoreManager.HiScoreAlert.enabled = false;
        
    }

    public void BgRandom()
    {
        bgRandom = Random.Range(0, 2);
        switch (bgRandom)
        {
            case 0:
                background1.SetActive(true);
                background2.SetActive(false);
                break;
            case 1:
                background1.SetActive(false);
                background2.SetActive(true);
                break;
        }
    }
    
}
