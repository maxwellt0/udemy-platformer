using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    public GameObject slammerTarget;
    public float slamSpeed;
    public float waitAfterSlam;
    public float resetSpeed;

    
    private float waitAfterSlamCounter;
    private int directionY = 0;
    private Vector3 initialPos;
    private void Start()
    {
        initialPos = transform.position;
        slammerTarget.transform.parent = null;
    }

    private void Update()
    {
        if (waitAfterSlamCounter > 0f)
        {
            waitAfterSlamCounter -= Time.deltaTime;
            return;
        }
        
        switch (directionY)
        {
            case 0 when IsPlayerClose():
                directionY = -1;
                break;
            case -1 when IsAtTheBottom():
                waitAfterSlamCounter = waitAfterSlam;
                directionY = 1;
                break;
            case 1 when IsAtTheTop():
                directionY = 0;
                break;
        }

        MoveInDirection();
    }

    private void MoveInDirection()
    {
        switch (directionY)
        {
            // going down
            case -1:
                transform.position =
                    Vector3.MoveTowards(transform.position, slammerTarget.transform.position, slamSpeed * Time.deltaTime);
                break;
            // going up
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, initialPos, resetSpeed * Time.deltaTime);
                break;
        }
    }

    private bool IsAtTheTop()
    {
        return Vector3.Distance(initialPos, transform.position) < .05f;
    }

    private bool IsAtTheBottom()
    {
        return Vector3.Distance(slammerTarget.transform.position, transform.position) < .05f;
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(slammerTarget.transform.position, PlayerController.instance.transform.position) < 2f;
    }
}
