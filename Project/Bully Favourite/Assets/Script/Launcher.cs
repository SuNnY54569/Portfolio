using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;
    
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] private float launchForce = 1.5f;
    [SerializeField] private float trajectoryTimeStep = 0.05f;
    [SerializeField] private int trajectoryStepCount = 15;
    
    public int remainingBall = 2;
    public bool isWin;

    private int maxBall = 1;
    public int ballThrown = 0;
    public bool isPaused;

    public int starGet = 0;

    private Vector2 velocity, startMousePos, currentMousePos;

    private void Start()
    {
        instance = this;
        UI.instance.UpdateText();
        isWin = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && remainingBall > 0 && ballThrown < maxBall && !isPaused)
        { 
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }//click left button
            
        if (Input.GetMouseButton(0) && remainingBall > 0 && ballThrown < maxBall && !isPaused) 
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePos - currentMousePos) * launchForce;
                     
            DrawTrajectory();
            RotateLauncher();
        }//Hold left button
            
        if (Input.GetMouseButtonUp(0) && remainingBall > 0 && ballThrown < maxBall && !isPaused)
        {
            
            FireProjectile();
            ClearTrajectory();
            remainingBall--;
            ballThrown++;
            UI.instance.UpdateText();
            AudioManager.instance.PlaySFX(AudioManager.instance.throwBall);
        }//Release left button
        CheckHaveNoBall();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Pause.instance.Resume();
                AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
            }
            else
            {
                UI.instance.PauseGame();
                AudioManager.instance.PlaySFX(AudioManager.instance.buttonPress);
            }
            
        }
    }

    void DrawTrajectory() 
    {
        Vector3[] position = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t;

            position[i] = pos;
        }

        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(position);
    }//Draw guideline

    void RotateLauncher()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }//Rotate Arm

    void FireProjectile()
    {
        Transform pr = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        pr.GetComponent<Rigidbody2D>().velocity = velocity;
    }//Throw the ball

    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }//Remove guide line

    public void CheckHaveNoBall()
    {
        if (remainingBall == 0 && isWin == false && GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            UI.instance.youLose.SetActive(true);
            AudioManager.instance.PlaySFX(AudioManager.instance.failSound);
        }
    }//Check if there is no ball left and can't win
    
}
