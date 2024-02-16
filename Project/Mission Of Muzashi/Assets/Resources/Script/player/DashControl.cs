using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    bool _playerCollision = true;
    
    [Header("Dashing")]
    [SerializeField] float startDashTime = 1f;
    [SerializeField] float dashSpeed = 1f;
    float _currentDashTime;
    bool _canDash = true;
    
    [Header("Dash Cooldown")]
    [SerializeField] float dashCooldown = 0f;
    [SerializeField] float MaxCooldown = 2f;
    private bool _cool;
    
    [Header("Sound")] 
    [SerializeField] private AudioClip dashSound;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
 
    void Update()
    {
        dashCooldown -= Time.deltaTime;
        if (_canDash && Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Dash(Vector2.up));
                GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(dashSound);
            }
 
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(Vector2.left));
                GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(dashSound);
            }
            
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(Vector2.right));
                GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<SoundManager>().PlaySound(dashSound);
            }

        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        _canDash = false;
        _playerCollision = false;
        _currentDashTime = startDashTime; // Reset the dash timer.
        

        while (_currentDashTime > 0f)
        {
            _currentDashTime -= Time.deltaTime; // Lower the dash timer each frame.

            _rb.velocity = direction * dashSpeed; // Dash in the direction that was held down.
            // No need to multiply by Time.DeltaTime here, physics are already consistent across different FPS.
            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        _rb.velocity = new Vector2(0f, 0f); // Stop dashing.

        _canDash = true;
        _playerCollision = true;
        dashCooldown = MaxCooldown;
    }
}
