using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] public int enemyKilled;
    [SerializeField] private GameObject healthText;
    [SerializeField] private GameObject enemyKilledText;
    [SerializeField] private GameObject timePanel;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private TextMeshProUGUI endEnemyKilled;
    [SerializeField] private TextMeshProUGUI timeSurvive;
    [SerializeField] private TimeManager timeManager;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject dieUI;
    [SerializeField] private GameObject bath;
    public bool isDead;

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        enemyKilled = 0;
        diePanel.SetActive(false);
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void Update()
    {
        if (!isDead && !timeManager.isGameOver)
        {
            healthText.GetComponent<TextMeshProUGUI>().text = "Health : " + currentHealth;
            enemyKilledText.GetComponent<TextMeshProUGUI>().text = "Enemy Killed : " + enemyKilled;
        }
        else
        {
            healthText.GetComponent<TextMeshProUGUI>().text = "Health : 0" ;
        }
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead && !timeManager.isGameOver)
        {
            currentHealth -= damageAmount;
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
            Debug.Log("enemyhit");
            SoundManager.instance.Play(SoundManager.SoundName.TakeDamage);
        }
    }

    private void Die()
    {
        isDead = true;
        timePanel.SetActive(false);
        diePanel.SetActive(true);
        pauseButton.SetActive(false);
        dieUI.SetActive(true);
        bath.SetActive(false);
        endEnemyKilled.text = "Enemy Killed : " + enemyKilled;
        timeSurvive.text = "You Survive " + timeManager.totalTimeSurvive + " Minute"; 
        SoundManager.instance.Play(SoundManager.SoundName.LoseGame);
        Debug.Log("Player died.");
    }
}
