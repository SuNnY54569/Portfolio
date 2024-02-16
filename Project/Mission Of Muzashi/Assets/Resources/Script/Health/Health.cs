using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private DeadMenu deadMenu;
    private LevelBlock _levelBlock;
    
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")] 
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Component")] 
    [SerializeField] private Behaviour[] components;

    [Header("Sound")] 
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        deadMenu = FindObjectOfType<DeadMenu>();
        
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(hurtSound);
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("Grounded", true);
                anim.SetTrigger("Die");

                dead = true;
                GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(deathSound);
            }

            if (GameObject.Find("Player").GetComponent<Health>().dead == true)
            {
                deadMenu.GameOver();
            }
            
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(iFramesDuration);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
