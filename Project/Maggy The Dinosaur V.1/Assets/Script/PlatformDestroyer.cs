using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{

    public GameObject platformDestroyerPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        platformDestroyerPoint = GameObject.Find("PlatformDestroyerPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < platformDestroyerPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }   
    }
}
