using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPosition;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(changeSound);
        }
        if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(changeSound);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(interactSound);
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
        {
             
        }

        if (currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if(currentPosition> options.Length - 1)
        {
            currentPosition = 0;
        }
        
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
