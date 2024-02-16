using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] private float moveSpeedStore;
    [SerializeField] public float speedMultiplier;

    [SerializeField] public float speedIncreaseMilestone;
    [SerializeField] public float speedIncreaseMilestoneStore;
    
    [SerializeField] private float speedMilestoneCount;
    [SerializeField] private float speedMilestoneCountStore;
    
    [SerializeField] public float jumpForce;
    [SerializeField] public float jumpTime;
    [SerializeField] private float jumpTimeCounter;

    [SerializeField] private bool stoppedJumping;
    [SerializeField] private bool canDoubleJump;

    private Rigidbody2D myRigidBody;

    [SerializeField] public bool grounded;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius;

    [SerializeField] public int playerLife;
    [SerializeField] public int playerDefaultLife;
    [SerializeField] public Text liveText;
    
    private PlatformDestroyer[] boneList;
    

    private Collider2D myCollider;

    private Animator MyAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<Collider2D>();

        MyAnimator = GetComponent<Animator>();

        liveText.text = "Life: " + playerLife;

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone *= speedMultiplier;

            moveSpeed *= speedMultiplier;
        }

        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSound.Play();
            if (grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                stoppedJumping = false;
            }

            if (!grounded && canDoubleJump)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
            }

            
        }

        if (Input.GetKey(KeyCode.Space) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }
        
        MyAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        MyAnimator.SetBool("Grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("killbox"))
        {
            deathSound.Play();
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            
        }
        
        if (other.gameObject.CompareTag("Bone"))
        {

            if (playerLife > 1)
            {
                deathSound.Play();
                playerLife--;
                liveText.text = "Life: " + playerLife;
                boneList = FindObjectsOfType<PlatformDestroyer>();
                for (int i = 0; i < boneList.Length; i++)
                {
                    if (boneList[i].gameObject.name.Contains("Bone")) 
                    {
                        boneList[i].gameObject.SetActive(false);
                    }
                        
                }
                
                
            }
            else if (playerLife <= 1)
            {
                liveText.text = "Life: " + playerLife;
                playerLife--;
                deathSound.Play();
                theGameManager.RestartGame();
                moveSpeed = moveSpeedStore;
                speedMilestoneCount = speedMilestoneCountStore;
                speedIncreaseMilestone = speedIncreaseMilestoneStore;
            }
            
            
        }
    }
}
