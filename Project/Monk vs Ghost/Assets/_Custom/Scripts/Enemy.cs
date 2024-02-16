using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int damageAmount = 10;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform ghostModel;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ghostModel = transform.Find("GhostModel");
        playerHealth = FindObjectOfType<PlayerHealth>();
        
        ghostModel.LookAt(player.transform);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Normalize the direction vector to maintain constant speed
            directionToPlayer.Normalize();

            // Move the enemy towards the player
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            SoundManager.instance.Play(SoundManager.SoundName.PlayerAttack);
            Destroy(gameObject);
            Debug.Log("killed");
            playerHealth.enemyKilled += 1;
        }
        if (other.CompareTag("Dome"))
        {
            SoundManager.instance.Play(SoundManager.SoundName.PlayerAttack); 
            Destroy(gameObject);
            Debug.Log("killed by dome");
            
        }
    }
}
