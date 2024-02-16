using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Enemy_Behaviour : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform leftLimit;
    public Transform rightLimit;

    [Header("Attack")]
    [HideInInspector] public Transform _target;
    [HideInInspector] public bool _inRange; // Check if player is in range
    [SerializeField] private float collisionDamage;
    public GameObject hotZone;
    public GameObject triggerArea;
    private Animator _anim;
    public float attackDistance; // minimum distance for attack
    public float timer; // attack cooldown
    private float _distance;  // Store distance between player and Enemy
    private bool _attackMode;
    private bool _cooling; // Check if Enemy is cooling after attack
    private float _intTimer;
    
    



    private void Awake()
    {
        SelectTarget();
        _intTimer = timer; // Store the initial Value of Timer
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_attackMode)
        {
            Move();
        }

        if (!InsideOfLimit() && !_inRange && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            SelectTarget();
        }
        if (_inRange)
        {
            EnemyLogic();
        }
    }
    
    void EnemyLogic()
    {
        _distance = Vector2.Distance(transform.position, _target.position);

        if (_distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= _distance && _cooling == false)
        {
            Attack();
        }

        if (_cooling)
        {
            Cooldown();
            _anim.SetBool("Attack", false);
        }
    }
    
    void Move()
    {
        _anim.SetBool("canWalk",true);
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(_target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = _intTimer; // Reset timer when Player enter Attack Range
        _attackMode = true; // To check if Enemy can still attack or not
        
        _anim.SetBool("canWalk", false);
        _anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <=0 && _cooling && _attackMode)
        {
            _cooling = false;
            timer = _intTimer;
        }
    }
    void StopAttack()
    {
        _cooling = false;
        _attackMode = false;
        _anim.SetBool("Attack",false);
    }
    
    public void TriggerCooling()
    {
        _cooling = true;
    }

    private bool InsideOfLimit()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            _target = leftLimit;
        }
        else
        {
            _target = rightLimit;
        }
        
        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > _target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(collisionDamage);
        }
    }
}
