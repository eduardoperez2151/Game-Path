using System;
using UnityEngine;

public class SpikeHead : EnemyDamage
{

    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayerMask;
    private float checkTimer;
    private Vector3 destination;

    private Vector3[] directions;
    private bool isAttacking;


    private void Awake()
    {
        this.directions = new Vector3[4];
    }


    private void Update()
    {
        if (this.isAttacking)
        {

            transform.Translate(destination * speed * Time.deltaTime);
        }
        else
        {
            this.checkTimer += Time.deltaTime;
            if (this.checkTimer > this.checkDelay)
            {
                CheckForPLayer();
            }
        }
    }

    private void OnEnable()
    {
        this.Stop();
    }

    private void CheckForPLayer()
    {
        this.calculateDirections();
        for (int i = 0; i < this.directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, this.directions[i], this.range, this.playerLayerMask);

            if (hit.collider != null && !isAttacking)
            {
                this.isAttacking = true;
                this.destination = this.directions[i];
                this.checkTimer = 0;
            }
        }
    }

    private void calculateDirections()
    {
        this.directions[0] = transform.right * range; //right
        this.directions[1] = -transform.right * range; //left
        this.directions[2] = transform.up * range; //up
        this.directions[3] = -transform.up * range;//down
    }

    private void Stop()
    {
        this.destination = transform.position;
        this.isAttacking = false;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        this.Stop();
    }
}
