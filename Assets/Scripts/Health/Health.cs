using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isDead;

    public float CurrentHealth { get; private set; }


    private void Awake()
    {
        this.CurrentHealth = this.startingHealth;
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AddHealth(float healthValue)
    {
        this.CurrentHealth = Mathf.Clamp(this.CurrentHealth + healthValue, 0, this.startingHealth);
    }

    public void TakeDamage(float damage)
    {
        print(damage);
        this.CurrentHealth = Mathf.Clamp(this.CurrentHealth - damage, 0, this.startingHealth);

        if (this.CurrentHealth > 0)
        {
            print("Current HEalth");
            print(this.CurrentHealth.ToString());
            this.animator.SetTrigger("hurt");
            StartCoroutine(this.Invulnerability());
            return;
        }

        if (!this.isDead)
        {
            this.animator.SetTrigger("die");
            if (GetComponent<PlayerMovement>())
                GetComponent<PlayerMovement>().enabled = false;

            if (GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if (GetComponent<Enemy>())
                GetComponent<Enemy>().enabled = false;
            this.isDead = true;
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < this.numberOfFlashes; i++)
        {
            this.spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(this.iFramesDuration / (this.numberOfFlashes * 2));
            this.spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(this.iFramesDuration / (this.numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
