using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dome : MonoBehaviour
{
    [SerializeField] private GameObject dome;
    [SerializeField] private int domeDuration = 5;
    [SerializeField] private int domeCooldown = 5;
    [SerializeField] private bool isCooldown = false;
    [SerializeField] private GameObject water;
    // Start is called before the first frame update
    void Start()
    {
        dome = GameObject.FindGameObjectWithTag("Dome");
        dome.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (!isCooldown)
            {
                SoundManager.instance.Play(SoundManager.SoundName.DomeUp);
                isCooldown = true;
                //Debug.Log("dome up");
                StartCoroutine(DomeUp());
            }
            else
            {
                Debug.Log("Dome is cooldown");
            }
            
        }
    }

    IEnumerator DomeUp()
    {
        dome.SetActive(true);
        water.SetActive(false);
        yield return new WaitForSeconds(domeDuration);
        dome.SetActive(false);
        yield return new WaitForSeconds(domeCooldown);
        isCooldown = false;
        water.SetActive(true);
    }

}

