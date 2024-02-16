using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class playerCombat : MonoBehaviour
{
    public static playerCombat instance;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public bool canReceiveInput;
    public bool inputReceive;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canReceiveInput)
            {
                inputReceive = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }
        
    }

    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }
    }
}
