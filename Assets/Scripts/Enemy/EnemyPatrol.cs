using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform LeftEdgeTransform;
    [SerializeField] private Transform RightEdgeTransform;


    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Enemy Animator")]
    [SerializeField] private Animator enemyAnimator;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private Vector3 initialScale;
    private bool isMovingLeft;


    private void Awake()
    {
        this.initialScale = this.enemy.localScale;
    }

    private void Update()
    {
        if (isMovingLeft)
        {

            if (enemy.position.x >= LeftEdgeTransform.position.x)
            {
                this.MoveInDirection(-1);
                return;
            }
            DirectionChange();
            return;
        }

        if (enemy.position.x <= RightEdgeTransform.position.x)
        {
            this.MoveInDirection(1);
            return;
        }
        DirectionChange();
    }

    private void DirectionChange()
    {
        this.enemyAnimator.SetBool("moving", false);
        this.idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            this.isMovingLeft = !isMovingLeft;
        }

    }


    private void MoveInDirection(int direction)
    {
        this.idleTimer = 0;
        this.enemyAnimator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }

    private void OnDisable()
    {
        this.enemyAnimator.SetBool("moving", false);
    }
}
