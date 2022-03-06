using System;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider2D;


    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        this.lifeTime = 0;
        this.gameObject.SetActive(true);
        this.boxCollider2D.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = this.speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        this.lifeTime += Time.deltaTime;
        if (this.lifeTime > this.resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {


        base.OnTriggerEnter2D(collision);
        this.boxCollider2D.enabled = false;


        if (collision.tag != "Collectible")
        {
            hit = true;

            if (this.animator != null)
            {
                this.animator.SetTrigger("explode");
            }


            gameObject.SetActive(false);

        }

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }


}