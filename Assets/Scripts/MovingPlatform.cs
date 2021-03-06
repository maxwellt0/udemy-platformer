﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;

    public Transform platform;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(platform.position, points[currentPoint].position) < .05f)
        {
            currentPoint++;
            if (currentPoint > points.Length - 1)
            {
                currentPoint = 0;
            }
        }
    }
}
