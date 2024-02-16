using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    public bool isHit = false;
    public GameObject player;
    public Rigidbody rb;
    public float hitForce;
    public float blowOut;
    public float angularSpeed;
    public Material die;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    public void Hit()
    {
        if (isHit)
        {
            GetComponent<Renderer>().material = die;
            Vector3 direction = transform.position - player.transform.position;
            direction.Normalize();
            Vector3 velocity = direction.normalized * blowOut;
            Vector3 angularVelocity = Vector3.Cross(direction, Vector3.up * angularSpeed);
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;
            rb.AddForce(angularVelocity);
            Destroy(gameObject,2f);
        }
    }
}
