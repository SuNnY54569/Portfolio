using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    [SerializeField]public int iLevelToload;
    [SerializeField]public string sLevelToLoad;

    public bool useIntergerToLoadLevel = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (useIntergerToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToload);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }
}
