﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    // Start is called before the first frame update
    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;

    }

    // Update is called once per frame
    private void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            Move();

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);

        } else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;

            theRB.velocity = new Vector2(0, theRB.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f); ;
            }

            anim.SetBool("isMoving", false);
        }
    }

    private void Move()
    {
        if (movingRight)
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            theSR.flipX = true;

            if (transform.position.x > rightPoint.transform.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            theSR.flipX = false;

            if (transform.position.x < leftPoint.transform.position.x)
            {
                movingRight = true;
            }
        }
    }
}
