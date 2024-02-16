using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    // Start is called before the first frame update
    public void Resume()
    {
        StartSceneUI.instance.onSetting = false;
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
        SceneManager.UnloadSceneAsync("Setting");
    }
}
