using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    public BossTankController bossController;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossController.TakeHit();
            PlayerController.instance.Bounce();
            
            gameObject.SetActive(false);
        }
    }
}
