using System;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;

    public void ActivateProjectile()
    {
        this.lifeTime = 0;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
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

        if (collision.tag != "Collectible")
        {
            gameObject.SetActive(false);
        }

    }


}