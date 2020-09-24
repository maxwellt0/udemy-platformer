using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator anim;
    public float bounceForce = 20f;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }
}
