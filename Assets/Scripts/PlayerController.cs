﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D rb;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer sr;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PauseMenu.instance.isPaused || stopInput)
        {
            return;
        }

        if (knockBackCounter <= 0)
        {
            HandleInputs();
        } else
        {
            knockBackCounter -= Time.deltaTime;
            rb.velocity = new Vector2((sr.flipX ? 1 : -1) * knockBackForce, rb.velocity.y);
        }

        HandleAnimation();
    }

    private void HandleInputs()
    {
        rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                AudioManager.instance.PlaySFX(10);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = false;
                    AudioManager.instance.PlaySFX(10);
                }
            }
        }

        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }

    private void HandleAnimation()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("jumpSpeed", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        rb.velocity = new Vector2(0, knockBackForce);

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
