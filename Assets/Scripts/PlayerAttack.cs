using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;
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
    }
}
