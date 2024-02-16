using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float totalTime = 60f;  // Total time for the countdown in seconds
    [SerializeField] public float currentTime;      // Current time left
    public bool isGameOver = false; // Flag to track if the game is over
    [SerializeField] float timeSurvive;
    public string totalTimeSurvive;
     
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI mainTimerText;// Reference to a UI text element to display the countdown
    [SerializeField] GameObject enemySpawner;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject timePanel;
    [SerializeField] private TextMeshProUGUI WinEnemyKilled;
    
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject endUI;
    [SerializeField] private GameObject bath;
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        currentTime = totalTime; // Initialize the current time to the total time
        UpdateTimerText();       // Update the UI text to show the initial time
    }

    void Update()
    {
        if (!isGameOver && !playerHealth.isDead)
        {
            // Update the current time based on Time.deltaTime
            currentTime -= Time.deltaTime;

            // Check if time is over
            if (currentTime <= 0f)
            {
                currentTime = 0f; // Ensure the time doesn't go below zero
                isGameOver = true;
                EndGame(); // Call a function to handle game over
            }

            // Update the UI text to show the remaining time
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        // Display the time in the format MM:SS
        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");

        // Update the UI text
        timerText.text = minutes + ":" + seconds;
        mainTimerText.text = minutes + ":" + seconds;

        timeSurvive = totalTime - currentTime;
        string surMinutes = Mathf.Floor(timeSurvive / 60).ToString("00");
        string surSeconds = (timeSurvive % 60).ToString("00");
        totalTimeSurvive = surMinutes + ":" + surSeconds;
    }

    public void EndGame()
    {
        timePanel.SetActive(false);
        winPanel.SetActive(true);
        pauseButton.SetActive(false);
        endUI.SetActive(true);
        bath.SetActive(false);
        WinEnemyKilled.text = "Enemy Killed : " + playerHealth.enemyKilled;
        Destroy(enemySpawner);
        isGameOver = true;
        SoundManager.instance.Play(SoundManager.SoundName.WinGame);
        Debug.Log("Game Over!");
    }
}
