using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PowerUpManager : MonoBehaviour
{
    
    public Text scoreText;
    [SerializeField] private bool doublePoints;
    [SerializeField] private bool safeMode;

    [SerializeField] private bool powerUpActive;

    [SerializeField] private float powerUpLengthCounter;

    [SerializeField] private ScoreManager theScoreManager;
    [SerializeField] private PlatformGenerator thePlatformGenerator;
    [SerializeField] private PlayerController thePlayerController;
    private float normalPointsPerSecond;
    private float boneRate;

    private PlatformDestroyer[] boneList;
    
    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();

        normalPointsPerSecond = theScoreManager.pointPerSecond;
        boneRate = thePlatformGenerator.randomBoneThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;

            
            if (doublePoints)
            {
                theScoreManager.shouldDouble = true;
                scoreText.GetComponent<Text>().color = new Color(1f, 0.9f, 0f);
            }

            if (safeMode)
            {
                thePlatformGenerator.randomBoneThreshold = 0;
                scoreText.GetComponent<Text>().color = Color.white;

            }
            
            if (powerUpLengthCounter <= 0)
            {
                scoreText.GetComponent<Text>().color = Color.white;
                theScoreManager.pointPerSecond = normalPointsPerSecond;
                thePlatformGenerator.randomBoneThreshold = boneRate;
                theScoreManager.shouldDouble = false;
                
                powerUpActive = false;
                safeMode = false;
                doublePoints = false;
            }
        }
    }

    public void ActivatePowerUp(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerUpLengthCounter = time;
        powerUpActive = true;
        if (safeMode)
        {
            boneList = FindObjectsOfType<PlatformDestroyer>();
                    for (int i = 0; i < boneList.Length; i++)
                    {
                        if (boneList[i].gameObject.name.Contains("Bone")) 
                        {
                            boneList[i].gameObject.SetActive(false);
                        }
                        
                    }
        }
    }
    
    public void DeactivatePowerUp() 
    {
        powerUpLengthCounter = 0;
    }
}
