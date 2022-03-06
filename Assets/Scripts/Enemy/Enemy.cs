using System;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    private float coolDownTimer;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;


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
                this.animator.SetTrigger("enemyAttack");
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


        if (hit.collider != null)
        {
            this.playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
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
