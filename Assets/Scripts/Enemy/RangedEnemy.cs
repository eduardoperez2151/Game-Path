using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerMask;


    private SpriteRenderer spriteRenderer;
    private EnemyPatrol enemyPatrol;
    private Health playerHealth;
    private float coolDownTimer;
    private Animator animator;


    private void Awake()
    {
        this.coolDownTimer = Mathf.Infinity;
        this.animator = GetComponent<Animator>();
        this.enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        this.coolDownTimer += Time.deltaTime;

        if (this.IsPlayerInSight())
        {
            if (this.coolDownTimer >= this.attackCoolDown)
            {
                this.coolDownTimer = 0;
                this.animator.SetTrigger("rangeAttack");
            }
        }

        if (this.enemyPatrol != null)
        {
            this.enemyPatrol.enabled = !IsPlayerInSight();
        }
    }

    private bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.boxCollider2D.bounds.center + transform.right * this.range * transform.localScale.x * colliderDistance,
         new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
         0, Vector2.left, 0, playerMask);

        return hit.collider != null;
    }

    private void RangedAttack()
    {
        this.coolDownTimer = 0;
        this.fireballs[FindFireball()].transform.position = this.firePoint.position;
        this.fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }


    private int FindFireball()
    {
        for (int i = 0; i < this.fireballs.Length; i++)
        {
            if (!this.fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * this.range * transform.localScale.x * colliderDistance, new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (this.IsPlayerInSight())
        {
            this.playerHealth.TakeDamage(this.damage);
        }
    }
}
