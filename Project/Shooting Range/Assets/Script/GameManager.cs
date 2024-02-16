using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Use t$$anonymous$$s for initialization
    int enemiesLeft = 0;
    public bool killedAllEnemies = false;
    public TextMeshProUGUI enemyLeft;
    public TextMeshProUGUI win;
    void Start () {
        enemiesLeft = 10; // or whatever;
        win.gameObject.SetActive(false);
        
    }
     
    // Update is called once per frame
    void Update () {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Dummie");
        enemiesLeft = enemies.Length;
        if(enemiesLeft == 0)
        {
            endGame();
        }
    }
     
    void endGame()
    {
        killedAllEnemies = true;
    }
     
    void OnGUI()
    {
        if(killedAllEnemies)
        {
            enemyLeft.gameObject.SetActive(false);
            win.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StartCoroutine(WaitBeforeEnd());
        }
        else
        {
            enemyLeft.text = "Enemy Left : " + enemiesLeft;
        }
    }

    IEnumerator WaitBeforeEnd()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
}
