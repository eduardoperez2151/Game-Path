using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePointTransform;

    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer;

    private float lifeTime;
    private Animator animator;
    private PlayerMovement playerMovement;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.playerMovement = GetComponent<PlayerMovement>();
        this.cooldownTimer = Mathf.Infinity;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && this.cooldownTimer > this.attackCooldown && this.playerMovement.CanAttack())
        {
            Attack();
        }
        this.cooldownTimer += Time.deltaTime;

    }

    private void Attack()
    {
        this.animator.SetTrigger("attack");
        this.cooldownTimer = 0;
        this.fireballs[FindFireball()].transform.position = this.firePointTransform.position;
        this.fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
