using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Sprite star1, star2, star3;

    public Image level1, level2, level3;
    
    // Update is called once per frame
    void Update()
    {
        switch (LevelManager.instance.highestStars[0])
        {
            case 0:
                level1.gameObject.SetActive(false);
                break;
            case 1:
                level1.gameObject.SetActive(true);
                level1.sprite = star1;
                break;
            case 2:
                level1.gameObject.SetActive(true);
                level1.sprite = star2;
                break;
            case 3:
                level1.gameObject.SetActive(true);
                level1.sprite = star3;
                break;
        }//Level 1 Highest star
        
        switch (LevelManager.instance.highestStars[1])
        {
            case 0:
                level2.gameObject.SetActive(false);
                break;
            case 1:
                level2.gameObject.SetActive(true);
                level2.sprite = star1;
                break;
            case 2:
                level2.gameObject.SetActive(true);
                level2.sprite = star2;
                break;
            case 3:
                level2.gameObject.SetActive(true);
                level2.sprite = star3;
                break;
        }//Level 2 Highest star
        
        switch (LevelManager.instance.highestStars[2])
        {
            case 0:
                level3.gameObject.SetActive(false);
                break;
            case 1:
                level3.gameObject.SetActive(true);
                level3.sprite = star1;
                break;
            case 2:
                level3.gameObject.SetActive(true);
                level3.sprite = star2;
                break;
            case 3:
                level3.gameObject.SetActive(true);
                level3.sprite = star3;
                break;
        }//Level 3 Highest star
    }

    public void LoadLevel1()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level 1");
    }
    
    public void LoadLevel2()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level 2");
    }
    
    public void LoadLevel3()
    {
        LevelManager.instance.ResetPlayThroughStars();
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Level 3");
    }

    public void LoadMenu()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.LoadScene("Menu");
    }
}
