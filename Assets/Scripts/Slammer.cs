using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    public GameObject smasher;
    public GameObject slammerTarget;
    public float slamSpeed;
    public float waitAfterSlam;
    public float resetSpeed;

    private float waitAfterSlamCounter;
    private bool hasSlammed;
    private Vector3 initialPos;
    private void Start()
    {
        initialPos = transform.position;
        slammerTarget.transform.parent = null;
    }

    private void Update()
    {
        if (waitAfterSlamCounter > 0f && hasSlammed)
        {
            waitAfterSlamCounter -= Time.deltaTime;
            return;
        }
        
        if (Vector3.Distance(slammerTarget.transform.position, transform.position) < .5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, resetSpeed * Time.deltaTime);
        }
        
        if (Vector3.Distance(slammerTarget.transform.position, PlayerController.instance.transform.position) < 2f)
        {
            MoveToTarget();

            if (Vector3.Distance(slammerTarget.transform.position, smasher.transform.position) < .5f)
            {
                hasSlammed = true;
                waitAfterSlamCounter = waitAfterSlam;
            }
        }
        
       
    }

    private void MoveToTarget()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, slammerTarget.transform.position, slamSpeed * Time.deltaTime);
    }
}
