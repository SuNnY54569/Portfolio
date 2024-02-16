using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isHit = false;
    public GameObject player;
    public Rigidbody rb;
    public float hitForce;
    private float blowOut;
    public float tuque;
    
    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Hit()
    {
        if (isHit)
        {
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            blowOut = rb.mass * hitForce;
            rb.AddForce(direction * blowOut, ForceMode.Impulse);
            rb.AddTorque(transform.up * tuque);
        }
    }
}
