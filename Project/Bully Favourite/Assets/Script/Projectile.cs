using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public static Projectile instance;
    
    private Rigidbody2D rb;
    private Collider2D ballCol;
    
    public Sprite afterHit;

    private void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        ballCol = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //rotate ball same as projectile
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject,0.2f);
            Launcher.instance.ballThrown = 0;
        }//Hit ground
        
        if (other.gameObject.CompareTag("Receiver"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().sprite = afterHit;
            AudioManager.instance.PlaySFX(AudioManager.instance.bonk);
            LevelManager.instance.UpdateHighestStarsForLevel(SceneManager.GetActiveScene().buildIndex, Launcher.instance.starGet);
            LevelManager.instance.UpdatePlayThroughStarsForLevel(SceneManager.GetActiveScene().buildIndex, Launcher.instance.starGet);
            Launcher.instance.ballThrown = 0;
            Launcher.instance.isWin = true;
            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.cheeringSound);
                LevelManager.instance.clearAll = true;
            }

            UI.instance.Win();
            Destroy(gameObject);
        }//Hit Receiver

        if (other.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
            Launcher.instance.ballThrown = 0;
        }//Hit Border
    }
}
