using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [SerializeField] private float damage;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isTriggered;
    private bool isActive;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!this.isTriggered)
            {
                StartCoroutine(this.ActivateTrapFire());
            }

            if (this.isActive)
            {
                collision.GetComponent<Health>().TakeDamage(this.damage);
            }
        }
    }

    private IEnumerator ActivateTrapFire()
    {
        
        this.isTriggered = true;
        this.spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(this.activationDelay);
        this.animator.SetBool("activated", true);
        this.isActive = true;
        this.spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(this.activeTime);
        this.animator.SetBool("activated", false);
        this.isTriggered = false;
        this.isActive = false;
    }
}
