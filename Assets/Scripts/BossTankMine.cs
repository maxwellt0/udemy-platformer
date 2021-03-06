﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
            PlayerHealthController.instance.DealDamage();
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(3);
    }
}
