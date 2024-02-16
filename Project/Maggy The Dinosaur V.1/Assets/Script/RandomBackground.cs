using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    private int bgRandom;
    
    // Start is called before the first frame update
    void Start()
    {
        bgRandom = Random.Range(0, 2);
        switch (bgRandom)
        {
            case 0:
                background1.SetActive(true);
                background2.SetActive(false);
                break;
            case 1:
                background1.SetActive(false);
                background2.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
