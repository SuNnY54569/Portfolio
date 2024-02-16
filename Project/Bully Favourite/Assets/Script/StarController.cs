using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    
    public static StarController instance;
    private void Start()
    {
        instance = this;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(AudioManager.instance.starSound);
            if (Launcher.instance.isWin == false)
            {
                Launcher.instance.starGet++;
            }
            UI.instance.UpdateText();
        }
    }
}
