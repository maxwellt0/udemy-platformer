using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;
    private SpriteRenderer theSR;
    public Sprite downSprite;
    private bool hasSwitched;
    public bool deactivateOnSwitch;
    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSwitched)
        {
            objectToSwitch.SetActive(!deactivateOnSwitch);
            theSR.sprite = downSprite;
            hasSwitched = true;
        }
    }
}
