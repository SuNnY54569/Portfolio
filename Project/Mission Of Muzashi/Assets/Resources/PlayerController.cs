using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{

    [SerializeField]public float speed = 1f;
    [SerializeField]public float jumpSpeed = 9f;
    [SerializeField]public float maxSpeed = 10f;
    [SerializeField] public float jumpPower;
    [SerializeField]public bool grounded;
    [SerializeField]public float jumpRate = 1f;
    [SerializeField]public float nextJumpPress = 0.0f;
    [SerializeField]public float fireRate = 0.2f;
    [SerializeField]public float nextFireRate = 0.0f;
    [SerializeField] private Rigidbody2D rigidbody2D;
    private Physics2D physics2D;
    private Animator animator;
    public int healthbar = 100;
    public GameObject hitArea;
    
    void Start()
    {
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        animator.SetBool("Grounded",true);
        
        animator.SetFloat("Speed",Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (Input.GetButtonDown("Jump") && Time.time > nextJumpPress)
        {
            animator.SetBool("Jump",true);
            nextJumpPress = Time.time * jumpRate;
            rigidbody2D.AddForce(Vector2.up * (jumpSpeed * jumpPower));
        }
        else
        {
            animator.SetBool("Jump",false);
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFireRate)
        {
            nextFireRate = Time.time + fireRate;
            animator.SetBool("Attack",true);
            Attack();
        }
        else
        {
            animator.SetBool("Attack",false);
        }
        
    }

    public void Attack()
    {
        StartCoroutine(DelaySlash());
    }

    IEnumerator DelaySlash()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(hitArea,transform.position,transform.rotation);
    }
    
}
