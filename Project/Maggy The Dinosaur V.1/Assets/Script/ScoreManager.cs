using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]public Text scoreText;
    [SerializeField]public Text hiScoreText;

    [SerializeField] public float scoreCount;
    [SerializeField] public float hiScoreCount;

    [SerializeField] public float pointPerSecond;

    [SerializeField] public bool scoreIncreasing;
    [SerializeField] public bool shouldDouble;
    [SerializeField] public Text HiScoreAlert;
    
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("HighScore"))
        {
            hiScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointPerSecond * Time.deltaTime;
        }
        
        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);
            StartCoroutine(ShowMessage("New High Score!!!", 2));
            
        }
        
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        hiScoreText.text = "High Score: " + Mathf.Round(hiScoreCount);
        
        
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += shouldDouble ? pointsToAdd * 2 : pointsToAdd;
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        HiScoreAlert.text = message;
        HiScoreAlert.enabled = true;
        yield break;
    }
}
