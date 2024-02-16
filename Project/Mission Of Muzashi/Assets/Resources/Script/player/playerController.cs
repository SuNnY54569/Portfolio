using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class playerController : MonoBehaviour
{
    
    public static playerController instance;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] public Animator playerAnimator;
    private Health EnemyHealth;
    
    
    
    [Header("Movement Variable")]
    [SerializeField] public float maxSpeed;
    [SerializeField] public bool canMove = true;
    [SerializeField] bool facingRight;
    [SerializeField] public bool isAttacking = false;

    [Header("Ground Check")]
    [SerializeField] bool grounded = false;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask ground;
    [SerializeField] public Transform groundCheck;
    
    [Header("Jumping Variable")]
    [SerializeField] public float jumpHeight;
    [SerializeField] public float nextJumpPress;
    [SerializeField] public float jumpRate;
    [SerializeField] public float jumpPower;
    
    [Header("Attack Parameter")]
    [SerializeField] private float range;
    [SerializeField] private int damage;
    
    [Header("Collider Parameter")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("Sound")] 
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip attackSound;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        facingRight = true;
    }

    void Update()
    {
        EnemyInSight();
        
        if (grounded && Input.GetButtonDown("Jump") && Time.time > nextJumpPress && isAttacking == false)
        {
            
            grounded = false;
            playerAnimator.SetBool("Grounded",grounded);
            nextJumpPress = Time.time * jumpRate;
            playerRb.AddForce(Vector2.up * (jumpHeight * jumpPower));
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(jumpSound);
            
        }
        
        if (grounded && Input.GetKeyDown(KeyCode.W) && Time.time > nextJumpPress && isAttacking == false)
        {
            
            grounded = false;
            playerAnimator.SetBool("Grounded",grounded);
            nextJumpPress = Time.time * jumpRate;
            playerRb.AddForce(Vector2.up * (jumpHeight * jumpPower));
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(jumpSound);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            Attack();
            GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(attackSound);
        }

        playerAnimator.SetFloat("VerticalSpeed",playerRb.velocity.y);
        
        float move = Input.GetAxis("Horizontal");
        if (canMove)
        {
            playerRb.velocity = new Vector2(move * maxSpeed, playerRb.velocity.y);
        }
        playerAnimator.SetFloat("Speed",Mathf.Abs(move));
        
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

    }

    void FixedUpdate()
    {
        
        //Check if Grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        playerAnimator.SetBool("Grounded",grounded);

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Attack()
    {
        isAttacking = true;
    }

    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance
            , new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y,boxCollider.bounds.size.z), 0, Vector2.left, 0, enemyLayer);
       
        if (hit.collider != null)
            EnemyHealth = hit.transform.GetComponent<Health>();
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y,boxCollider.bounds.size.z));
    }
    
    public void DamageEnemy()
    {
        if (EnemyInSight())
        {
            EnemyHealth.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
