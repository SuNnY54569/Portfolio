using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void EndRestart()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().StartMusic();
        SceneManager.LoadScene(2);
    }
    
    public void EndMainMenu()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().EndMusic();
        SceneManager.LoadScene(0);
    }
    
    public void EndQuit()
    {
        Application.Quit();
    }
}
